using UnityEngine;
using System.Collections;

public class Event_Manager : MonoBehaviour {

    //特別なイベントがある場合にこれが呼び出される(Manager)

	public TextAsset[] EventText;//ここにイベント用のテキストを格納

	public GameObject Canvas;//UI

    private Static save;//日数、起動回数、HP、MP、名声、ボーナスポイント

	//コルーチン
	private Coroutine coroutine;
	private int count;//汎用のカウント用の箱(使い終わったら0に戻すこと)
	private bool isCoroutineH = false;
	private bool isCoroutineG = false;

	// Use this for initialization
	void Start () {
	
        save = GetComponent<Static>();
		Canvas = GameObject.Find ("Canvas");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Home_T(){//
		
		if(isCoroutineH){yield break;}
		isCoroutineH = true;
		if (save.GetDay () == 1) {
			
			Canvas.GetComponent<uGUI_Msg>().dispMessage(EventText[0]);
			
		}
		isCoroutineH = false;
		
	}
	IEnumerator guild_T(){
		
		if(isCoroutineG){yield break;}
		isCoroutineG = true;
		if (save.GetDay () == 1) {
			
			Canvas.GetComponent<uGUI_Msg>().dispMessage(EventText[0]);
			
		}
		isCoroutineG = false;
		
	}

}
