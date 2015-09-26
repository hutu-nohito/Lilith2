using UnityEngine;
using System.Collections;

public class Player_Flag : MonoBehaviour {

	//キャラの状態
	
	//キャラクタの行動状態//////////////////////////////////////////////////////////
	public bool flag_shot = false;//攻撃状態
	public bool flag_knockback = false;//ノックバック状態

	public bool flag_air = false;//空中にいるかどうか
	public bool flag_airjump = false;//空中ジャンプしたかどうか

	public bool flag_snipe = false;//望遠してるかどうか

	public bool flag_key = false;//動けないかどうか
	public bool flag_action = true;//プレイヤーの移動ができるかどうか
	public bool flag_invincible = false;//無敵かどうか
		
	//キャラクタの状態異常(ailment)/////////////////////////////////////////////////////////////////

	public bool flag_poison = false;//毒状態

	//キャラクタの状態変化(conversion)/////////////////////////////////////////////////////////////////

	public bool flag_speedUp = false;//加速状態

}
