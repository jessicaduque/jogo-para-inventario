using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotChest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public char slotNum;
    bool mouseOver = false;

    void Update()
    {
        if (mouseOver)
        {
            //GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryController>().?(numSlot);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        slotNum = eventData.pointerEnter.transform.name[5];
        mouseOver = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}
