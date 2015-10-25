using UnityEngine;
using System.Collections;

public class ColEffect : MonoBehaviour {

    //ぶつかったときに出るエフェクト　ダメージエフェクトとかに

    public GameObject[] Effects;//出すエフェクト

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.tag != "Bullet")//弾と接触するときはエフェクトは出さない
        for(int i = 0;i < Effects.Length; i++)
        {
            Effects[i].transform.parent = null;//子供にしとくとたいてい消える
            Effects[i].SetActive(true);
                Destroy(Effects[i],2);//2秒ぐらいで消しとく
        }
    }
}
