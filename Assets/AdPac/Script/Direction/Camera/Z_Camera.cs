using UnityEngine;
using System.Collections;

public class Z_Camera : MonoBehaviour {

    public float RotSpeed = 20;//注目時の回転移動速度の調整用
    public float tiltAngle = 2;//カメラティルト調整用の角度

    public float length;//注目してる敵との距離
	public GameObject Target;//ここに注目対象を格納すればいい

	private GameObject Player;
	private Player_ControllerZ pcZ;
    private Quaternion StartCamera;//カメラの初期角度

	
	// Use this for initialization
	void Start () {
		
		Player = GameObject.FindGameObjectWithTag("Player");
        pcZ = Player.GetComponent<Player_ControllerZ>();

        StartCamera = transform.localRotation;
		
	}

	//ちらちらしそうだったらLateUpdateに変更
	// Update is called once per frame
	void Update () {
        
        //とりあえず右クリックで注目
		if(Input.GetMouseButton(1)){
		
			Vector3 LocalPos = Vector3.zero;
			Vector3 cal = Vector3.zero;//計算用の箱
			float degreeTheta = 0;//アフィン変換用の角度

			if(pcZ.GetF_Move()){
				//回転角を指定してやる
				if(Input.GetKey(KeyCode.A)){
					
					//degreeTheta = -0.1f;
                    degreeTheta = -pcZ.GetSpeed() * RotSpeed / length  * Time.deltaTime;//動かすときはスペックで違いが出ないようにDeltatime
					
				}
				if(Input.GetKey(KeyCode.D)){
					
					//degreeTheta = 0.1f;
                    degreeTheta = pcZ.GetSpeed() * RotSpeed / length * Time.deltaTime;//動かすときはスペックで違いが出ないようにDeltatime
					
				}
			}

            if (Target != null) { //ターゲットがいたら

                pcZ.Set_Watch();//注目しているよー

			    //アフィン変換　回転
			    LocalPos = Player.transform.position - Target.transform.position;//ターゲットに対するローカル座標に変換
			    //回転
			    cal.x = LocalPos.x * Mathf.Cos(degreeTheta * Mathf.Deg2Rad) - LocalPos.z * Mathf.Sin(degreeTheta * Mathf.Deg2Rad);
			    cal.z = LocalPos.x * Mathf.Sin(degreeTheta * Mathf.Deg2Rad) + LocalPos.z * Mathf.Cos(degreeTheta * Mathf.Deg2Rad);
			    LocalPos = cal;//座標に代入
			    LocalPos.y = Player.transform.position.y - Target.transform.position.y;//ジャンプさせるからYの値は変えない
			    Player.transform.position = LocalPos + Target.transform.position;//ワールド座標に直す

                //Playerの方向 = (最初の方向,向けたい方向,向けたい速度)
                Player.transform.rotation = Quaternion.Slerp(Player.transform.rotation, Quaternion.LookRotation(Target.transform.position - Player.transform.position), 0.1f);//Playerをターゲットのほうにゆっくり向ける
                Player.transform.rotation = Quaternion.Euler(0, Player.transform.eulerAngles.y, 0);//Playerのx,zの回転を直す。回転嫌い。全部Eulerにしてしまえばよい

			    //this.transform.LookAt((Player.transform.position + Target.transform.position) / 2);//Playerとターゲットの中心をPlayerの後ろから映して、どっちも画面内に収める

                //ターゲットとの高さによって回転角度を決めてる。tiltAngleで角度の調整
                //Vector3 LookRot = new Vector3((this.transform.position.y - Target.transform.position.y) * tiltAngle, 0, 0);//カメラの回転角度。X方向だけでいい。(ティルト)
               // this.transform.localEulerAngles = LookRot;
                //this.transform.localRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookRot) , 1.0f);//Cameraをターゲットのほうにゆっくり向ける

            }
		}
		
		if(Input.GetMouseButtonUp(1)){

            //ゆっくりにしてもあんま意味なかった
            //this.transform.rotation = Quaternion.Slerp(transform.rotation, StartCamera, 0.1f);//Cameraをターゲットのほうにゆっくり向ける
            this.transform.localRotation = StartCamera;//カメラを元に戻す

            pcZ.Release_Watch();//注目解除
            Target = null;//ターゲットを解放

		}
		
	}

    public void SetTarget(GameObject Target,float near)
    {

        if (Target == null) { 
            
            this.Target = Target;
            this.transform.localRotation = StartCamera;//カメラを元に戻す
            pcZ.Release_Watch();//注目解除
        
        }//nullのときは注目を外す
        else if (!pcZ.GetF_Watch()) { this.Target = Target; }//注目中はターゲットは変えない

        length = near;

    }
}
