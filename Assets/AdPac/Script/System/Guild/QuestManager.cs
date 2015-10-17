using UnityEngine;
using System.Collections;

public class QuestManager : Quest_Parameter {

	//クエストクリア・クエスト失敗
	
	//クエストのパラメタ//////////////////////////////////////////////////////////
	public int clear_count = 3;
	
	public int now_count = 0;//使い終わったら戻す！

	//クエストの状態//////////////////////////////////////////////////////////
/*	private　bool quest_count = false;//クエスト識別用・数
	private　bool quest_time = false;//クエスト識別用・時間
	private　bool quest_position = false;//クエスト識別用・位置*/

	//Script//////////////////////////////////////////////////////////
	private Static _static;
    private SceneManager sM;
    private uGUI_Msg GUImsg;//メッセージ操作用

    //コルーチン用
    private Coroutine coroutine;

    //カメラ操作用
    //public Camera F_camera;

    //Player
    public GameObject Player;

    // Use this for initialization
    void Start()
    {

        _static = GetComponent<Static>();
        sM = GetComponent<SceneManager>();

        //F_camera = GameObject.Find("FrontCamera").GetComponent<Camera>();

    }

    //クエストスタート時///////////////////////////////////////////////////////////

    public void QuestStart()
    {
        
        coroutine = StartCoroutine(C_QuestStart());

    }
    IEnumerator C_QuestStart()
    {
        yield return new WaitForSeconds(0.01f);//シーン切り替わり待ち

        Player = GameObject.FindGameObjectWithTag("Player");//切り替わってからでないと読めない
        Player.GetComponent<Player_ControllerZ>().SetKeylock(); Player.GetComponent<Player_ControllerZ>().SetKeylock();

        yield return new WaitForSeconds(3);//表示に時間がかかる可能性を考えて少したってから行動できるようにしておく

        Player.GetComponent<Player_ControllerZ>().SetActive();

    }

    //クエスト内/////////////////////////////////////////////////////////////////////
    //ここにクエスト内でのクリア条件に関する処理を書く
    void Update()
    {

        //Count
        if (now_count >= clear_count)
        {

            Clear_Count();

        }
    }

    public void SetCount(string CharaName)
    {
        for(int i = 0;i < quest_Target.Length ; i++){

            if (CharaName == quest_Target[i])//ターゲットに同じ文字列が入ってるとその数分だけカウントされるので注意
            {
                now_count++;
            }

        }
        
    }

	//クエスト終了/////////////////////////////////////////////////////////////////

	//ここにクリアした時の処理を書く。使ったものは戻す。

    IEnumerator QuestClear(){

        //クリア後だからたぶんほっといても大丈夫
        Player.GetComponent<Player_ControllerZ>().SetKeylock();
        /*Camera.main.enabled = false;
        F_camera.enabled = true;*/

		//クリア時のHP、MPを引き継がせる
		_static.SetHP(Player.GetComponent<Player_ControllerZ> ().GetHP ());
		_static.SetMP(Player.GetComponent<Player_ControllerZ> ().GetMP ());

        yield return new WaitForSeconds(3);//クリアを見せる

        sM.Guild();
        _static.day += quest_time;

    }

	void Clear_Count () {

        now_count = 0;//使い終わったら戻す
        coroutine = StartCoroutine(QuestClear());
		
	}

	//失敗したときの処理。使ったものは戻す
}
