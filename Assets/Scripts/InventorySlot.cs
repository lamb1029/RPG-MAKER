using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text itemName;
    public Text itemCount;
    public GameObject selected;

    public void AddItem(Item _item)
    {
        itemName.text = _item.itemName;
        icon.sprite = _item.itemIcon;
        if(Item.ItemType.use == _item.itemType)
        {
            if (_item.itemCount > 0)
            {
                itemCount.text = "x" + _item.itemCount.ToString();
            }
            else
                itemCount.text = "";
        }
    }

    public void RemoveItem()
    {
        itemName.text = "";
        itemCount.text = "";
        icon.sprite = null;
    }
}
