﻿using UnityEngine;
using System.Collections;

public class Guild : MonoBehaviour {

    //ギルドで行うことは基本的にここで処理する

    private int quest_num = 0;//どのクエストを選んだか

	public GameObject[] Quest_paper;

	private QuestManager qM;
	private Quest_Parameter quest_parameter;
    private SceneManager sM;
    private Static _static;

    //チュートリアル用
    private bool flag_onetime = false;
    public GameObject Tyuto;

    void Awake()
    {
        qM = GameObject.FindGameObjectWithTag("Manager").GetComponent<QuestManager>();
        sM = GameObject.FindGameObjectWithTag("Manager").GetComponent<SceneManager>();
        _static = GameObject.FindGameObjectWithTag("Manager").GetComponent<Static>();

        Camera.main.transform.position = CameraPos[0].transform.position;

        //ここでクエストから帰ってきた時の処理

        if (qM.isQuest)
        {
            _static.day += qM.quest_time;//失敗・成功にかかわらず終わったクエストのクエストタイムを加算
            qM.isQuest = false;//おろしとかないと家とギルドの往復で日にちが過ぎてく。

            /*//昼になったらクエスト期間を減らす
            if (_static.day - (int)_static.day == 0)//dayが偶数の時昼
            {
                for (int i = 0; i < 6; i++)//全部のクエストぺーパーのクエスト期間を1減少させる
                {
                    Quest_paper[i].GetComponent<Quest_Parameter>().SetTerm(Quest_paper[i].GetComponent<Quest_Parameter>().GetTerm() - 1); ;
                    //張り出し期間が終了したら入れ替える
                    if (Quest_paper[i].GetComponent<Quest_Parameter>().GetTerm() <= 0)
                    {
                        Quest_Refresh(i);//終了したクエスト
                    }
                }

            }*/

        }
    }
	// Use this for initialization
	void Start () {

	}

    //カメラ動かすよう
    public float MoveTime = 2;
    private float elapsedTime = 0;
    public GameObject[] CameraPos;
    private bool isMove = false;
    private int camNum = 0;//カメラを移動する位置
    public GameObject QuestBoad;
    public GameObject EntranceBoad;

    // Update is called once per frame
    void Update () {

        //チュートリアル用
        if (flag_onetime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Tyuto.SetActive(false);
            }
        }

        if (isMove)//カメラ用
        {
            elapsedTime += Time.deltaTime;
            Camera.main.transform.position += (CameraPos[camNum].transform.position - Camera.main.transform.position).normalized * 15 * Time.deltaTime;

            if(elapsedTime > MoveTime)
            {
                Camera.main.transform.position = CameraPos[camNum].transform.position;
                elapsedTime = 0;
                isMove = false;
                if(camNum == 1)
                {
                    QuestBoad.SetActive(true);
                }
                if(camNum == 0)
                {
                    EntranceBoad.SetActive(true);
                }
            }
        }
	}

    //カメラ
    public void Quest()
    {
        if (_static.day == 1)
        {
            if (!flag_onetime)
            {
                flag_onetime = true;
                Invoke("OneTime", 2);
            }
        }
        
        EntranceBoad.SetActive(false);
        isMove = true;
        camNum = 1;
    }

    void OneTime()
    {
        Tyuto.SetActive(true);
    }

    public void Entrance()
    {

        QuestBoad.SetActive(false);

        isMove = true;
        camNum = 0;
    }

    public void Home()
    {
        sM.Home();
    }

    //クエスト掲示板/////////////////////////////////////////////////////////

    public void Quest0 (){

        if (_static.day - (int)_static.day == 0)//昼だったら
        {
            Quest_paper[0].SetActive(!Quest_paper[0].activeSelf);
            quest_num = 0;
        }
        else//夜だったら
        {
            Quest_paper[4].SetActive(!Quest_paper[4].activeSelf);
            quest_num = 4;
        }
		

	}
	
	public void Quest1 (){

        if (_static.day - (int)_static.day == 0)//昼だったら
        {
            Quest_paper[1].SetActive(!Quest_paper[1].activeSelf);
            quest_num = 1;
        }
        else//夜だったら
        {
            Quest_paper[5].SetActive(!Quest_paper[5].activeSelf);
            quest_num = 5;
        }
	}
    public void Quest2()
    {

        if (_static.day - (int)_static.day == 0)//昼だったら
        {
            Quest_paper[2].SetActive(!Quest_paper[2].activeSelf);
            quest_num = 2;
        }
        else//夜だったら
        {
            Quest_paper[6].SetActive(!Quest_paper[6].activeSelf);
            quest_num = 6;
        }
       
    }

    public void Quest3()
    {
        if (_static.day - (int)_static.day == 0)//昼だったら
        {
            Quest_paper[3].SetActive(!Quest_paper[3].activeSelf);
            quest_num = 3;

        }
        else//夜だったら
        {
            Quest_paper[7].SetActive(!Quest_paper[7].activeSelf);
            quest_num = 7;
        }
       
    }

    public void Quest4()
    {

        Quest_paper[4].SetActive(!Quest_paper[4].activeSelf);
        quest_num = 4;

    }
    public void Quest5()
    {

        Quest_paper[5].SetActive(!Quest_paper[5].activeSelf);
        quest_num = 5;

    }

	public void Quest_Start(){//クエストのパラメタをManagerに渡してスタート スタートボタン

        quest_parameter = Quest_paper[quest_num].GetComponent<Quest_Parameter>();

        qM.quest_ID = quest_parameter.quest_ID;
        qM.QuestName = quest_parameter.QuestName;
        qM.type = quest_parameter.type;
        qM.stage = quest_parameter.stage;
        qM.SetQuestStageID(quest_parameter.GetQestStageID());
        qM.quest_Target = quest_parameter.quest_Target;//これで配列の中身が全部移る
        qM.clear_num = quest_parameter.clear_num;
        qM.rewards = quest_parameter.rewards;
        qM.quest_level = quest_parameter.quest_level;
        qM.quest_term = quest_parameter.quest_term;
        qM.quest_time = quest_parameter.quest_time;

        qM.isQuest = true;//クエスト中フラグを立てる
        qM.QuestStart();

        switch (qM.stage)
        {
            case Quest_Parameter.Stage.Gaidou:
                sM.Gaidou();
                break;
            case Quest_Parameter.Stage.Forest:
                sM.Forest();
                break;
            case Quest_Parameter.Stage.Pond:
                sM.Pond();
                break;
            case Quest_Parameter.Stage.Kougen:
                sM.Kougen();
                break;
            case Quest_Parameter.Stage.Green:
                sM.Green();
                break;
            case Quest_Parameter.Stage.Mine:
                sM.Mine();
                break;
            case Quest_Parameter.Stage.Town:
                sM.Town();
                break;
            case Quest_Parameter.Stage.Swamp:
                sM.Swamp();
                break;
            case Quest_Parameter.Stage.Ruins:
                sM.Ruins();
                break;
            default:
                break;
        }

	}

    //戻るボタン
	public void Quest_Back()
	{
		
		Quest_paper[quest_num].SetActive(!Quest_paper[quest_num].activeSelf);
		
	}

    //情報掲示板/////////////////////////////////////////////////////////

    //BP交換所/////////////////////////////////////////////////////////

    //クエスト番号をもらって入れ替え　ここに全クエスト書いとく
    private void Quest_Refresh(int num)
    {
        //入れ替え用の箱を用意
        int quest_ID = 0;//クエスト識別番号
        string QuestName = "Lilith";//クエストの名前
        Quest_Parameter.Quest_Type type = Quest_Parameter.Quest_Type.Subjugation;
        Quest_Parameter.Stage stage = Quest_Parameter.Stage.Green;
        int queststageID = 0;//ステージの構成を選択　変わる
        string[] quest_Target = new string[] { "MadMushroom" };//そのクエストの終了条件を満たす対象
        string[] rewards = new string[] { "魔石" };//クエ報酬（複数可）
        string quest_level = "E";//クエスト難易度
        int quest_term = 7;//クエストが張り出される期間
        float quest_time = 0.5f;//そのクエストで何日進むか

        int nextQuest = Random.Range(0,1);//0~0まででランダムで次のクエストを決定

        switch (nextQuest)
        {
            case 0:
                quest_ID = 0;//クエスト識別番号
                QuestName = "チュートリアル";//クエストの名前
                type = Quest_Parameter.Quest_Type.Subjugation;
                stage = Quest_Parameter.Stage.Green;
                queststageID = 1;
                quest_Target = new string[]{ "MadMushroom"};//そのクエストの終了条件を満たす対象
                rewards = new string[] { "レシピ" };//クエ報酬（複数可）
                quest_level = "E";//クエスト難易度
                quest_term = 5;//クエストが張り出される期間
                quest_time = 0.5f;//そのクエストで何日進むか
                break;
            case 1:
                quest_ID = 1;//クエスト識別番号
                QuestName = "精霊採取";//クエストの名前
                type = Quest_Parameter.Quest_Type.Collect;
                stage = Quest_Parameter.Stage.Mine;
                queststageID = 0;
                quest_Target = new string[] { "Enemy_Flame" };//そのクエストの終了条件を満たす対象
                rewards = new string[] { "レシピ" };//クエ報酬（複数可）
                quest_level = "D";//クエスト難易度
                quest_term = 3;//クエストが張り出される期間
                quest_time = 0.5f;//そのクエストで何日進むか
                break;
            default:
                break;
        }

        //入れ替え
        quest_parameter = Quest_paper[num].GetComponent<Quest_Parameter>();//入れ替えるクエストペーパー
        quest_parameter.quest_ID = quest_ID;
        quest_parameter.QuestName = QuestName;
        quest_parameter.type = type;
        quest_parameter.stage = stage;
        quest_parameter.SetQuestStageID(queststageID);
        quest_parameter.quest_Target = quest_Target;
        quest_parameter.rewards = rewards;
        quest_parameter.quest_level = quest_level;
        quest_parameter.quest_term = quest_term;
        quest_parameter.quest_time = quest_time;

    }

}
