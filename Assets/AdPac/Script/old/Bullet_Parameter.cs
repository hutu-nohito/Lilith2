using UnityEngine;
using System.Collections;

public class Bullet_Parameter : MonoBehaviour {

	//攻撃のパラメタ設定と弾（あたり判定）の情報
	
	//キャラクタのパラメタ設定//////////////////////////////////////////////////////////

	/*名称一覧
	 * Bullet	弾が出てくるタイプ
	 * Buff		自分を強化するやつ
	 * Air		二段ジャンプ・空中ダッシュ
	 * 
	*/
	public string at_name = "Bullet";//攻撃の名称
	public int at_power = 3;//アタックパワー(攻撃力)
	public int mag_power = 1;//マジックパワー(消費MP)
	public float at_speed = 5.0f; //弾速
	public float at_time = 5.0f; //判定の持続時間
	public float wait_time = 5.0f; //硬直時間

	public string at_element;//攻撃の属性
	public string at_property;//攻撃の特性

	public string at_ailment;//付加される状態異常
	
	//キャラクタ情報/////////////////////////////////////////////////////////////////
	public Vector3 move_direction = Vector3.zero;//攻撃方向
	
	public GameObject Parent;//攻撃した人

	void Start (){//持続時間で消える

		if(at_name == "Bullet"){

			Destroy(this.gameObject,at_time);

		}

	}

}
