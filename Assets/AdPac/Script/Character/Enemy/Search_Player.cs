using UnityEngine;
using System.Collections;

public class Search_Player : MonoBehaviour {

	private Enemy_ControllerZ ecZ;//敵の状態

	// Use this for initialization
	void Start () {

        ecZ = GetComponentInParent<Enemy_ControllerZ>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col){

		if(col.tag == "Player"){
			switch(ecZ.habit){
                case Enemy_ControllerZ.Enemy_Habit.Positive:
                    ecZ.SetState(Enemy_ControllerZ.Enemy_State.Attack);
				break;
                case Enemy_ControllerZ.Enemy_Habit.Normal:
				break;
                case Enemy_ControllerZ.Enemy_Habit.Negative:
                ecZ.SetState(Enemy_ControllerZ.Enemy_State.Run);
				break;

			}
		}
	}

	void OnTriggerExit (Collider col){

		if(col.tag == "Player"){

			Invoke("LoseSight",2);
						
		}

	}

	void LoseSight (){

        ecZ.SetState(Enemy_ControllerZ.Enemy_State.Search);

	}
}
