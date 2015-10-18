﻿using UnityEngine;
using System.Collections;

public class Option_Bullet : Attack_Parameter
{
    //ターゲットの周りを回転するようにしてるから勝手に追従もするらしい
    public float degreeTheta = 0;//アフィン変換用の角度

    public GameObject Target;//ここに注目対象を格納すればいい
    private GameObject Player;//

    // Use this for initialization
    void Start()
    {

        if (Target == null)
            Target = GameObject.FindGameObjectWithTag("Player");

        Player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 LocalPos = Vector3.zero;
        Vector3 cal = Vector3.zero;//計算用の箱

        //アフィン変換　回転
        LocalPos = transform.position - Target.transform.position;//ターゲットに対するローカル座標に変換
                                                                  //回転
        cal.x = LocalPos.x * Mathf.Cos(degreeTheta * Mathf.Deg2Rad) - LocalPos.z * Mathf.Sin(degreeTheta * Mathf.Deg2Rad);
        cal.z = LocalPos.x * Mathf.Sin(degreeTheta * Mathf.Deg2Rad) + LocalPos.z * Mathf.Cos(degreeTheta * Mathf.Deg2Rad);
        LocalPos = cal;//座標に代入
        LocalPos.y = transform.position.y - Target.transform.position.y;//ジャンプさせるからYの値は変えない
        transform.position = LocalPos + Target.transform.position;//ワールド座標に直す

    }

}
