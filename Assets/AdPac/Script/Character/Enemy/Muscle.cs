using UnityEngine;
using System.Collections;

public class Muscle : MonoBehaviour {

    private bool isCoroutine = false;

    private Enemy_ControllerZ ecZ;

    // Use this for initialization
    void Start () {

        ecZ = GetComponent<Enemy_ControllerZ>();

    }
	
	// Update is called once per frame
	void Update () {

        StartCoroutine(Run());

	}

    IEnumerator Run()
    {

        if (isCoroutine) yield break;
        isCoroutine = true;

        /*
        transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(ecZ.move_controller.End - transform.localPosition), 0.05f);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        */
        /*
        //前を向ける
        transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(transform.localPosition + new Vector3(0, 0, 20)), 5);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        ecZ.Move(transform.position + new Vector3(0,0,20),2.5f);
        */

        ecZ.Move(transform.position + new Vector3(0, 0, 50), 10);

        yield return new WaitForSeconds(5.0f);//反転までの時間

        /*
        //前を向ける
        transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(transform.localPosition + new Vector3(0, 0, -20)), 5);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        
        */
        transform.Rotate(0,180,0);

        ecZ.Move(transform.position + new Vector3(0, 0, -50), 10);

        yield return new WaitForSeconds(5.0f);//反転までの時間

        transform.Rotate(0, 180, 0);

        isCoroutine = false;

    }
}
