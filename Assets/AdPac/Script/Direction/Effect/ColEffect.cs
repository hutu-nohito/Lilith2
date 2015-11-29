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
        //当たっても消したくないもの
        if (col.gameObject.name == "Vision" || col.gameObject.name == "Search" || col.gameObject.name == "Territory")
        {
            return;
        }
        //当たっても消えないもの
        if (col.tag == "Reflect")
        {
            return;
        }
        if (col.tag != "Bullet")//弾と接触するときはエフェクトは出さない
        for(int i = 0;i < Effects.Length; i++)
        {
            Effects[i].transform.parent = null;//子供にしとくとたいてい消える
            Effects[i].SetActive(true);
            Destroy(Effects[i],2);//2秒ぐらいで消しとく
        }
    }
}
