using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    static public DatabaseManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public string[] var_name;
    public float[] var;

    public string[] swich_name;
    public bool[] swich;

    public List<Item> itemList = new List<Item>();

    public void UseItem(int _itemID)
    {
        switch (_itemID)
        {
            case 10001:
                break;
            case 10002:
                break;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        itemList.Add(new Item(10001, "���� ����", "ü���� 50 ȸ�������ִ� ����", Item.ItemType.use));
        itemList.Add(new Item(10002, "�Ķ� ����", "������ 15 ȸ�������ִ� ����", Item.ItemType.use));
        itemList.Add(new Item(10003, "���� ���� ����", "ü���� 300 ȸ�������ִ� ����", Item.ItemType.use));
        itemList.Add(new Item(10004, "���� �Ķ� ����", "������ 60 ȸ�������ִ� ����", Item.ItemType.use));
        itemList.Add(new Item(11001, "���� ����", "�������� ������ ���´�.", Item.ItemType.use));
        itemList.Add(new Item(20001, "ª�� ��", "�⺻���� ����� ��", Item.ItemType.Equip));
    }
}
