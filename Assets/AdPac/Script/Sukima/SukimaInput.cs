using UnityEngine;
using System.Collections;
using UnityEngine.UI;//これが必要

public class SukimaInput : MonoBehaviour {

	new private Text guiText;

	public GameObject Sukima;
	private Sukima sukima;

	public enum Text_element{Ti = 0, Mizu = 1, Hi = 2 , Kaze = 3}; //ここで表示したいものを設定
	public Text_element element = Text_element.Ti;//ここに数字が格納
	
	
	// Use this for initialization
	void Start () {
		
		sukima = Sukima.GetComponent<Sukima>();
		guiText = this.GetComponent<Text>();

		
	}
	
	// Update is called once per frame
	void Update () {

		switch (element) {
		case Text_element.Ti:
			guiText.text = "地" + sukima.num_ti;
			break;
		case Text_element.Mizu:
			guiText.text = "水" + sukima.num_mizu;
			break;
		case Text_element.Hi:
			guiText.text = "火" + sukima.num_hi;
			break;
		case Text_element.Kaze:
			guiText.text = "風" + sukima.num_kaze;
			break;
		default:
			break;
		}

	}
}
