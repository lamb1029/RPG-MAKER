using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int itemID; //�������� ���� id��, �ߺ� �Ұ���
    public string itemName; //������, �̸� �ߺ�����
    public string itemDescription; //������ ����
    public int itemCount; //��������
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
