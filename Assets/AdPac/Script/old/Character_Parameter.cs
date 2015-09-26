using UnityEngine;
using System.Collections;

public class Character_Parameter : MonoBehaviour {

	//キャラのパラメタ設定とキャラクターの情報

	//キャラクタのパラメタ設定(初期値)//////////////////////////////////////////////////////////
	public string CharaName = "Lilith";//キャラの種族名
	public int max_c_point = 3;//コンセントレートポイントの最大値
	public int max_mag_point = 10;//マジックポイントの最大値
	public float speed = 5; //キャラクタの移動速度
	public float jump_power = 6;//キャラクタがジャンプする高さ

	public string[] weak_element;//弱点属性
	public string[] proof_element;//耐性属性

	//public Vector3 adjust = new Vector3(0.0f,0.5f,0.0f);//モデルの位置調整

	//キャラクタ情報(現在値)/////////////////////////////////////////////////////////////////
	public Vector3 move_direction = Vector3.zero;//キャラクタの移動方向
	public Vector3 direction = Vector3.zero;//キャラクタが向いている方向

	public int now_c_point = 3;//今のコンセントレートポイント
	public int now_mag_point = 10;//今のマジックポイント
	public float now_speed = 5; //今のキャラクタの移動速度
	public float now_jump_power = 6;//今のキャラクタがジャンプする高さ

	//private Transform Parent;//キャラの親
	//public GameObject Model;//キャラのModel

	void Start (){
		
		now_c_point = max_c_point;
		now_mag_point = max_mag_point;
		now_speed = speed;
		now_jump_power = jump_power;

	}

}
