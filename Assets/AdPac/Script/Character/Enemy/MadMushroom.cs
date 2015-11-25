using UnityEngine;
using System.Collections;

public class MadMushroom : MonoBehaviour {

	public GameObject Bullet;//こいつが飛ばす球
	public Transform Muzzle;//発射口

	private Enemy_ControllerZ ecZ;
	private Attack_Parameter at_para;
    private Animator animator;

	private Transform Player;//操作キャラ

	// Use this for initialization
	void Start () {

        ecZ = GetComponent<Enemy_ControllerZ>();
        at_para = Bullet.GetComponent<Attack_Parameter>();
        animator = GetComponentInChildren<Animator>();

		Player = GameObject.FindWithTag ("Player").transform;

	}

    // Update is called once per frame
    void Update() {

/*
        if (ecZ.state == Enemy_Parameter.Enemy_State.Attack) {

            //前を向ける
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.transform.position - transform.position), 0.05f);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            //transform.LookAt(Player.transform.position);
            if (ecZ.GetF_Magic())
            {
                
                ecZ.Reverse_Magic();
                StartCoroutine(Attack());
                animator.SetTrigger("Grun");

            }
        }*/
    }

    IEnumerator Attack()
    {
        GameObject bullet;

        bullet = GameObject.Instantiate(Bullet);
        bullet.GetComponent<Attack_Parameter>().Parent = this.gameObject;//誰が撃ったかを渡す


        //弾を飛ばす処理
        bullet.transform.position = Muzzle.position + (ecZ.direction);
        bullet.GetComponent<Rigidbody>().velocity = ((Player.transform.position - this.transform.position).normalized * at_para.speed);

        /*if(!audioSource.isPlaying){
			
			audioSource.Play();
			
		}*/

        Destroy(bullet, at_para.GetA_Time());

        yield return new WaitForSeconds(at_para.GetR_Time());

        ecZ.Reverse_Magic();
        //enemy_flag.state = Enemy_Flag.Enemy_State.Attack;
    }
}
