using UnityEngine;
using System.Collections;

public class AirCutter : Magic_Parameter {
    public GameObject bullet_Prefab;//弾のプレハブ

    private GameObject Player;
    private Magic_Controller MC;
    private Player_ControllerZ pcZ;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        MC = Player.GetComponent<Magic_Controller>();
        pcZ = Player.GetComponent<Player_ControllerZ>();
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
        GameObject[] bullet = new GameObject[GetExNum()];

        for (int i = 0; i < 2; i++)
        {

            bullet[i] = GameObject.Instantiate(bullet_Prefab);//弾生成


            if (bullet[i] != null)
            {

                MC.AddExistBullet(bullet[i]);//現在の弾数を増やす
                bullet[i].GetComponent<Attack_Parameter>().Parent = this.Parent;//もらった親を渡しておく必要がある

                bullet[i].transform.position = transform.position + Parent.GetComponent<Character_Parameters>().direction + Parent.transform.TransformDirection(new Vector3(-0.5f + i, 0, 0));//

                Destroy(bullet[i], bullet[0].GetComponent<Attack_Parameter>().GetA_Time());

            }

        }

        //MPの処理
        pcZ.SetMP(pcZ.GetMP() - GetSMP());

        for (int i = 0; i < 2; i++)
        {

            if (bullet[i] != null)
            {

                //広げる
                bullet[i].GetComponent<Rigidbody>().velocity = (Parent.transform.TransformDirection(new Vector3(-0.5f + i, 0, 0.5f)) * bullet[i].GetComponent<Attack_Parameter>().speed);

            }

        }

        yield return new WaitForSeconds(1);//弾を曲げるまでの時間

        for (int i = 0; i < 2; i++)
        {

            if (bullet[i] != null)
            {

                //縮める
                bullet[i].GetComponent<Rigidbody>().velocity = (Parent.transform.TransformDirection(new Vector3(0.5f - i, 0, 0.5f)) * bullet[i].GetComponent<Attack_Parameter>().speed);

            }

        }

        //効果音と演出
        /*if(!audioSource.isPlaying){
			
            audioSource.Play();//SE
			
        }*/

        yield return new WaitForSeconds(bullet_Prefab.GetComponent<Attack_Parameter>().GetR_Time());//撃った後の硬直

        //硬直を解除
        Parent.GetComponent<Character_Manager>().SetActive();

    }

}
