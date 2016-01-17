using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class DropUI : MonoBehaviour, IDropHandler
{
    
    public void OnDrop(PointerEventData e)
    {
        DragUI.obj.position = transform.position;
        DragUI.isWaku = true;
    }
}
