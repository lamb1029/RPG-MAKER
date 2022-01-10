using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int itemID; //아이템의 고유 id값, 중복 불가능
    public string itemName; //아이템, 이름 중복가능
    public string itemDescription; //아이템 설명
    public int itemCount; //소지개수
    public Sprite itemIcon;
    public ItemType itemType;

    public enum ItemType
    {
        use,
        Equip,
        Quest,
        ETC
    }

    public Item(int _itemId, string _itemName, string _itemDes, ItemType _itemType, int _itemCount = 1)
    {
        itemID = _itemId;
        itemName = _itemName;
        itemDescription = _itemDes;
        itemType = _itemType;
        itemCount = _itemCount;
        itemIcon = Resources.Load("Images/ItemIcon/" + _itemId.ToString(), typeof(Sprite)) as Sprite;
    }
}
