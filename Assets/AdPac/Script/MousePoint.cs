using UnityEngine;
using System.Collections;

public class MousePoint : MonoBehaviour {

	public Vector3 worldPoint = Vector3.zero;
	public float z_position = 50.0f;
	
	void Update () {
		
		Vector3 screenPoint = Input.mousePosition;
		screenPoint.z = 50.0f;
		worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
		
	}
}
