using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    
    public static RectTransform obj;

    public static bool isWaku = false;//枠に入ったかどうか

    //返すよう
    public SukimaCanvas SuCa;
    private bool isReturn = false;//魔石置き場に返す
    private float elapsedTime = 0;
    public float returnTime = 1;

    void Start()
    {
    }

    void Update()
    {
        /*if (isReturn)
        {
            elapsedTime += Time.deltaTime;
            obj.position += (SuCa.Maseki_ButtonPos[SuCa.Kind_Maseki] - obj.position).normalized * Time.deltaTime;
            if(elapsedTime > returnTime)
            {
                obj.position = SuCa.Maseki_ButtonPos[SuCa.Kind_Maseki];
                elapsedTime = 0;
                isReturn = false;
            }

        }*/
    }

    public void OnBeginDrag(PointerEventData e)
    {
        isWaku = false;//持ったら入ってない
        obj = Instantiate(SuCa.Maseki_Button[SuCa.Kind_Maseki]).GetComponent<RectTransform>();//ボタンそのものを持ってかれると困る
        obj.gameObject.transform.parent = SuCa.gameObject.transform.FindChild("Tukue");
        //obj.SetAsLastSibling();//これで階層内での順番を決めてる
        //obj.SetAsFirstSibling();
        obj.SetSiblingIndex(2);
    }
    public void OnDrag(PointerEventData e)
    {
        obj.position = e.position;
    }
    public void OnEndDrag(PointerEventData e)
    {
        obj.SetAsLastSibling();
        if (!isWaku)
        {
            Destroy(obj.gameObject);
        }
    }
}
