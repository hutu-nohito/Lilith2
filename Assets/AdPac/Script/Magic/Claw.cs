using UnityEngine;
using System.Collections;

public class Claw : Magic_Parameter {
    public GameObject bullet_Prefab;//弾のプレハブ

    private GameObject Player;
    private Magic_Controller MC;
    private Player_ControllerZ pcZ;

    //近接ホーミング用
    private Vision vision;
    private bool flag_Homing;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        MC = Player.GetComponent<Magic_Controller>();
        pcZ = Player.GetComponent<Player_ControllerZ>();
        vision = Player.GetComponentInChildren<Vision>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flag_Homing)
        {
            if(vision.nearTarget != null)
            {
                Player.transform.rotation = Quaternion.Slerp(Player.transform.rotation, Quaternion.LookRotation(vision.nearTarget.transform.position - Player.transform.position), 0.5f);//Playerをターゲットのほうにゆっくり向ける
                Player.transform.rotation = Quaternion.Euler(0, Player.transform.eulerAngles.y, 0);//Playerのx,zの回転を直す。回転嫌い。全部Eulerにしてしまえばよい
            }
        }
    }

    void Fire()
    {
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        Parent.GetComponent<Character_Manager>().SetKeylock();
        GameObject[] bullet = new GameObject[GetExNum()];

        //プレイヤを敵のほうに向けてホーミングしてるように見せる
        if (vision.nearTarget != null)
        {
            flag_Homing = true;
        }

        yield return new WaitForSeconds(0.2f);//振り向くまで待つ

        for (int i = 0; i < 2; i++)
        {

            bullet[i] = GameObject.Instantiate(bullet_Prefab);//弾生成


            if (bullet[i] != null)
            {

                MC.AddExistBullet(bullet[i]);//現在の弾数を増やす
                bullet[i].GetComponent<Attack_Parameter>().Parent = this.Parent;//もらった親を渡しておく必要がある

                bullet[i].transform.position = transform.position + Parent.GetComponent<Character_Parameters>().direction + Parent.transform.TransformDirection(new Vector3(-0.5f + i, 0, 0));//
                bullet[i].transform.rotation = Quaternion.LookRotation(Parent.GetComponent<Character_Parameters>().direction);//回転させて弾頭を進行方向に向ける

                Destroy(bullet[i], bullet[0].GetComponent<Attack_Parameter>().GetA_Time());

            }
            
        }

        //MPの処理
        pcZ.SetMP(pcZ.GetMP() - GetSMP());

        yield return new WaitForSeconds(0.3f);//飛ばすまでに「溜め」がある

        //ずっと発動してるのでおろしとく
        if (flag_Homing)
        {
            flag_Homing = false;
        }

        for (int i = 0; i < 2; i++)
        {

            if (bullet[i] != null)
            {

                //斜めに振り下ろす
                bullet[i].GetComponent<Rigidbody>().velocity = (Parent.transform.TransformDirection(new Vector3(0.5f - i, -1, 0.5f)) * bullet[i].GetComponent<Attack_Parameter>().speed);

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
