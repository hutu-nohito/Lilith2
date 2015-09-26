using UnityEngine;
using System.Collections;

public class SimpleCol : MonoBehaviour {

    public enum Type
    {
        Destroy,//消す
        Through//貫通
    }
    public Type type = Type.Destroy;

    public bool ThroughEnemy = true;//敵貫通
    public bool ThroughBullet = true;//弾を打ち消す
    public bool ThroughPlayer = true;//自分貫通

    private Magic_Controller MC;

    // Use this for initialization
    void Start () {

        MC = GameObject.FindGameObjectWithTag("Player").GetComponent<Magic_Controller>();

	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Vision" || col.gameObject.name == "Search")
        {
            return;
        }
        switch(type){
            case Type.Destroy:

                Destroy(this.gameObject);
                MC.SubExistBullet();
                break;
            case Type.Through:

                if (col.tag == "Enemy")
                {
                    if (ThroughEnemy) break;
                    else
                    {
                        Destroy(this.gameObject);
                        MC.SubExistBullet();
                    }
                }
                if (col.tag == "Bullet")
                {
                    if (ThroughBullet) break;
                    else
                    {
                        Destroy(this.gameObject);
                        MC.SubExistBullet();
                    }
                }
                if (col.tag == "Player")
                {
                    if (ThroughPlayer) break;
                    else
                    {
                        Destroy(this.gameObject);
                        MC.SubExistBullet();
                    }
                }
                break;
                    default:
                break;
        }
    }
}
