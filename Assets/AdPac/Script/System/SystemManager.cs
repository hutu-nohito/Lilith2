using UnityEngine;
using System.Collections;

public class SystemManager : MonoBehaviour {

	//全体で使いたいものを取っといてこれを参照する
	
	//Object/////////////////////////////////////////////////////////
	public GameObject Player;//プレイヤーのキャラ
	public GameObject UI;//UIの親Object

	public GameObject[] Enemy;//出現してる敵

	// Use this for initialization
	void Start () {

		Player = GameObject.FindGameObjectWithTag("Player");
		UI = GameObject.FindGameObjectWithTag("UI");

		//これで配列に入るらしい。順番はわからん
		Enemy = GameObject.FindGameObjectsWithTag("Enemy");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
