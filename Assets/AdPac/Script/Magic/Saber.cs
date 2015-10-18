using UnityEngine;
using System.Collections;

public class Saber : Magic_Parameter {

    public GameObject bullet_Prefab;//弾のプレハブ

    private GameObject Player;
    private Magic_Controller MC;
    private Player_ControllerZ pcZ;

    //近接ホーミング用 ホーミングはないけど一応入れとく
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
        //ホーミングが必要になったら解放
        /*if (flag_Homing)
        {
            if (vision.nearTarget != null)
            {
                Player.transform.rotation = Quaternion.Slerp(Player.transform.rotation, Quaternion.LookRotation(vision.nearTarget.transform.position - Player.transform.position), 0.5f);//Playerをターゲットのほうにゆっくり向ける
                Player.transform.rotation = Quaternion.Euler(0, Player.transform.eulerAngles.y, 0);//Playerのx,zの回転を直す。回転嫌い。全部Eulerにしてしまえばよい
            }
        }*/
    }

    void Fire()
    {
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        Parent.GetComponent<Character_Manager>().SetKeylock();
        GameObject bullet;

        /*//プレイヤを敵のほうに向けてホーミングしてるように見せる
        if (vision.nearTarget != null)
        {
            flag_Homing = true;
        }*/

        //yield return new WaitForSeconds(0.2f);//振り向くまで待つ

        bullet = GameObject.Instantiate(bullet_Prefab);//弾生成

        //CPに応じて大きさ変化
        if (pcZ.GetMP() > pcZ.max_MP / 2)//体力半分以上
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x, bullet.transform.localScale.y, bullet.transform.localScale.z * 3);
        if (pcZ.GetMP() > pcZ.max_MP / 4)//体力4分の1以上
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x, bullet.transform.localScale.y, bullet.transform.localScale.z * 2);

        MC.AddExistBullet(bullet);//現在の弾数を増やす
        bullet.GetComponent<Attack_Parameter>().Parent = this.Parent;//もらった親を渡しておく必要がある
        bullet.transform.position = transform.position + Parent.GetComponent<Character_Parameters>().direction;//

        Destroy(bullet, bullet.GetComponent<Attack_Parameter>().GetA_Time());

        //MPの処理
        pcZ.SetMP(pcZ.GetMP() - GetSMP());

        yield return new WaitForSeconds(0.3f);//飛ばすまでに「溜め」がある

        /*//ずっと発動してるのでおろしとく
        if (flag_Homing)
        {
            flag_Homing = false;
        }*/

        //斜めに振り下ろす
        if(bullet != null)
        bullet.GetComponent<Rigidbody>().velocity = (Parent.transform.TransformDirection(new Vector3(2, -1, 0.5f)) * bullet.GetComponent<Attack_Parameter>().speed);

        //効果音と演出
        /*if(!audioSource.isPlaying){
			
            audioSource.Play();//SE
			
        }*/

        yield return new WaitForSeconds(bullet_Prefab.GetComponent<Attack_Parameter>().GetR_Time());//撃った後の硬直

        //硬直を解除
        Parent.GetComponent<Character_Manager>().SetActive();

    }

}
