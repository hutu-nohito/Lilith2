using UnityEngine;
using System.Collections;

public class Homing : Magic_Parameter {

    public GameObject Bullet;//弾のObject
    private Z_Camera z_Camera;//注目用
    private Magic_Controller MC;
    private Player_ControllerZ pcZ;


    // Use this for initialization
    void Start()
    {
        z_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Z_Camera>();
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
        GameObject bullet;

        bullet = GameObject.Instantiate(Bullet);
        MC.AddExistBullet(bullet);//現在の弾数を増やす
        bullet.GetComponent<Homing_Bullet>().Parent = this.Parent;//もらった親を渡しておく必要がある

        Parent.GetComponent<Player_ControllerZ>().SetMP(Parent.GetComponent<Player_ControllerZ>().GetMP() - GetSMP());//MPを減らす

        Vector3 Target;

        //注目中なら注目対象、注目してなかったらマウスの位置
        if (pcZ.GetF_Watch())
        {
            Target = z_Camera.Target.transform.position;
            bullet.GetComponent<Homing_Bullet>().TargetSet(z_Camera.Target);//撃った時に注目していたらホーミングする
        }
        else
        {
            Target = Parent.GetComponent<MousePoint>().worldPoint;
        }
        //弾を飛ばす処理
        bullet.transform.position = transform.position + Parent.GetComponent<Character_Parameters>().direction;
        bullet.transform.rotation = Quaternion.LookRotation(-(Target - Parent.transform.position).normalized);//回転させて矢じりを進行方向に向ける
        bullet.GetComponent<Rigidbody>().velocity = ((Target - Parent.transform.position).normalized * bullet.GetComponent<Homing_Bullet>().speed);
        /*if(!audioSource.isPlaying){
			
            audioSource.Play();
			
        }*/

        Destroy(bullet, bullet.GetComponent<Attack_Parameter>().GetA_Time());

        yield return new WaitForSeconds(bullet.GetComponent<Attack_Parameter>().GetR_Time());//撃った後の硬直

        //硬直を解除
        Parent.GetComponent<Character_Manager>().SetActive();

    }    
}
