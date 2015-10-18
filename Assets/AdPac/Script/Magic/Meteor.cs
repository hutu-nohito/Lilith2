using UnityEngine;
using System.Collections;

public class Meteor : Magic_Parameter {
    public GameObject bullet_Prefab;//弾のプレハブ

    private Magic_Controller MC;
    private Player_ControllerZ pcZ;

    private int num_meteor;//出せるメテオの数

    // Use this for initialization
    void Start()
    {
        MC = GameObject.FindGameObjectWithTag("Player").GetComponent<Magic_Controller>();
        pcZ = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_ControllerZ>();
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

        //体力に応じて出せるメテオ数を変える
        if (pcZ.GetHP() > pcZ.max_HP / 2)//体力が半分以上
            num_meteor = 10;
        if (pcZ.GetHP() > pcZ.max_HP / 4)//4分の1以上
            num_meteor = 7;
        else//瀕死
            num_meteor = 3;

        for (int i = 0; i < num_meteor; i++)
        {

            bullet[i] = GameObject.Instantiate(bullet_Prefab);//弾生成

            if (bullet[i] != null)
            {

                MC.AddExistBullet(bullet[i]);//現在の弾数を増やす
                bullet[i].GetComponent<Attack_Parameter>().Parent = this.Parent;//もらった親を渡しておく必要がある

                bullet[i].transform.position = transform.position + Parent.transform.TransformDirection(new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5)));//ランダム
                bullet[i].transform.rotation = Quaternion.LookRotation(Parent.transform.TransformDirection(Vector3.forward));//回転させて弾頭を進行方向に向ける
                bullet[i].GetComponent<Rigidbody>().velocity = ((Parent.transform.TransformDirection(new Vector3(0, -4, 2))) * bullet_Prefab.GetComponent<Attack_Parameter>().speed);

                Destroy(bullet[i], bullet[0].GetComponent<Attack_Parameter>().GetA_Time());

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
