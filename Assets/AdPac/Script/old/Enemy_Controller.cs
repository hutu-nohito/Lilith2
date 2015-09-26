using UnityEngine;
using System.Collections;

public class Enemy_Controller : MonoBehaviour {

	/*

	敵の操作用

	*/
	
	/*敵のタイプ
	 * 
	 * 基本Moveで徘徊してる
	 * その場で立たせておきたかったらMove_controllerのほうを0に
	 * 
	 * 敵発見時
	 * positive : 攻撃
	 * negative : 逃げる
	 * normal   : 何もしない
	*/

	//public enum Enemy_Type { Positive = 0, Normal = 1, Negative = 2 }
	//public Enemy_Type type = Enemy_Type.Normal;

	//使うもの
	private Character_Parameter character_parameter;//敵のパラメタ
	private Enemy_Flag enemy_flag;//敵の状態
	private Move_Controller move_controller;//周辺探索用
	private GameObject Player;//操作キャラ

	//public GameObject target ;//追うもの
	NavMeshAgent agent;//navi用

	// Use this for initialization
	void Start () {
	
		character_parameter = GetComponent< Character_Parameter >();//パラメタ取得
		enemy_flag = GetComponent< Enemy_Flag >();//状態取得
		agent = GetComponent<NavMeshAgent>();
		move_controller = GetComponent<Move_Controller>();
		Player = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {
		
		if(enemy_flag.flag_move){
			
			//agent.Move(move_controller.End * Time.deltaTime * character_parameter.now_speed);
			agent.destination = move_controller.End;//これで移動
			
		}

		//逃げ
		if(enemy_flag.state == Enemy_Flag.Enemy_State.Run){

			Vector3 follow = (Player.transform.position - this.transform.position).normalized;
			follow.y = 0.0f;
			agent.Move(-follow * Time.deltaTime * character_parameter.now_speed);//これで移動
			transform.rotation = Quaternion.LookRotation(-follow);
			
		}
		
		character_parameter.direction = transform.TransformDirection(Vector3.forward);//移動方向を格納

	}

}
