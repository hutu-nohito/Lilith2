using UnityEngine;
using System.Collections;

public class FatBat : MonoBehaviour {

    /*
        (Serch) → Player → (威嚇) → 距離10m以下 → (踊る) → HP減る → (攻撃) 
                                ↓                                           ↓
                            距離10m以上
                            　　↓
          ↑         ←      がっかり                                       ←

        */

    public GameObject Bullet;//攻撃
    public Transform Muzzle;//攻撃が出てくる場所

    private Enemy_ControllerZ ecZ;//状態の変更
    private MoveSmooth MS;//移動は全部これ
    private Attack_Parameter at_para;//攻撃の硬直など
    private Animator animator;

    private Transform Player;//操作キャラ
    public Transform Territory;//縄張り

    private int priority = 0;//状態の優先度
    /*
    Attack  1
    Damage  2
    Ikaku
    Dance
    Syobon
    Return  3
    Search  4
    Run     5
    */

    //キャラクタ固有の状態
    //アニメーションは基本ここで管理するのでこれを使うときは向こうはIdleにでもしとく
    public enum Fatbat_State
    {
        Idle,//使わないとき
        Ikaku,//威嚇
        Dance,//踊る
        Syobon,//がっかり
    }
    public Fatbat_State state = Fatbat_State.Idle;
    private Fatbat_State old_state = Fatbat_State.Idle;//ここの前の状態
    public Fatbat_State GetState() { return state; }
    public void SetState(Fatbat_State state) { this.state = state; }

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

            state = Fatbat_State.Ikaku;
            ecZ.SetState(Enemy_Parameter.Enemy_State.Idle);

        }

        if (ecZ.state == Enemy_Parameter.Enemy_State.Damage)
        {
            //前を向ける
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.transform.position - transform.position), 0.05f);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        }

        if (state == Fatbat_State.Ikaku)
        {
            //前を向ける
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.transform.position - transform.position), 0.05f);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

            //ecZ.SetState(Enemy_Parameter.Enemy_State.Idle);

            //プレイヤとの距離で行動変化
            if ((Player.transform.position - transform.position).magnitude < 5)//距離が5以下だったら
            {
                state = Fatbat_State.Dance;
            }
            if ((Player.transform.position - transform.position).magnitude > 10)//距離が10以上だったら
            {
                state = Fatbat_State.Syobon;
            }

        }

        if (state == Fatbat_State.Dance)
        {
            //前を向ける
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.transform.position - transform.position), 0.05f);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

            //ecZ.SetState(Enemy_Parameter.Enemy_State.Idle);

            //プレイヤとの距離で行動変化
            if ((Player.transform.position - transform.position).magnitude < 5)//距離が5以下だったら
            {
                state = Fatbat_State.Dance;
            }
            else if ((Player.transform.position - transform.position).magnitude < 10)
            {
                state = Fatbat_State.Ikaku;
            }

        }

        if (state == Fatbat_State.Syobon)
        {
            //前を向ける
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.transform.position - transform.position), 0.05f);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

            //ecZ.SetState(Enemy_Parameter.Enemy_State.Idle);

            //プレイヤとの距離で行動変化
            //プレイヤとの距離で行動変化
            if ((Player.transform.position - transform.position).magnitude < 10)
            {
                state = Fatbat_State.Ikaku;
            }

        }

        if (ecZ.state == Enemy_Parameter.Enemy_State.Return)
        {
            //アニメーション
            if (ecZ.old_state == Enemy_Parameter.Enemy_State.Attack);
            ecZ.SetState(Enemy_Parameter.Enemy_State.Idle);

            StartCoroutine(Attack());

            //とりあえず中心へ(Territoryはワールド座標にしとく)
            MS.Move(Territory.position, ecZ.speed);

            //前を向ける
            transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(Territory.position - transform.localPosition), 0.05f);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

            
            //state = ecZ.old_state;//10秒くらいたったら元の状態に戻す
        }

        if (ecZ.state == Enemy_Parameter.Enemy_State.Search)
        {
            if (old_state != Fatbat_State.Idle)//固有の行動してたらサーチには戻らん
            {
                state = old_state;
            }
        }

        

        if (ecZ.state == Enemy_Parameter.Enemy_State.Run)
        {
            //とりあえずこの状態にはならない
        }

        //アニメーションはある程度まとめといたほうがいいかもしれない
        switch (state)
        {
            case Fatbat_State.Idle:
                break;
            case Fatbat_State.Ikaku:
                animator.SetBool("Ikaku",true);
                animator.SetBool("Dance", false);
                animator.SetBool("Syobon", false);
                break;
            case Fatbat_State.Dance:
                animator.SetBool("Dance", true);
                animator.SetBool("Ikaku", false);
                break;
            case Fatbat_State.Syobon:
                animator.SetBool("Syobon", true);
                animator.SetBool("Ikaku", false);
                break;
            default:
                break;
        }

        //前の状態をひろっとく
        if (old_state != state)
        {
            old_state = state;
        }
    }

    /////////////////////////////////////
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

    IEnumerator Ikaku()
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
