using UnityEngine;
using System.Collections;

public class HeartManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.H)){
				
			GetComponent<Rigidbody>().AddForce(new Vector3(0.0f,1.0f,0.0f),ForceMode.Impulse);

			Debug.Log(GetComponent<Rigidbody>().velocity);

		}

	}
}
