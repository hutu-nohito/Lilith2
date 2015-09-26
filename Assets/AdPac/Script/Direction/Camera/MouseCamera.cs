using UnityEngine;
using System.Collections;

public class MouseCamera : MonoBehaviour {
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	
	public float minimumX = -360F;
	public float maximumX = 360F;
	
	public float minimumY = -60F;
	public float maximumY = 60F;
	
	float rotationY = 0F;
	
	private bool flag_TopView = false;
	
	void Update ()
	{	
		
		if (axes == RotationAxes.MouseXAndY){

			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
			
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
			
		
		if(Input.GetKeyDown(KeyCode.Q)){
			
			//this.transform.eulerAngles = new Vector3(10, 0, 0);
			rotationY = 0;
			this.transform.localPosition = new Vector3(0,2,-2);
			
		}
		
		if(Input.GetKeyDown(KeyCode.T)){
			
			if(!flag_TopView){
				
				this.transform.localPosition += new Vector3(0,0,2.5f);
				flag_TopView = true;
				
			}else{
				
				this.transform.localPosition += new Vector3(0,0,-2.5f);
				flag_TopView = false;
				
			}
			
		}
		
	}
}
