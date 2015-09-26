using UnityEngine;
using System.Collections;

public class MoveSmooth : MonoBehaviour {

    public Vector3 StartPos;
	public Vector3 EndPos;
	public float time = 5;
	private Vector3 deltaPos;

	private float elapsedTime;

    private bool isMove = false;

	// Use this for initialization
	void Start () {

        StartPos = transform.localPosition;
        deltaPos = (EndPos - StartPos) / time;
        elapsedTime = 0;

    }

    // Update is called once per frame
    void Update() {

        if (isMove) { 
            transform.position += deltaPos * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            if (elapsedTime > time)
            {

                transform.localPosition = EndPos;

                elapsedTime = 0;
                isMove = false;
                
            }
        }
    }

    public void Move (Vector3 End)
    {
        elapsedTime = 0;
        EndPos = End;
        StartPos = transform.localPosition;
        deltaPos = (EndPos - StartPos) / time;
        isMove = true;
    }
}
