﻿using UnityEngine;
using System.Collections;

public class Arrow : Magic_Parameter {

    private Magic_Controller MC;
    private Player_ControllerZ pcZ;

    public GameObject Bullet;//矢のObject

    private float time = 0;//矢を飛ばすまでの時間

	// Use this for initialization
	void Start () {
        MC = GameObject.FindGameObjectWithTag("Player").GetComponent<Magic_Controller>();
        pcZ = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_ControllerZ>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void Hold()
    {
        time += Time.deltaTime;
    }

    void Fire()
    {
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        Parent.GetComponent<Character_Manager>().SetKeylock();
        GameObject bullet;

        bullet = GameObject.Instantiate(Bullet);
        MC.AddExistBullet();//現在の弾数を増やす
        bullet.GetComponent<Attack_Parameter>().Parent = this.Parent;//もらった親を渡しておく必要がある

        pcZ.SetMP(pcZ.GetMP() - GetSMP());

        if (time >= 2.0f) { time = 2.0f; }//1秒までスピードに反映

        //弾を飛ばす処理
        bullet.transform.position = transform.position + Parent.GetComponent<Character_Parameters>().direction;
        bullet.transform.rotation = Quaternion.LookRotation(-(Parent.GetComponent<MousePoint>().worldPoint - Parent.transform.position).normalized);//回転させて矢じりを進行方向に向ける
        bullet.GetComponent<Rigidbody>().velocity = ((Parent.GetComponent<MousePoint>().worldPoint - Parent.transform.position).normalized * Bullet.GetComponent<Attack_Parameter>().speed * (time * time));
        /*if(!audioSource.isPlaying){
			
            audioSource.Play();
			
        }*/

        Destroy(bullet, bullet.GetComponent<Attack_Parameter>().GetA_Time());
        Invoke("subbullet", bullet.GetComponent<Attack_Parameter>().GetA_Time());

        yield return new WaitForSeconds(bullet.GetComponent<Attack_Parameter>().GetR_Time());//撃った後の硬直

        //硬直を解除
        Parent.GetComponent<Character_Manager>().SetActive();
        time = 0;

    }

    void subbullet()
    {
        MC.SubExistBullet();
    }

}
