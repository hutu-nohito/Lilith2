using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SukimaCanvas : MonoBehaviour {

    //こっちでシステム全体の管理
    //魔石運びもこっち

    public Button[][] Waku;
    public GameObject[] Maseki_Button;//魔石置き場
    public int Kind_Maseki = 0;//持ってる魔石の種類
    public int[] MasekiNum;//staticから拾ってくる
    public Vector3[] Maseki_ButtonPos = new Vector3[4];//動いちゃうから保持
    private GameObject Maseki;//インスタンス用
    private GameObject Manager;

    // Use this for initialization
    void Start () {

        Manager = GameObject.FindGameObjectWithTag("Manager");

        for (int i = 0; i < MasekiNum.Length; i++)
        {
            MasekiNum[i] = Manager.GetComponent<Static>().MasekiNum[i];
        }
            
        for(int i = 0; i < Maseki_Button.Length; i++)
        {
            Maseki_ButtonPos[i] = Maseki_Button[i].transform.position;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
        print("MouseOver!");
    }

    //魔石がドラッグされてるとき
    public void PointHi()
    {
        Maseki = Instantiate(Maseki_Button[2]);
        Maseki.transform.position = Maseki_ButtonPos[2];
        Maseki.transform.parent = transform;
        Kind_Maseki = 2;
    }
    
}
