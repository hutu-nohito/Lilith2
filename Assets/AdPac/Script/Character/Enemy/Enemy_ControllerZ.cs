﻿using UnityEngine;
using System.Collections;

public class Enemy_ControllerZ : Enemy_Parameter
{

    /*

    敵の基本操作用

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
    private GameObject Player;//操作キャラ7
    private MoveSmooth MoveS;//動かすよう

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
        MoveS = GetComponent<MoveSmooth>();

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

            MoveS.Move(move_controller.End,speed);//これで移動
            //前を向ける
            transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(move_controller.End - transform.localPosition), 0.05f);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        }

        //逃げ
        if (state == Enemy_State.Run)
        {

            Vector3 follow = (Player.transform.localPosition - this.transform.localPosition).normalized;
            follow.y = 0.0f;
            MoveS.Move(-follow * speed,speed);//これで移動
            transform.localRotation = Quaternion.LookRotation(-follow);

        }

        if (state == Enemy_State.Return)
        {
            time += Time.deltaTime;

            MoveS.Move(Territory.position, speed);//とりあえず中心へ(Territoryはワールド座標にしとく)
            //前を向ける
            transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(Territory.position - transform.localPosition), 0.05f);
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            
            if(time > 10.0f)
            {
                state = old_state;//10秒くらいたったら元の状態に戻す
            }
        }

        direction = transform.TransformDirection(Vector3.forward);//移動方向を格納

    }

    //縄張りから外れた時に戻ってくるよう
    public void Return()
    {
        old_state = state;
        state = Enemy_State.Return;
    }

}
