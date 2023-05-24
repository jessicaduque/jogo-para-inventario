using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotChest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    char slotNum;
    public int slot;
    public int lado;
    bool mouseOver = false;

    void Update()
    {
        if (mouseOver)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryController>().TransferirItem(slot, lado);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.fullyExited)
        {
            return;
        }
        Debug.Log("ESTÁ DENTRO");
        slotNum = eventData.pointerEnter.transform.name[5];
        slot = (int)char.GetNumericValue(slotNum);
        if(eventData.pointerEnter.transform.parent.parent.name == "Chest Inv")
        {
            lado = 0;
        }
        else
        {
            lado = 1;
        }

        mouseOver = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("ESTÁ FORA");
        mouseOver = false;
    }
}
