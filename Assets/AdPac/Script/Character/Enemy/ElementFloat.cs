﻿using UnityEngine;
using System.Collections;

public class ElementFloat : MonoBehaviour {

    /*


        (Serch) → Player → (Attack) → 距離10m以下 → 固有攻撃 　→  距離　→　yes → Serchへ
                               ↓                                20m以上
                           距離10m以上      →   ワープ回り込み →  Attackへ

    */

    //この辺は敵によりけりなのでこっち
    public GameObject[] Bullet;//攻撃
    public Transform[] Muzzle;//攻撃が出てくる場所

    public int Enemy_Level = 0;//この値によって攻撃方法を変える
    /*

        1:  Bubble
        2:  Crescent
        3:  Icicle

        */

    private Enemy_ControllerZ ecZ;
    private MoveSmooth MS;//移動は全部これ
    private Camera_ControllerZ CCZ;

    private Animator animator;//アニメーションはふよふよと攻撃モーションのみ
    private AudioSource[] SE;//音

    //汎用
    private float time = 0;//使ったら戻す
    private Coroutine coroutine;//一度に動かすコルーチンは1つ ここでとっとけば止めるのが楽
    private bool isCoroutine = false;//コルーチンを止めるときにはfalseに戻すこと


    private int priority = 0;//状態の優先度
    /*
    isReturn  1 こうしないとどっかにワープしてっちゃう

    Attack    2
    isDamage  3
    
    isFind    4
    Search    5
    */

    //キャラクタ固有の状態
    //アニメーションは基本ここで管理するのでこれを使うときは向こうはIdleにでもしとく
    public enum Element_State
    {
        Attack,//攻撃
        Search,//うろうろ
        Damage,
        Return,
    }
    public Element_State state = Element_State.Search;
    public Element_State GetState() { return state; }
    public void SetState(Element_State state) { this.state = state; }

    private Element_State oldstate = Element_State.Search;

    // Use this for initialization
    void Start()
    {

        ecZ = GetComponent<Enemy_ControllerZ>();
        MS = GetComponent<MoveSmooth>();
        animator = GetComponentInChildren<Animator>();
        //SE = GetComponent<AudioSource>();

        CCZ = Camera.main.gameObject.GetComponent<Camera_ControllerZ>();

        priority = 5;//最初はサーチに
    }

    // Update is called once per frame
    void Update()
    {
        if (ecZ.isReturn)
        {
            if (priority >= 1)
            {
                state = Element_State.Return;
                priority = 1;
            }

        }
        if (state == Element_State.Return)
        {

            if (priority >= 1)
            {
                priority = 1;
                //アニメーションセット
                //animator.SetTrigger("Walk");//歩き

                //とりあえず中心へ(Territoryはワールド座標にしとく)
                ecZ.Move(ecZ.Territory.position, ecZ.speed);

                //前を向ける
                transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(ecZ.Territory.localPosition - transform.localPosition), 0.05f);
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

                //テリトリとの距離で行動変化
                if ((ecZ.Territory.localPosition - transform.localPosition).magnitude < 10)//真ん中ら辺まで戻ったら
                {
                    state = Element_State.Search;
                    priority = 5;
                }

            }

        }

        if (state == Element_State.Attack)
        {
            priority = 2;

            //前を向ける
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(ecZ.Player.transform.position - transform.position), 0.05f);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

            ecZ.Stop();//こけた時まれに下方向に力が働くので一応止めとく
            coroutine = StartCoroutine(Attack());

        }

        if (ecZ.isDamage)
        {
            if (priority >= 3)
            {
                if (state != Element_State.Damage)
                {
                    state = Element_State.Damage;
                    priority = 3;
                    //前を向ける
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(ecZ.Player.transform.position - transform.position), 0.05f);
                    transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
                    //アニメーションセット
                    animator.SetTrigger("Damage");//ダメージ
                }
            }

        }
        if (state == Element_State.Damage)
        {

            if (priority >= 6)
            {

                priority = 3;

                //アニメーションセット
                //animator.SetTrigger("Kyorokyoro");//ここできょろきょろ                  

                //前を向ける
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(ecZ.Player.transform.position - transform.position), 0.05f);
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

            }
            else if (priority >= 3)
            {

                priority = 3;
                //アニメーションセット
                //animator.SetTrigger("Walk");//歩き

                //前を向ける
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(ecZ.Player.transform.position - transform.position), 0.05f);
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

                //逃げる
                ecZ.Move((transform.position - ecZ.Player.transform.position).normalized * 5, 1);//ﾌﾟﾚｲﾔから距離をとる
                coroutine = StartCoroutine(Run());
                ecZ.Stop();//こけた時まれに下方向に力が働くので一応止めとく
            }
        }



        if (ecZ.isFind)
        {
            if (priority >= 4)
            {
                state = Element_State.Attack;
            }
        }

        if (state == Element_State.Search)
        {
            if (priority >= 5)
            {
                priority = 5;
                //アニメーションセット
                //animator.SetTrigger("Walk");//歩き

                //前を向ける
                transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(ecZ.move_controller.End - transform.localPosition), 0.05f);
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

                MS.Move(ecZ.move_controller.End, ecZ.speed);

            }

        }

        //状態が変化したら前の状態のいどうは中断
        if (oldstate != state)
        {
            time = 0;
            MS.Stop();
            ecZ.Stop();
        }
        oldstate = state;

    }

    IEnumerator Run()
    {

        if (isCoroutine) yield break;
        isCoroutine = true;

        yield return new WaitForSeconds(1);//ちょっと間をおく

        state = Element_State.Search;//Search状態に戻す
        priority = 5;
        ecZ.Reverse_Magic();
        isCoroutine = false;

    }

    IEnumerator Attack()
    {

        if (isCoroutine) yield break;
        isCoroutine = true;
        ecZ.Reverse_Magic();

        yield return new WaitForSeconds(1);//ちょっと間をおいてから攻撃

        GameObject bullet;

        //アニメーションセット
        //animator.SetTrigger("Attack");//攻撃


        bullet = GameObject.Instantiate(Bullet[0]);//通常弾　flame
        bullet.GetComponent<Attack_Parameter>().Parent = this.gameObject;//誰が撃ったかを渡す
                                                                         //弾を飛ばす処理
        bullet.transform.position = Muzzle[0].position;//Muzzleの位置
        bullet.transform.rotation = Quaternion.LookRotation(ecZ.direction);//回転させて弾頭を進行方向に向ける

        bullet.GetComponent<Rigidbody>().velocity = (ecZ.Player.transform.position - transform.position).normalized * bullet.GetComponent<Attack_Parameter>().speed;//ﾌﾟﾚｲﾔに向けて撃つ

        Destroy(bullet, bullet.GetComponent<Attack_Parameter>().GetA_Time());

        yield return new WaitForSeconds(bullet.GetComponent<Attack_Parameter>().GetR_Time());//撃った後の硬直

        ecZ.Move((transform.position - ecZ.Player.transform.position).normalized * 50, 1);//ﾌﾟﾚｲﾔから距離をとる


        //Bomb
        if (Enemy_Level == 1)
        {
        }

        //Kanketusen
        if (Enemy_Level == 2)
        {

        }

        //Meteor
        if (Enemy_Level == 3)
        {

        }


        ecZ.Reverse_Magic();

        state = Element_State.Search;//Search状態に戻す
        priority = 5;


        isCoroutine = false;

    }
}
