using UnityEngine;
using System.Collections;

public class Ememy_Float : MonoBehaviour {

    public GameObject Bullet;//こいつが飛ばす球
    public Transform Muzzle;//発射口

    private Enemy_ControllerZ ecZ;
    private MoveSmooth MS;
    private Attack_Parameter at_para;
    private Animator animator;

    private Transform Player;//操作キャラ

    // Use this for initialization
    void Start()
    {

        ecZ = GetComponent<Enemy_ControllerZ>();
        MS = GetComponent<MoveSmooth>();
        at_para = Bullet.GetComponent<Attack_Parameter>();
        animator = GetComponentInChildren<Animator>();

        Player = GameObject.FindWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {


        if (ecZ.state == Enemy_Parameter.Enemy_State.Attack)
        {

            //前を向ける
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.transform.position - transform.position), 0.05f);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            //transform.LookAt(Player.transform.position);

            //プレイヤと一定の距離を保つ
            if((Player.transform.position - transform.position).magnitude < 15)//距離が15以下だったら
            {
                Vector3 EndPos = new Vector3((transform.position.x - Player.transform.position.x) * ecZ.GetSpeed(),transform.position.y, (transform.position.z - Player.transform.position.z) * ecZ.GetSpeed());
                MS.Move(EndPos, ecZ.GetSpeed());
            }

            if (ecZ.GetF_Magic())
            {

                ecZ.Reverse_Magic();
                StartCoroutine(Attack());
                //Vector3 moveEnd = new Vector3(Player.transform.position.x - transform.position.x * ecZ.GetSpeed(),0,Player.transform.position.z - transform.position.z * ecZ.GetSpeed());
                //MS.Move(Vector3.down,ecZ.GetSpeed());//プレイヤーのほうにちょっと進む
                //animator.SetTrigger("Grun");

            }
        }
    }

    IEnumerator Attack()
    {

        yield return new WaitForSeconds(1);//ちょっと間をおいてから攻撃

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
