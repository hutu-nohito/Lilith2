using UnityEngine;
using System.Collections;

public class Tyutorial : MonoBehaviour {

    private Player_ControllerZ pcZ;
    private Static _static;
    public GameObject[] tyuto = new GameObject[3];
    private bool[] istyuto = new bool[3];

	// Use this for initialization
	void Start () {

        _static = GameObject.FindGameObjectWithTag("Manager").GetComponent<Static>();
        pcZ = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_ControllerZ>();

        for(int i = 0; i < 3; i++)
        {
            istyuto[i] = false;
        }

	}
	
	// Update is called once per frame
	void Update () {
	
        if(_static.day == 1)
        {
            
            if (!istyuto[2])
            {
                pcZ.SetKeylock();

                if (istyuto[1])
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        istyuto[2] = true;
                        tyuto[2].SetActive(false);
                        pcZ.SetActive();
                    }
                }
                else
                {
                    if (!istyuto[0])
                    {

                        tyuto[0].SetActive(true);

                        if (Input.GetMouseButtonDown(0))
                        {
                            istyuto[0] = true;
                            tyuto[0].SetActive(false);
                            tyuto[1].SetActive(true);
                        }
                    }
                    else
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            istyuto[1] = true;
                            tyuto[1].SetActive(false);
                            tyuto[2].SetActive(true);
                        }
                    }
                }
                

                
            }
          
        }
	}
}
