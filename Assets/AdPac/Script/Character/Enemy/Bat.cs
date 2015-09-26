using UnityEngine;
using System.Collections;

public class Bat : MonoBehaviour {

    public GameObject Bullet;//こいつが飛ばす球

    private Enemy_ControllerZ ecZ;
    private MoveSmooth MoveS;//動かすよう
    private Attack_Parameter at_para;
    private Animator animator;

    private Transform Player;//操作キャラ

    public Transform HomePos;

    // Use this for initialization
    void Start()
    {

        ecZ = GetComponent<Enemy_ControllerZ>();
        at_para = Bullet.GetComponent<Attack_Parameter>();
        MoveS = GetComponent<MoveSmooth>();
        animator = GetComponentInChildren<Animator>();

        Player = GameObject.FindWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update () {

        if (ecZ.state == Enemy_Parameter.Enemy_State.Attack)
        {

            //前を向ける
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.transform.position - transform.position), 0.05f);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            if (ecZ.GetF_Magic())
            {

                ecZ.Reverse_Magic();
                StartCoroutine(Attack());

            }
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1);

        GameObject bullet;

        bullet = GameObject.Instantiate(Bullet);
        bullet.GetComponent<Attack_Parameter>().Parent = this.gameObject;//誰が撃ったかを渡す
        bullet.transform.parent = this.transform;

        //弾を飛ばす処理
        bullet.transform.position = transform.position;

        MoveS.Move(Player.position - HomePos.position);//これで移動

        yield return new WaitForSeconds(1);

        MoveS.Move(Vector3.zero);//これで移動

        /*if(!audioSource.isPlaying){
			
			audioSource.Play();
			
		}*/

        Destroy(bullet, at_para.GetA_Time());

        yield return new WaitForSeconds(at_para.GetR_Time());

        ecZ.Reverse_Magic();
        ecZ.SetState(Enemy_Parameter.Enemy_State.Search);

    }
}
