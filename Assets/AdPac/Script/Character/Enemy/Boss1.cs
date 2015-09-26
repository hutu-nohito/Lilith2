using UnityEngine;
using System.Collections;

public class Boss1 : MonoBehaviour {
	public GameObject[] Bullet;//こいつが飛ばす球
	public Transform[] Muzzle;//発射口
	
	private Character_Parameter character_parameter;
	private Enemy_Flag enemy_flag;
	private Bullet_Parameter bullet_parameter;

	private Animator animator;

	private GameObject Player;//操作キャラ
	private int old_cp;//ダメージを受けたか判定用

	private Coroutine coroutine;//コルーチン用の箱
	private bool isTutacoroutine = false;//蔦してるかどうか
	private bool isBombcoroutine = false;//ボムしてるかどうか
	private bool isSporecoroutine = false;//胞子してるかどうか
	
	// Use this for initialization
	void Start () {
		
		character_parameter = GetComponent<Character_Parameter>();
		enemy_flag = GetComponent<Enemy_Flag>();
		animator = GetComponentInChildren< Animator >();
		
		Player = GameObject.FindWithTag ("Player");

		old_cp = character_parameter.now_c_point;
		
	}
	
	// Update is called once per frame
	void Update () {

		if(character_parameter.now_c_point != old_cp){

			StopCoroutine(Bomb ());
			StopCoroutine(Spore ());
			StartCoroutine(Tuta());
			enemy_flag.flag_key = true;

		}

		if(enemy_flag.state == Enemy_Flag.Enemy_State.Attack){

			float far = (this.transform.position - Player.transform.position).magnitude;

			transform.LookAt(Player.transform.position);

			if(!enemy_flag.flag_key){

				if(far > 40){

					coroutine = StartCoroutine(Bomb());

				}else{

					coroutine = StartCoroutine(Spore());

				}

				enemy_flag.flag_key = true;
				
			}
		}

		old_cp = character_parameter.now_c_point;
	}

	IEnumerator Spore (){

		if(isTutacoroutine){yield break;}
		if(isBombcoroutine){yield break;}
		if(isSporecoroutine){yield break;}
		isSporecoroutine = true;
		GameObject bullet;
		bullet = GameObject.Instantiate( Bullet[0] );
		bullet_parameter = bullet.GetComponent<Bullet_Parameter>();
		bullet_parameter.Parent = this.gameObject;//誰が撃ったかを渡す
		
		//弾を飛ばす処理
		animator.SetTrigger ("Gyon");

		bullet.transform.position = Muzzle[0].position;

		/*if(!audioSource.isPlaying){
			
			audioSource.Play();
			
		}*/

		//硬直
		yield return new WaitForSeconds(bullet_parameter.wait_time);//攻撃前の隙

		enemy_flag.flag_key = false;
		isSporecoroutine = false;

		//enemy_flag.state = Enemy_Flag.Enemy_State.Attack;
		
	}

	IEnumerator Bomb (){

		if(isTutacoroutine){yield break;}
		if(isBombcoroutine){yield break;}
		if(isSporecoroutine){yield break;}
		isBombcoroutine = true;
		GameObject bullet;
		bullet = GameObject.Instantiate( Bullet[1] );
		bullet_parameter = bullet.GetComponent<Bullet_Parameter>();
		bullet_parameter.Parent = this.gameObject;//誰が撃ったかを渡す
		
		//弾を飛ばす処理
		animator.SetTrigger ("Grun");

		bullet.transform.position = Muzzle[1].position;
		bullet.GetComponent<Rigidbody>().velocity = ( (Player.transform.position - this.transform.position).normalized * bullet_parameter.at_speed );
		
		/*if(!audioSource.isPlaying){
			
			audioSource.Play();
			
		}*/

		//硬直
		yield return new WaitForSeconds(bullet_parameter.wait_time);//攻撃前の隙
		
		enemy_flag.flag_key = false;
		isBombcoroutine = false;
		//enemy_flag.state = Enemy_Flag.Enemy_State.Attack;
		
	}

	IEnumerator Tuta (){

		isTutacoroutine = true;
		GameObject bullet;
		bullet = GameObject.Instantiate( Bullet[2] );
		bullet_parameter = bullet.GetComponentInChildren<Bullet_Parameter>();
		bullet_parameter.Parent = this.gameObject;//誰が撃ったかを渡す
		
		//弾を飛ばす処理
		animator.SetTrigger ("Grun");



		bullet.transform.position = Muzzle[1].position;
		bullet.GetComponent<Rigidbody>().velocity = ( (Player.transform.position - this.transform.position + new Vector3 (0,1,0)).normalized * 5 );


		
		/*if(!audioSource.isPlaying){
			
			audioSource.Play();
			
		}*/

		//硬直
		yield return new WaitForSeconds(2);//攻撃前の隙
		enemy_flag.flag_key = false;
		isTutacoroutine = false;
		
		//enemy_flag.state = Enemy_Flag.Enemy_State.Attack;
		
	}

}
