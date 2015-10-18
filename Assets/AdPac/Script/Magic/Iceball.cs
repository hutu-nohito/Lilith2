using UnityEngine;
using System.Collections;

public class Iceball : Magic_Parameter {

    private Magic_Controller MC;
    private Player_ControllerZ pcZ;

    public GameObject Bullet;//雪玉のObject

    private GameObject bullet;
    private GameObject Player;
    private Vector3 oldPos;//プレイヤーの動きをとる

    private bool isIceball = false;//弾を出しているかどうか
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

    void Hold()
    {
        //まず雪玉を出します
        if (!isIceball) {
            bullet = GameObject.Instantiate(Bullet);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.LookRotation(Parent.transform.TransformDirection(Vector3.forward));//回転させて矢じりを進行方向に向ける

            bullet.GetComponent<Attack_Parameter>().Parent = this.Parent;//もらった親を渡しておく必要がある

            pcZ.SetMP(pcZ.GetMP() - GetSMP());

            isIceball = true;
            oldPos = Player.transform.position;
        }

        //雪玉を大きく
        if(oldPos != Player.transform.position)
        {
            if (bullet.transform.localScale.y < 5)//一応制限しとく
                bullet.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);

            //転がってるように見せる　回転
            bullet.transform.Rotate(1, 0, 0);
        }

        //雪玉がついてくるように さすがに親子にしたほうが早い
        //bullet.transform.position += Player.transform.position - oldPos;
        bullet.transform.parent = Player.transform;

        

        oldPos = Player.transform.position;

    }

    void Fire()
    {
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        Parent.GetComponent<Character_Manager>().SetKeylock();
        GameObject bullet_Shot;
        bullet.transform.parent = null;
        bullet_Shot = bullet;
        bullet = null;
        isIceball = false;

        MC.AddExistBullet(bullet_Shot);//現在の弾数を増やす

        //弾を飛ばす処理
        bullet_Shot.GetComponent<Rigidbody>().velocity = (Parent.transform.TransformDirection(Vector3.forward) * Bullet.GetComponent<Attack_Parameter>().speed);
        /*if(!audioSource.isPlaying){
			
            audioSource.Play();
			
        }*/

        Destroy(bullet_Shot, bullet_Shot.GetComponent<Attack_Parameter>().GetA_Time());

        yield return new WaitForSeconds(bullet_Shot.GetComponent<Attack_Parameter>().GetR_Time());//撃った後の硬直

        //硬直を解除
        Parent.GetComponent<Character_Manager>().SetActive();
    }

}
