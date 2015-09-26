using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {
	/*
	プレイヤーの操作用
	Jump()//ジャンプ
	Move()//移動

	*/

	//使うもの
	private CharacterController playerController;//キャラクタコントローラで動かす場合
	private Animator animator;//アニメーション設定用
	private Character_Parameter character_parameter;
	private Player_Flag player_flag;

	void Start() {
		playerController = GetComponent< CharacterController >();//rigidbodyを使う場合は外す
		animator = GetComponentInChildren< Animator >();//アニメータを使うとき

		character_parameter = GetComponent< Character_Parameter >();//パラメタ取得
		player_flag = GetComponent< Player_Flag >();//フラグ取得

	}
	
	void Update() {
		//アニメーションリセット///////////////////////////////////////////////////////////
		//character_parameter.Model.transform.localPosition = character_parameter.adjust;
		character_parameter.move_direction = new Vector3(0.0f,character_parameter.move_direction.y,0.0f);
		//キーボードからの入力読み込み/////////////////////////////////////////////////////

		float InputX = 0.0f;
		float InputY = 0.0f;
		
		Vector3 inputDirection = Vector3.zero;//入力された方向

		if (!player_flag.flag_key && player_flag.flag_action) {

			InputX = Input.GetAxis ("Horizontal");
			InputY = Input.GetAxis ("Vertical");

			//if(flag_Key){
			
			inputDirection = new Vector3 (InputX, 0, InputY);//入力された方向
			
			//ジャンプ
			if (playerController.isGrounded) {
				player_flag.flag_air = false;
				player_flag.flag_airjump = false;
				character_parameter.move_direction = Vector3.zero;
				animator.SetBool ("Jump", false);
				if (Input.GetButtonDown ("Jump")) {
					
					Jump ();

				}
				//J_counter = 0;
				//animator.SetBool("Jump",false);
			}

		}
			
			/*if(J_counter < 1){
				
				if( Input.GetButtonDown("Jump")) {
					direction.y = jumpPower;
					animator.SetBool("Jump",true);
					J_counter++;
				}
			}else if(J_counter < 2){
				
				if(flag_twoJ){
					if(flag_MP){
						if( Input.GetButtonDown("Jump")) {
							direction.y = jumpPower;
							animator.SetBool("Jump",true);
							J_counter++;
							Gauge.SendMessage("MPn",2);
						}
					}
				}
			}*/
			
		//キャラクタの方向回転
		//this.transform.Rotate(0,Input.GetAxis("Mouse X") * character_parameter.now_speed * Time.deltaTime * 90,0);
		this.transform.Rotate(0,InputX * character_parameter.now_speed * Time.deltaTime * 12,0);
		//}

		//キャラクタの方向を取得
		character_parameter.direction = transform.TransformDirection(Vector3.forward);

		//キャラクタ移動処理
		if (inputDirection.magnitude > 0.1) {

			_Move ();
			animator.SetFloat ("Speed", character_parameter.move_direction.magnitude);

		} else {

			animator.SetFloat ("Speed", 0);

		}

		//キャラにかかる重力決定（少しふわふわさせてる）
		if(character_parameter.move_direction.y > -2){

			if(!player_flag.flag_airjump){

				character_parameter.move_direction.y += Physics.gravity.y * Time.deltaTime;

			}

		}

		//キャラの移動実行（動かす方向＊動かすスピード＊補正）
		playerController.Move( character_parameter.move_direction * character_parameter.now_speed * Time.deltaTime );

	}

	//ジャンプ
	void Jump (){
		
		character_parameter.move_direction.y = character_parameter.now_jump_power;
		player_flag.flag_air = true;
		animator.SetBool("Jump",true);

	}

	//実際に動かす方向決定
	void _Move (){

		//character_parameter.move_direction = new Vector3(0.0f,character_parameter.move_direction.y,0.0f);
		
		if(Input.GetKey(KeyCode.W)){
			character_parameter.move_direction.x += character_parameter.direction.x;
			character_parameter.move_direction.z += character_parameter.direction.z;
		}
		if(Input.GetKey(KeyCode.S)){
			character_parameter.move_direction.x -= character_parameter.direction.x / 2;
			character_parameter.move_direction.z -= character_parameter.direction.z / 2;
		}
		if(Input.GetKey(KeyCode.A)){
			character_parameter.move_direction.x -= character_parameter.direction.z / 10;
			character_parameter.move_direction.z += character_parameter.direction.x / 10;
		}
		if(Input.GetKey(KeyCode.D)){
			character_parameter.move_direction.x += character_parameter.direction.z / 10;
			character_parameter.move_direction.z -= character_parameter.direction.x / 10;
		}
	}

}
