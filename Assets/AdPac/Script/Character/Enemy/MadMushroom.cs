using UnityEngine;
using System.Collections;

public class MadMushroom : MonoBehaviour {

	public GameObject Bullet;//こいつが飛ばす球
	public Transform Muzzle;//発射口

	private Enemy_ControllerZ ecZ;
	private Attack_Parameter at_para;

	private Transform Player;//操作キャラ

	// Use this for initialization
	void Start () {

        ecZ = GetComponent<Enemy_ControllerZ>();
        at_para = Bullet.GetComponent<Attack_Parameter>();

		Player = GameObject.FindWithTag ("Player").transform;

	}
	
	// Update is called once per frame
	void Update () {

		
			if(ecZ.state == Enemy_Parameter.Enemy_State.Attack){

				transform.LookAt(Player.transform.position);
                if (ecZ.GetF_Magic())
                {
                    Invoke("Attack", at_para.rigid_time);
                    ecZ.Reverse_Magic();

                }				
			}
	}

	void Attack (){

		GameObject bullet;

        bullet = GameObject.Instantiate(Bullet);
		bullet.GetComponent<Attack_Parameter>().Parent = this.gameObject;//誰が撃ったかを渡す
		
		
		//弾を飛ばす処理
		bullet.transform.position = Muzzle.position + (ecZ.direction );
		bullet.GetComponent<Rigidbody>().velocity = ( (Player.transform.position - this.transform.position).normalized * at_para.speed );
		/*if(!audioSource.isPlaying){
			
			audioSource.Play();
			
		}*/
		//硬直
		Invoke ("UnLock",at_para.rigid_time);
		
		Destroy( bullet, at_para.attack_time );
		//enemy_flag.state = Enemy_Flag.Enemy_State.Attack;

	}

	void UnLock(){

        ecZ.Reverse_Magic();

	}
}
