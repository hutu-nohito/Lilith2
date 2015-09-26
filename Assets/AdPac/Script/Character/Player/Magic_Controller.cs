using UnityEngine;
using System.Collections;

public class Magic_Controller : MonoBehaviour{

    /*

    魔法の選択
    魔法発動

    */

    //変数(ex:time)////////////////////////////////

    public int magic_num = 0;//選択している弾の種類

    public int[] existBullet;//現在存在しているバレット数
    public void AddExistBullet() { existBullet[magic_num]++; }//バレットを増やす
    public void SubExistBullet() { existBullet[magic_num]--; }//バレットを減らす


    //GameObject/////////////////////////////////////////////

    public GameObject[] Magic;//魔法の大本。PlayerのみこれをMuzzleとして使う
    private Player_ControllerZ Pz;

	//コルーチン
	private Coroutine coroutine;
	//private int count;//汎用のカウント用の箱(使い終わったら0に戻すこと)
	private bool isCoroutine = false;

    //使うもの

    private Magic_Parameter.InputType InputType;

    void Start()
    {

        Pz = GetComponent<Player_ControllerZ>();

		MagicSet ();

        for(int i = 0;i < Magic.Length;i++){

            Magic[i].GetComponent<Magic_Parameter>().SetParent(this.gameObject);//親はプレイヤー
            existBullet[i] = 0;//親はプレイヤー

        }
        
    }

    void MagicSet()
    {
        /*
         * 
         * Magic[0]にもらったIDと同じ魔法をInstanseして格納
         * Magic[0]をPlayerの子オブジェクトに
         * インスタンスしたものにPlayerが親だと伝える
         * 
         */
    }

    void Update()
    {

        MagicSelect();

        InputType = Magic[magic_num].GetComponent<Magic_Parameter>().inputtype;//選択してる弾のパラメタ読み込み

        if (Pz.GetF_Magic())//魔法が打てる状態かどうかを確認
        {
            if (Magic[magic_num].GetComponent<Magic_Parameter>().spend_MP <= Pz.M_point)//使うMP < 現MPだったら魔法が打てる
            {
                if(this.existBullet[magic_num] < Magic[magic_num].GetComponent<Magic_Parameter>().GetExNum())//いまだしてる弾の数 < 出せる弾の数
                MagicFire();
            }

        }

		coroutine = StartCoroutine (MPRecover ());

    }

    void MagicSelect()
    {
        /*
        //マウスホイール式
        if (Input.GetAxis("Mouse ScrollWheel") == 1.0f)
        {

            if (bullet_num == Bullet.Length - 1)
            {

                bullet_num = 0;

            }
            else
            {

                bullet_num += (int)Input.GetAxis("Mouse ScrollWheel");

            }

            bullet_parameter = null;//念のためパラメタリセット

        }

        if (Input.GetAxis("Mouse ScrollWheel") == -1.0f)
        {

            if (bullet_num == 0)
            {

                bullet_num = Bullet.Length - 1;

            }
            else
            {

                bullet_num += (int)Input.GetAxis("Mouse ScrollWheel");

            }

            bullet_parameter = null;//念のためパラメタリセット

        }
        */

        //ボタン式
        if(Input.GetKeyDown(KeyCode.Z)){

            if (magic_num == 0)
            {

                magic_num = Magic.Length - 1;
				
            }else{

                magic_num -= 1;
				
            }
			
        }

        if (Input.GetKeyDown(KeyCode.X))
        {

            if (magic_num == Magic.Length - 1)
            {

                magic_num = 0;
				
            }else{

                magic_num += 1;
				
            }
			
        }

    }

    void MagicFire()
    {

        switch (InputType)
        {
            case Magic_Parameter.InputType.GetDown:
                if (Input.GetButtonDown("Fire1"))
                {

                    Magic[magic_num].SendMessage("Fire");                 

                }
                break;
            case Magic_Parameter.InputType.Get:
                if (Input.GetButton("Fire1"))
                {

                    Magic[magic_num].SendMessage("Hold");//ボタンおしっぱのとき

                }
                if (Input.GetButtonUp("Fire1"))
                {
                    
                    Magic[magic_num].SendMessage("Fire");

                }
                break;
            default:
                if (Input.GetButtonDown("Fire1"))
                {
                    
                    Magic[magic_num].SendMessage("Fire");//魔法を制御しているScriptは1つ1つ名前が違うからしょうがない

                }
                break;
        }
    }

	IEnumerator MPRecover(){//MPは自然回復

		if(isCoroutine){yield break;}
		isCoroutine = true;
		
		yield return new WaitForSeconds(5.0f);//回復スピード
		
        if(Pz.M_point < Pz.max_MP){Pz.M_point += 1;}//最大MPを超えないようにする
		
		isCoroutine = false;

	}
}
