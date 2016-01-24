﻿using UnityEngine;
using System.Collections;

public class Icicle : Magic_Parameter {

    public GameObject bullet_Prefab;//弾のプレハブ

    private Magic_Controller MC;
    private Player_ControllerZ pcZ;
    private AudioSource SE;//音
    
    // Use this for initialization
    void Start()
    {
        MC = GameObject.FindGameObjectWithTag("Player").GetComponent<Magic_Controller>();
        pcZ = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_ControllerZ>();
        SE = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Fire()
    {
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        Parent.GetComponent<Character_Manager>().SetKeylock();
        //MPの処理
        pcZ.SetMP(pcZ.GetMP() - GetSMP());

        GameObject[] bullet = new GameObject[GetExNum()];

        //効果音と演出
        if (!SE.isPlaying)
        {

            SE.PlayOneShot(SE.clip);//SE

        }
        for (int i = 0; i < GetExNum(); i++)
        {

            bullet[i] = GameObject.Instantiate(bullet_Prefab);//弾生成

            if (bullet[i] != null)
            {

                MC.AddExistBullet(bullet[i]);//現在の弾数を増やす
                bullet[i].GetComponent<Attack_Parameter>().Parent = this.Parent;//もらった親を渡しておく必要がある

                bullet[i].transform.position = transform.position + Parent.transform.TransformDirection(new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)));//ランダム
                bullet[i].transform.rotation = Quaternion.LookRotation(Parent.transform.TransformDirection(Vector3.down));//回転させて弾頭を進行方向に向ける            

                Destroy(bullet[i], bullet_Prefab.GetComponent<Attack_Parameter>().GetA_Time());

            }

            yield return new WaitForSeconds(0.1f);//弾の生成間隔
        }

        for (int i = 0; i < GetExNum(); i++)
        {
            if (bullet[i] != null)
            {
                bullet[i].GetComponent<Rigidbody>().velocity = ((Parent.transform.TransformDirection(Vector3.down)) * bullet_Prefab.GetComponent<Attack_Parameter>().speed);
                yield return new WaitForSeconds(0.2f);//落とすまでの間
            }
        }

        yield return new WaitForSeconds(bullet_Prefab.GetComponent<Attack_Parameter>().GetR_Time());//撃った後の硬直

        //硬直を解除
        Parent.GetComponent<Character_Manager>().SetActive();

    }
}
