using UnityEngine;
using System.Collections;

public class Enemy_Flag : MonoBehaviour {

	//キャラの状態
	
	//キャラクタの行動状態(State)//////////////////////////////////////////////////////////
	public enum Enemy_State{
		Idle = 0,//通常
		Search = 1,//索敵
		Attack = 2,//臨戦態勢
		Run = 3//逃げ
	}
	public Enemy_State state = Enemy_State.Idle;

	public bool flag_move = true;//移動を使ってるかどうか

	//硬直系//
	public bool flag_key = false;//動けないかどうか(硬直)
	public bool flag_invincible = false;//無敵かどうか

	public bool flag_knockback = false;//ノックバック状態

	//キャラクタの体調(Condition)/////////////////////////////////////////////////////////////////
	
	public bool flag_poison = false;//毒状態
	
	public bool flag_speedUp = false;//加速状態

	//キャラクタの習性(Habit)////////////////////////////////////////////////////////////

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

	public enum Enemy_Habit{
		Positive = 0,
		Normal = 1,
		Negative = 2
	}
	public Enemy_Habit habit = Enemy_Habit.Normal;

	/*索敵方法
	 * 
	 * Sight 視覚
	 * Audition 聴覚
	 * Flair 嗅覚
	 * Oscillo 振動
	 * Magic 魔法
	 * Thermo 温度
	 * 
	 */

	public enum Enemy_Search{
		Sight = 0,
		Audition = 1,
		Flair = 2,
		Oscillo = 3,
		Magic = 4,
		Thermo = 5,
	}
	public Enemy_Search search = Enemy_Search.Audition;
}
