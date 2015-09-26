using UnityEngine;
using System.Collections;

public class Character_Shot : MonoBehaviour {

	/*

	弾を撃つ動作全般

	*/

	//変数(ex:time)////////////////////////////////
	
	public int bullet_num = 0;//選択している弾の種類

	//GameObject/////////////////////////////////////////////
	
	public GameObject[] Bullet;//弾の箱
	public Transform[] Muzzle;//発射口

	//SE/////////////////////////////////////////////////
	
	//private var audioSource : AudioSource;//script内での音
	
	////////////////////////////////////////////////////
	
	//使うもの
	//private Animator animator;//アニメーション設定用

	private Character_Parameter character_parameter;
	private Player_Flag player_flag;
	private Bullet_Parameter bullet_parameter;
	private MousePoint Mouse;

	private Coroutine coroutine;
	private float time = 0;//いろいろ時間測るよう(使ったら0にすること)

	void Start() {
		//animator = GetComponentInChildren< Animator >();//アニメータを使うとき

		character_parameter = GetComponent< Character_Parameter >();//うつ人のパラメタ取得
		player_flag = GetComponent< Player_Flag >();//フラグ取得
		Mouse = GetComponent< MousePoint >();//マウスの位置

		//ToMouse = GameObject.Find( "MainCamera" ).GetComponent( "ToMouse" ) as ToMouse;
		//audioSource = gameObject.GetComponent(AudioSource);
		
	}

	void Update () {

		BulletSelect ();

		if(bullet_parameter == null){

			bullet_parameter = Bullet[bullet_num].GetComponent<Bullet_Parameter>();//選択してる弾のパラメタ読み込み

		}

		BulletSwitch ();

		if(Input.GetKeyDown(KeyCode.Q)){

			player_flag.flag_action = true;
			player_flag.flag_snipe = false;

		}
	}

	void BulletSelect (){

		//マウスホイール式
		if(Input.GetAxis("Mouse ScrollWheel") == 1.0f){
			
			if(bullet_num == Bullet.Length - 1){
				
				bullet_num = 0;
				
			}else{
				
				bullet_num += (int)Input.GetAxis("Mouse ScrollWheel");
				
			}
			
			bullet_parameter = null;//念のためパラメタリセット

		}
		
		if(Input.GetAxis("Mouse ScrollWheel") == -1.0f){
			
			if(bullet_num == 0){
				
				bullet_num = Bullet.Length - 1;
				
			}else{
				
				bullet_num += (int)Input.GetAxis("Mouse ScrollWheel");
				
			}

			bullet_parameter = null;//念のためパラメタリセット

		}

		//右クリック式
		/*if(Input.GetMouseButtonDown(2)){
			
			if(bullet_num == 0){
				
				bullet_num = Bullet.Length - 1;
				
			}else{
				
				bullet_num -= 1;
				
			}
			
		}
		
		if(Input.GetMouseButtonDown(1)){
			
			if(bullet_num == Bullet.Length - 1){
				
				bullet_num = 0;
				
			}else{
				
				bullet_num += 1;
				
			}
			
		}*/

	}

	void BulletSwitch (){

		switch(bullet_parameter.at_name){
		case "Bullet":
			if( Input.GetButtonDown( "Fire1" ) ) {
				if(!player_flag.flag_key){
					
					Shot();
					player_flag.flag_key = true;
					
				}
			}
			break;
		case"Snipe":
			if( Input.GetButtonDown( "Fire1" ) ) {
				if(!player_flag.flag_key){
					
					Snipe();
					
				}
			}
			break;
		case "Buff":
			if( Input.GetButtonDown( "Fire1" ) ) {
				if(!player_flag.flag_key){
					
					Buff();
					player_flag.flag_key = true;
					
				}
			}
			break;
		case "Air":
			if( Input.GetButtonDown( "Fire1" ) ) {
				if(!player_flag.flag_key){
					
					Air();
					
				}
			}
			break;
		case "Ya":
			if( Input.GetButton( "Fire1" ) ) {
				if(!player_flag.flag_key){

					time += Time.deltaTime;
					
				}
			}
			if( Input.GetButtonUp( "Fire1" ) ) {
				if(!player_flag.flag_key){

					if(time > 1){

						time = 1;

					}
					Ya (time);
					time = 0;
					player_flag.flag_key = true;

				}
			}
			break;
		default:
			if( Input.GetButtonDown( "Fire1" ) ) {
				if(!player_flag.flag_key){
					
					Shot();
					player_flag.flag_key = true;
					
				}
			}
			break;
		}

	}
	
	void Shot() {
		
		GameObject bullet;

		bullet_parameter.Parent = this.gameObject;//誰が撃ったかを渡す
		bullet = GameObject.Instantiate( Bullet[bullet_num] );

		/*RaycastHit hit;

		if(Physics.Raycast(transform.position,transform.TransformDirection (Vector3.forward),10)){

			Debug.Log("www");

		}*/

		//弾を飛ばす処理
		bullet.transform.position = Muzzle[bullet_num].position + (character_parameter.direction );
		bullet.GetComponent<Rigidbody>().velocity = ( (Mouse.worldPoint - this.transform.position).normalized * bullet_parameter.at_speed );
		/*if(!audioSource.isPlaying){
			
			audioSource.Play();
			
		}*/
		//硬直
		Invoke ("UnLock",bullet_parameter.wait_time);

		Destroy( bullet, bullet_parameter.at_time );
		//Gauge.SendMessage("MPn",0.01);

	}

	void Air() {

		bullet_parameter.Parent = this.gameObject;//誰が撃ったかを渡す

		if (player_flag.flag_air) {
			if(!player_flag.flag_airjump){

				character_parameter.move_direction.y = 0;
				character_parameter.now_speed *= bullet_parameter.at_speed;
				character_parameter.move_direction += bullet_parameter.move_direction;
				Invoke("Reset",bullet_parameter.at_time);
				player_flag.flag_airjump = true;

			}
		}

		/*if(!audioSource.isPlaying){
			
			audioSource.Play();
			
		}*/

	}

	void Buff() {

		bullet_parameter.Parent = this.gameObject;//誰が撃ったかを渡す
		character_parameter.now_speed *= bullet_parameter.at_speed;
		Invoke("Reset",bullet_parameter.at_time);
		/*if(!audioSource.isPlaying){
			
			audioSource.Play();
			
		}*/
		//硬直
		Invoke ("UnLock",bullet_parameter.wait_time);
		
	}

	void Snipe (){

		if(!player_flag.flag_snipe){

			Camera.main.transform.position = Muzzle[bullet_num].transform.position;
			player_flag.flag_snipe = true;
			player_flag.flag_action = false;

		}else{

			player_flag.flag_key = true;
			GameObject bullet;
			
			bullet_parameter.Parent = this.gameObject;//誰が撃ったかを渡す
			bullet = GameObject.Instantiate( Bullet[bullet_num] );
			
			/*RaycastHit hit;

		if(Physics.Raycast(transform.position,transform.TransformDirection (Vector3.forward),10)){

			Debug.Log("www");

		}*/
			
			//弾を飛ばす処理
			bullet.transform.position = Muzzle[bullet_num].position + (character_parameter.direction );
			bullet.GetComponent<Rigidbody>().velocity = ( (Mouse.worldPoint - Muzzle[bullet_num].position).normalized * bullet_parameter.at_speed );
			/*if(!audioSource.isPlaying){
			
			audioSource.Play();
			
		}*/
			//硬直
			Invoke ("UnLock",bullet_parameter.wait_time);
			
			Destroy( bullet, bullet_parameter.at_time );
			//Gauge.SendMessage("MPn",0.01);

		}

	}

	void Ya (float drawTime) {

		GameObject bullet;
		
		bullet_parameter.Parent = this.gameObject;//誰が撃ったかを渡す
		bullet = GameObject.Instantiate( Bullet[bullet_num] );

		//弾を飛ばす処理
		bullet.transform.position = Muzzle[bullet_num].position + (character_parameter.direction );
		bullet.transform.rotation = Quaternion.LookRotation(-(Mouse.worldPoint - this.transform.position).normalized);//回転させて矢じりを進行方向に向ける
		bullet.GetComponent<Rigidbody>().velocity = ( (Mouse.worldPoint - this.transform.position).normalized * bullet_parameter.at_speed * drawTime);
		/*if(!audioSource.isPlaying){
			
			audioSource.Play();
			
		}*/
		//硬直
		Invoke ("UnLock",bullet_parameter.wait_time);
		
		Destroy( bullet, bullet_parameter.at_time );
		//Gauge.SendMessage("MPn",0.01);
		
	}

	void UnLock (){

		player_flag.flag_key = false;

	}

	void Reset (){

		character_parameter.now_c_point = character_parameter.max_c_point;//今のコンセントレートポイント
		character_parameter.now_mag_point = character_parameter.max_mag_point;//今のマジックポイント
		character_parameter.now_speed = character_parameter.speed; //今のキャラクタの移動速度
		character_parameter.now_jump_power = character_parameter.jump_power;//今のキャラクタがジャンプする高さ
		player_flag.flag_airjump = false;

	}

}
