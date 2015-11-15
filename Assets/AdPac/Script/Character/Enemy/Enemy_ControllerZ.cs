using UnityEngine;
using System.Collections;

public class Enemy_ControllerZ : Enemy_Parameter
{

    /*

    敵の基本操作用

        キャラクタの状態管理は全部ここでやる
        他ではやらない

    */

    /*敵のタイプ
     * 
     * 基本Moveで徘徊してる
     * その場で立たせておきたかったらMove_controllerのほうを0に
     * 
     * 敵発見時
     * positive : 攻撃
     * negative : 逃げる
     * normal   : 何もしない
    */

    //使うもの
    private Move_Controller move_controller;//周辺探索用
    private GameObject Player;//操作キャラ
    private NavMeshAgent Nav;//動かすよう地上
    private MoveSmooth MoveS;//動かすよう空中

    private SphereCollider groundCollider;//足元用のコライダ
    public Transform Territory;//縄張り

    //汎用
    private Enemy_State old_state;//一個前のをとっとくよう
    private float time = 0;//使ったら戻す

    //初期パラメタ(邪魔なのでインスペクタに表示しない)
    [System.NonSerialized]
    public int max_HP, max_MP, base_Pow, base_Def;
    [System.NonSerialized]
    public float base_Sp, base_Ju;

    // Use this for initialization
    void Start()
    {
        move_controller = GetComponent<Move_Controller>();
        Player = GameObject.FindGameObjectWithTag("Player");

        //移動方法によって動きを変える
        switch (move)
        {
            case Enemy_Move.Ground://×ナビで動く　○自力で
                MoveS = GetComponent<MoveSmooth>();
                break;
            case Enemy_Move.Float://自力で動かす
                MoveS = GetComponent<MoveSmooth>();
                break;
            case Enemy_Move.Stand://何もしない
                break;
            default:
                break;
        }

        //足元用
        groundCollider = GetComponent<SphereCollider>();

        //初期パラメタを保存
        max_HP = H_point;
        max_MP = M_point;
        base_Pow = power;
        base_Def = def;
        base_Sp = speed;
        base_Ju = jump;

    }

    // Update is called once per frame
    void Update()
    {
        //HPがなくなった時の処理
        if (H_point <= 0)
        {

            Destroy(this.gameObject);
            GameObject.FindGameObjectWithTag("Manager").GetComponent<QuestManager>().SetCount(CharaName);

        }        

        if (state == Enemy_State.Search)
        {
            //どっちかある方移動
            if (Nav != null)
            Nav.Move(move_controller.End);
            if(MoveS != null)
            MoveS.Move(move_controller.End,speed);

            //前を向ける
            transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(move_controller.End - transform.localPosition), 0.05f);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        }

        //逃げ
        if (state == Enemy_State.Run)
        {

            Vector3 follow = (Player.transform.localPosition - this.transform.localPosition).normalized;
            follow.y = 0.0f;

            //どっちかある方移動
            if (Nav != null)
                Nav.Move(-follow * speed);
            if (MoveS != null)
                MoveS.Move(-follow * speed, speed);

            transform.localRotation = Quaternion.LookRotation(-follow);

        }

        if (state == Enemy_State.Return)
        {
            time += Time.deltaTime;

            //とりあえず中心へ(Territoryはワールド座標にしとく)
            if (Nav != null)
                Nav.Move(Territory.position);
            if (MoveS != null)
                MoveS.Move(Territory.position, speed);

            //前を向ける
            transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(Territory.position - transform.localPosition), 0.05f);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            
            if(time > 10.0f)
            {
                state = old_state;//10秒くらいたったら元の状態に戻す
            }
        }

        //レイキャストで何とかして壁に当たらないようにする
        RaycastHit hit;
        Vector3 StartPos = transform.position + new Vector3(0,transform.localScale.y,0);//とりあえず頭から出す

        //これのとき前に壁がある
        if (Physics.Raycast(StartPos, transform.TransformDirection(Vector3.forward), out hit ,20))
        {
            //Debug.DrawLine(StartPos, hit.point, Color.green);

        }

        direction = transform.TransformDirection(Vector3.forward);//移動方向を格納

    }

    //状態管理//////////////////////////////////////////////////////////////////////////////

        public void Idle()
    {
        //なんか条件付けるけどとりあえずIdle状態に
        state = Enemy_State.Idle;

    }

    public void Attack()
    {

        //なんか条件付けるけどとりあえずAttack状態に
        state = Enemy_State.Attack;

    }

    public void Search ()
    {

        //なんか条件付けるけどとりあえずSearch状態に
        state = Enemy_State.Search;

    }

    public void Run ()
    {

        //なんか条件付けるけどとりあえずRun状態に
        state = Enemy_State.Run;

    }
    
    //こっちはトリガー
    public void Damage()
    {

    }

    //縄張りから外れた時に戻ってくるよう
    public void Return()
    {
        old_state = state;
        state = Enemy_State.Return;
    }

}
