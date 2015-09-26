using UnityEngine;
using System.Collections;

public class HP_Manager : MonoBehaviour {

	//HPがなくなった場合の処理を行う

	private Character_Parameter character_parameter;
	private QuestManager questmanager;

	// Use this for initialization
	void Start () {

		character_parameter = GetComponent<Character_Parameter>();
		questmanager = GameObject.FindGameObjectWithTag("GameController").GetComponent<QuestManager>();

	}
	
	// Update is called once per frame
	void Update () {
	
		if(character_parameter.now_c_point <= 0){

			switch(this.tag){
			case "Player":
				Application.LoadLevel(Application.loadedLevel);
				break;
			case "Enemy":
				Destroy(this.gameObject);
				questmanager.now_count++;
				break;
			case "Gimmick":
				Destroy(this.gameObject);
				break;
			default:
				break;
			}

		}
	}
}
