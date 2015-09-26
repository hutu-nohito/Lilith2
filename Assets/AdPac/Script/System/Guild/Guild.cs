using UnityEngine;
using System.Collections;

public class Guild : MonoBehaviour {

    //ギルドで行うことは基本的にここで処理する

    private int quest_num = 0;

	public GameObject[] Quest_paper;

	private QuestManager qM;
	private Quest_Parameter quest_parameter;
    private SceneManager sM;

	// Use this for initialization
	void Start () {

        qM = GameObject.FindGameObjectWithTag("Manager").GetComponent<QuestManager>();
        sM = GameObject.FindGameObjectWithTag("Manager").GetComponent<SceneManager>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //クエスト掲示板/////////////////////////////////////////////////////////

	public void Quest0 (){

		Quest_paper [0].SetActive (!Quest_paper[0].activeSelf);
        quest_num = 0;

	}
	
	public void Quest1 (){
		
		Quest_paper [1].SetActive (!Quest_paper[1].activeSelf);
        quest_num = 1;
		
	}
    public void Quest2()
    {

        Quest_paper[2].SetActive(!Quest_paper[2].activeSelf);
        quest_num = 2;

    }

    public void Quest3()
    {

        Quest_paper[3].SetActive(!Quest_paper[3].activeSelf);
        quest_num = 3;

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

	public void Quest_Start(){

        quest_parameter = Quest_paper[quest_num].GetComponent<Quest_Parameter>();

        qM.quest_ID = quest_parameter.quest_ID;
        qM.QuestName = quest_parameter.QuestName;
        qM.type = quest_parameter.type;
        qM.stage = quest_parameter.stage;
        qM.quest_Target = quest_parameter.quest_Target;
        qM.rewards = quest_parameter.rewards;
        qM.quest_time = quest_parameter.quest_time;

        switch (qM.stage)
        {
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

        qM.QuestStart();

	}

	public void Quest_Back()
	{
		
		Quest_paper[quest_num].SetActive(!Quest_paper[quest_num].activeSelf);
		
	}

    //情報掲示板/////////////////////////////////////////////////////////

    //BP交換所/////////////////////////////////////////////////////////

}
