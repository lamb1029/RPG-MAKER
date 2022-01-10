using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private DatabaseManager thedatabase;
    private OrderManager theOrder;
    private AudioManager theAudio;

    public string key_sound;
    public string enter_sound;
    public string cancel_sound;
    public string open_sound;
    public string beep_sound;

    private InventorySlot[] slots;

    private List<Item> inventoryItemList; //�÷��̾ ������ ������ ����Ʈ.
    private List<Item> inventoryTabList; //������ �ǿ� ���� �ٸ��� ������ �����۸���Ʈ.

    public Text Description;
    public string[] tabDescription;

    public Transform tf; //����ü

    public GameObject go;
    public GameObject[] selectedTabImages;
    public GameObject selection_Window;

    private int selectedItem;
    private int selectedtab;

    private bool activated; //�κ��丮 Ȱ��ȭ
    private bool tabActivated;
    private bool itemActivated;
    private bool stopKeyInput; //���Ȯ�ν� Ű�Է�����
    private bool preventExec; //�ߺ����� ����

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    void Start()
    {
        instance = this;
        thedatabase = FindObjectOfType<DatabaseManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theAudio = FindObjectOfType<AudioManager>();
        inventoryItemList = new List<Item>();
        inventoryTabList = new List<Item>();
        slots = tf.GetComponentsInChildren<InventorySlot>();
    }

    public void GetAnItem(int _itemID, int _count = 1)
    {
        for (int i = 0; i < thedatabase.itemList.Count; i++)
        {
            if(_itemID == thedatabase.itemList[i].itemID)
            {
                for(int j = 0; j < inventoryItemList.Count; j++)
                {
                    if(inventoryItemList[j].itemID == _itemID)
                    {
                        if(inventoryItemList[j].itemType == Item.ItemType.use)
                        {
                            inventoryItemList[j].itemCount += _count;
                        }
                        else
                        {
                            inventoryItemList.Add(thedatabase.itemList[i]);
                        }
                        return;
                        
                    }
                }
                inventoryItemList.Add(thedatabase.itemList[i]);
                inventoryItemList[inventoryItemList.Count - 1].itemCount = _count;
                return;
            }
        }
        Debug.LogError("���������ʴ� �������Դϴ�.");
    }

    public void RemoveSlot()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    } //�κ��丮 ���� �ʱ�ȭ

    public void ShowTab()
    {
        RemoveSlot();
        SelectedTab();
    } //�� Ȱ��ȭ

    public void SelectedTab()
    {
        StopAllCoroutines();
        Color color = selectedTabImages[selectedtab].GetComponent<Image>().color;
        color.a = 0f;
        for(int i = 0; i < selectedTabImages.Length; i++)
        {
            selectedTabImages[i].GetComponent<Image>().color = color;
        }
        Description.text = tabDescription[selectedtab];
        StartCoroutine(SelectedTabEffect());
    } //���õ� ���� ������ �ٸ� �� �� ���İ� 0

    IEnumerator SelectedTabEffect()
    {
        Color color = selectedTabImages[0].GetComponent<Image>().color;
        while (tabActivated)
        {
            while(color.a < 0.5f)
            {
                color.a += 0.03f;
                selectedTabImages[selectedtab].GetComponent<Image>().color = color;
                yield return waitTime;
            }

            while (color.a > 0f)
            {
                color.a -= 0.03f;
                selectedTabImages[selectedtab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            yield return waitTime;
        }
    } // ���õ� �� ��¦��

    public void ShowItem()
    {
        inventoryTabList.Clear();
        RemoveSlot();
        selectedItem = 0;

        switch(selectedtab)
        {
            case 0:
                for(int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.use == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
            case 1:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Equip == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
            case 2:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Quest == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
            case 3:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.ETC == inventoryItemList[i].itemType)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
        } //�ǿ� ���� ������ �з� �� inventoryTabList�� �߰�

        for(int i = 0; i < inventoryTabList.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].AddItem(inventoryTabList[i]);
        }  //inventoryTabList�� ������ ���Կ� �߰�

        SelectedItem();

    } // ������ Ȱ��ȭ

    public void SelectedItem()
    {
        if (inventoryTabList.Count > 0)
        {
            Color color = slots[0].selected.GetComponent<Image>().color;
            color.a = 0f;
            for(int i = 0; i < inventoryTabList.Count; i++)
                slots[i].selected.GetComponent<Image>().color = color;
            Description.text = inventoryTabList[selectedItem].itemDescription;
            StartCoroutine(SelectedItemEffect());
        }
        else
            Description.text = "�ش� Ÿ���� �������� �����ϰ� ���� �ʽ��ϴ�.";
    }//���õ� �������� ������ �ٸ� �������� ���İ� 0

    IEnumerator SelectedItemEffect()
    {
        Color color = slots[0].GetComponent<Image>().color;
        while (itemActivated)
        {
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                slots[selectedItem].selected.GetComponent<Image>().color = color;
                yield return waitTime;
            }

            while (color.a > 0f)
            {
                color.a -= 0.03f;
                slots[selectedItem].selected.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            yield return waitTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!stopKeyInput)
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                activated = !activated;
                if(activated)
                {
                    theAudio.Play(open_sound);
                    theOrder.NotMove();
                    go.SetActive(true);
                    selectedtab = 0;
                    tabActivated = true;
                    itemActivated = false;
                    ShowTab();
                }
                else
                {
                    theAudio.Play(cancel_sound);
                    StopAllCoroutines();
                    go.SetActive(false);
                    tabActivated = false;
                    itemActivated = false;
                    theOrder.Move();
                }
            }

            if(activated)
            {
                if(tabActivated)
                {
                    if(Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (selectedtab < selectedTabImages.Length - 1)
                            selectedtab++;
                        else
                            selectedtab = selectedTabImages.Length - 1;
                        theAudio.Play(key_sound);
                        SelectedTab();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (selectedtab > 0)
                            selectedtab--;
                        else
                            selectedtab = 0;
                        theAudio.Play(key_sound);
                        SelectedTab();
                    }
                    else if(Input.GetKeyDown(KeyCode.Z))
                    {
                        StopAllCoroutines();
                        theAudio.Play(enter_sound);
                        Color color = selectedTabImages[selectedtab].GetComponent<Image>().color;
                        color.a = 0.5f;
                        selectedTabImages[selectedtab].GetComponent<Image>().color = color;
                        itemActivated = true;
                        tabActivated = false;
                        preventExec = true;

                        ShowItem();
                    }
                } //�� Ȱ��ȭ�� Ű�Է�

                else if(itemActivated)
                {
                    if(inventoryTabList.Count > 0)
                    {
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            if (selectedItem < inventoryTabList.Count - 2)
                                selectedItem += 2;
                            else
                                selectedItem %= 2;
                            theAudio.Play(key_sound);
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            if (selectedItem > 2)
                                selectedItem -= 2;
                            else
                                selectedItem = inventoryTabList.Count - 1 - selectedItem;
                            theAudio.Play(key_sound);
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (selectedItem < inventoryTabList.Count - 1)
                                selectedItem++;
                            else
                                selectedItem = 0;
                            theAudio.Play(key_sound);
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (selectedItem > 0)
                                selectedItem--;
                            else
                                selectedItem = inventoryTabList.Count - 1;
                            theAudio.Play(key_sound);
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.Z) && !preventExec)
                        {
                            if (selectedtab == 0)
                            {
                                theAudio.Play(enter_sound);
                                stopKeyInput = true;
                                //�Ҹ�ǰ ��� ������ ȣ��
                            }
                            else if (selectedtab == 1)
                            {

                            }
                            else
                            {
                                theAudio.Play(beep_sound);
                            }
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        theAudio.Play(cancel_sound);
                        StopAllCoroutines();
                        itemActivated = false;
                        tabActivated = true;
                        ShowTab();
                    }
                } //������ Ȱ��ȭ�� Ű�Է�

                if (Input.GetKeyUp(KeyCode.Z)) //�ߺ��������
                    preventExec = false;
            }
        }
    }
}
