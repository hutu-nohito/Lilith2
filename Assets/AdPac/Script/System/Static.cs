using UnityEngine;
using System.Collections;

public class Static : MonoBehaviour {

	//使いまわすパラメタ//

	public float day = 1;//何日目か
    public float GetDay() { return day; }
    public void SetDay(float day) { this.day += day; }

	public int count_Start = 0;//何回起動したか
    public int GetCountStart() { return count_Start; }
    public void SetCountStart() { count_Start++; }

    //HPとCPはクエストが終わっても引き継ぎ
    public int H_Point = 10;
    public int GetHP() { return H_Point; }
    public void SetHP(int HP) { this.H_Point = HP; }
    public int M_Point = 10;
    public int GetMP() { return M_Point; }
    public void SetMP(int MP) { this.M_Point = MP; }

    public int lank_P = 0;//名声
    public int GetLP() { return lank_P; }
    public void SetLP(int LP) { this.lank_P = LP; }

    public int bonus_P = 0;//名声
    public int GetBP() { return bonus_P; }
    public void SetBP(int BP) { this.bonus_P = BP; }

    /*
     * ここに隙間の状態と持ってるデータの情報 
     */

    private static bool flag_exist = false;
    void Awake()
    {

        if(!flag_exist){

            DontDestroyOnLoad(this.gameObject);
            flag_exist = true;

        }
        else
        {
            Destroy(this.gameObject);
        }

    }
	// Use this for initialization
	void Start () {

        		

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
