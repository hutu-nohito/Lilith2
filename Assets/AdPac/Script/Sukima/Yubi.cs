using UnityEngine;
using System.Collections;

public class Yubi : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.A)) {
				
			if (this.transform.position.x > -5) {

				this.transform.Translate (-0.1f, 0, 0);

			} else {

				this.transform.position = new Vector3 (-5.0f, 25, 0);

			}

		}

		if(Input.GetKey(KeyCode.D)){

			if(this.transform.position.x < 5){
				
				this.transform.Translate(0.1f,0,0);
				
			}else{
				
				this.transform.position = new Vector3(5.0f,25,0);
				
			}
		}

	}
}
