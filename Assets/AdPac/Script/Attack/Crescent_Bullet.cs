using UnityEngine;
using System.Collections;

public class Crescent_Bullet : Attack_Parameter {

    public float returnTime = 2.5f;//折り返す時間
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Return());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Return()
    {
        yield return new WaitForSeconds(returnTime);

        this.GetComponent<Rigidbody>().velocity = Vector3.zero;

        yield return new WaitForSeconds(0.4f);

        Vector3 ReturnPos = Parent.transform.position + new Vector3(0,1.0f,0);
        this.GetComponent<Rigidbody>().velocity = ((ReturnPos - this.transform.position).normalized * speed);

    }
}
