using UnityEngine;
using System.Collections;

public class Bombchu_Bullet : Attack_Parameter {

    private Vector3 move_direction;//動いてる方向
    //坂判定用
    private RaycastHit slideHit;
    public float slideSpeed = 1.5f;//滑るスピード
    private bool isSliding = false;//滑ってるかどうか
    private bool isAir = false;//空中にいる
    public float slopelimit = 10;//壁と判断する角度
    

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        move_direction = Vector3.zero;//リセット
        //坂に立ってたら滑らす
        if (isSliding)
        {//滑るフラグが立ってたら
            Vector3 hitNormal = slideHit.normal;
            move_direction.x = -hitNormal.x * slideSpeed;
            move_direction.y = hitNormal.y * slideSpeed;
            move_direction.z = -hitNormal.z * slideSpeed;
            isSliding = false;//ここでリセットしとく
        }

        if (isAir)//空中にいたら無理やり設置させる
        {
            move_direction = Vector3.zero;
            move_direction.y = -8;
        }

        transform.position += move_direction * Time.deltaTime;
        Debug.Log(move_direction);
        isAir = true;

    }

    void OnTriggerEnter()
    {
        isAir = false;
        isSliding = true;
        
    }

    void OnTriggerStay()
    {
        //キャラクターの位置から下方向にRayを飛ばす（指定レイヤー限定※この場合は地面コリジョンのレイヤー）
        //RayLengthは、Rayを飛ばす距離。私の場合は地面の位置すれすれまで飛ばしてます（地面の高さは固定な前提）
        //レイヤーマスクは⇒で指定 int layerMask =1 << LayerMask.NameToLayer("レイヤー名");
        if (Physics.Raycast(transform.position, Vector3.down, out slideHit, 50))
        {

            //衝突した際の面の角度とが滑らせたい角度以上かどうかを調べます。
            if (Vector3.Angle(slideHit.normal, Vector3.up) > slopelimit)
            {
                isSliding = true;
            }
            else
            {
                //isSliding = false;//ここでリセットしとく
            }
        }
    }
}
