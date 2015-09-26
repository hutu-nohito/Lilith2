using UnityEngine;
using System.Collections;

public class Sukima : MonoBehaviour {

	public GameObject Yubi;//出てくる場所 
	public GameObject Ti_1;//地のジェム
	public GameObject Mizu_1;//水のジェム
	public GameObject Hi_1;//火のジェム
	public GameObject Kaze_1;//風のジェム

	public int num_ti = 0;//地の総数
	public int num_mizu = 0;//水の総数
	public int num_hi = 0;//火の総数
	public int num_kaze = 0;//風の総数

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col){

		switch (col.GetComponent<Sukima_ball_parameter> ().gem_element) {
		case "Ti":

			num_ti += 1;

			break;
		case "Mizu":
			
			num_mizu += 1;
			
			break;
		case "Hi":
			
			num_hi += 1;
			
			break;
		case "Kaze":
			
			num_kaze += 1;
			
			break;
		default:
			break;

		}
	}

	void OnTriggerExit (Collider col){
		
		switch (col.GetComponent<Sukima_ball_parameter> ().gem_element) {
		case "Ti":
			
			num_ti -= 1;
			
			break;
		case "Mizu":
			
			num_mizu -= 1;
			
			break;
		case "Hi":
			
			num_hi -= 1;
			
			break;
		case "Kaze":
			
			num_kaze -= 1;
			
			break;
		default:
			break;
			
		}
	}


	public void Ti (){

		Ti_1.transform.position = Yubi.transform.position;
		Instantiate (Ti_1);

	}

	public void Mizu (){
		
		Mizu_1.transform.position = Yubi.transform.position;
		Instantiate (Mizu_1);
		
	}

	public void Hi (){
		
		Hi_1.transform.position = Yubi.transform.position;
		Instantiate (Hi_1);
		
	}

	public void Kaze (){
		
		Kaze_1.transform.position = Yubi.transform.position;
		Instantiate (Kaze_1);
		
	}
}
