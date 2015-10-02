using UnityEngine;
using System.Collections;

public class Rayfall : Magic_Parameter {

    public GameObject bullet_Prefab;//弾のプレハブ

    private Magic_Controller MC;
    private Player_ControllerZ pcZ;

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

        for (int i = 0;i < GetExNum();i++)
        {
            bullet[i] = GameObject.Instantiate(bullet_Prefab);//弾生成
            MC.AddExistBullet(bullet[i]);//現在の弾数を増やす
            bullet[i].GetComponent<Attack_Parameter>().Parent = this.Parent;//もらった親を渡しておく必要がある
                                                                            //弾を飛ばす処理
            bullet[i].transform.position = transform.position + Parent.GetComponent<Character_Parameters>().direction + new Vector3((float)System.Math.Pow(-1, i) * (2 - (int)(i / 2)),0, (float)System.Math.Pow(-1, i) * (2 - (int)(i / 2)));//前方に飛ばす
            bullet[i].transform.rotation = Quaternion.LookRotation(Parent.transform.TransformDirection(Vector3.up));//回転させて弾頭を進行方向に向ける
            bullet[i].GetComponent<Rigidbody>().velocity = (Parent.transform.TransformDirection(Vector3.up) * bullet[0].GetComponent<Attack_Parameter>().speed / 3);

            yield return new WaitForSeconds(0.1f);//撃った後の硬直

            Destroy(bullet[i], bullet[0].GetComponent<Attack_Parameter>().GetA_Time());
        }

        //効果音と演出
        /*if(!audioSource.isPlaying){
			
            audioSource.Play();//SE
			
        }*/

        yield return new WaitForSeconds(bullet[0].GetComponent<Attack_Parameter>().GetR_Time());//撃った後の硬直

        //硬直を解除
        Parent.GetComponent<Character_Manager>().SetActive();
        
        //yield return new WaitForSeconds(0.1f);//上で止まる

        for (int i = 0; i < GetExNum(); i++)
        {

            bullet[i].transform.rotation = Quaternion.LookRotation(Parent.transform.TransformDirection(Vector3.forward));//回転させて弾頭を進行方向に向ける
            bullet[i].GetComponent<Rigidbody>().velocity = Vector3.zero;

        }

        yield return new WaitForSeconds(1);//発射

        for (int i = 0; i < GetExNum(); i++)
        {

            bullet[i].GetComponent<Rigidbody>().velocity = ((bullet[i].transform.TransformDirection(Vector3.forward) - new Vector3(0, 0.5f, 0)) * bullet[0].GetComponent<Attack_Parameter>().speed);

        }

    }
}
