using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	//Scene系は全部ここで管理

	private Static _static;

	// Use this for initialization
	void Start () {
	
		_static = GetComponent<Static>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GameStart (){
			
		
		Application.LoadLevel("Home");
        _static.count_Start++;

	}

	public void Guild (){

		Application.LoadLevel("Guild");

	}

	public void Home (){

		Application.LoadLevel("Home");
		
	}

    //アクションステージ//////////////////////////////////

    public void Green()
    {

        Application.LoadLevel("Green");

    }

    public void Mine()
    {

        Application.LoadLevel("Mine");

    }

    public void Town()
    {

        Application.LoadLevel("Town");

    }

    public void Swamp()
    {

        Application.LoadLevel("Swamp");

    }

    public void Ruins()
    {

        Application.LoadLevel("Ruins");

    }

}
