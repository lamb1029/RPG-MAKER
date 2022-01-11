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
        itemList.Add(new Item(10001, "빨간 포션", "체력을 50 회복시켜주는 물약", Item.ItemType.use));
        itemList.Add(new Item(10002, "파란 포션", "마나를 15 회복시켜주는 물약", Item.ItemType.use));
        itemList.Add(new Item(10003, "농축 빨간 포션", "체력을 300 회복시켜주는 물약", Item.ItemType.use));
        itemList.Add(new Item(10004, "농축 파란 포션", "마나를 60 회복시켜주는 물약", Item.ItemType.use));
        itemList.Add(new Item(11001, "랜덤 상자", "랜덤으로 포션이 나온다.", Item.ItemType.use));
        itemList.Add(new Item(20001, "짧은 검", "기본적인 용사의 검", Item.ItemType.Equip));
    }
}
