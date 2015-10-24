using UnityEngine;
using System.Collections;

public class Bombchu_Bullet : Attack_Parameter {

    private Vector3 move_direction;//動いてる方向
    //坂判定用
    private RaycastHit slideHit;//下のレイ
    private RaycastHit forwardHit;//前方のレイ
    public float slideSpeed = 1.5f;//滑るスピード
    private bool isSliding = false;//滑ってるかどうか
    private bool isAir = false;//空中にいる
    public float slopelimit = 10;//壁と判断する角度

    private Vector3 oldPos;

    void Start () {

        oldPos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

        //transform.rotation = Quaternion.LookRotation(transform.position - oldPos);//回転させて進行方向に向ける

        //これでスライドヒットがとれるはず
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out slideHit, 10))
        {

        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out forwardHit, 1))
        {

            //前方に壁があったらHitObjectを入れ替える
            if (forwardHit.collider.gameObject != null)
            {
                slideHit = forwardHit;
            }

        }

        Debug.Log(slideHit);

        move_direction = new Vector3(0,move_direction.y,0);//リセット

        //これで移動
        Vector3 hitNormal = slideHit.normal;
        move_direction.x = -hitNormal.x * slideSpeed;
        move_direction.y = hitNormal.y * slideSpeed;
        move_direction.z = -hitNormal.z * slideSpeed;

        /*if (isAir)//空中にいたら無理やり設置させる
        {
            move_direction = Vector3.zero;
            move_direction.y = -10;
        }*/

        transform.position += transform.TransformDirection(move_direction * Time.deltaTime);
        
        Debug.Log(move_direction);
        isAir = true;
        oldPos = transform.position;

    }

    void OnTriggerStay()
    {
        isAir = false;

    }
}
