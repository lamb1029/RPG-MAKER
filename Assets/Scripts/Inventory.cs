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

    private List<Item> inventoryItemList; //플레이어가 소지한 아이템 리스트.
    private List<Item> inventoryTabList; //선택한 탭에 따라 다르게 보여질 아이템리스트.

    public Text Description;
    public string[] tabDescription;

    public Transform tf; //보모객체

    public GameObject go;
    public GameObject[] selectedTabImages;
    public GameObject selection_Window;

    private int selectedItem;
    private int selectedtab;

    private bool activated; //인벤토리 활성화
    private bool tabActivated;
    private bool itemActivated;
    private bool stopKeyInput; //사용확인시 키입력제한
    private bool preventExec; //중복실행 제한

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
        Debug.LogError("존재하지않는 아이템입니다.");
    }

    public void RemoveSlot()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    } //인벤토리 슬롯 초기화

    public void ShowTab()
    {
        RemoveSlot();
        SelectedTab();
    } //텝 활겅화

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
    } //선택된 텝을 제외한 다른 텝 의 알파값 0

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
    } // 선택된 탭 반짝임

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
        } //탭에 따른 아이템 분류 후 inventoryTabList에 추가

        for(int i = 0; i < inventoryTabList.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].AddItem(inventoryTabList[i]);
        }  //inventoryTabList의 내용을 슬롯에 추가

        SelectedItem();

    } // 아이템 활성화

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
            Description.text = "해당 타입의 아이템을 소유하고 있지 않습니다.";
    }//선택된 아이템을 제외한 다른 아이템의 알파값 0

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
                } //탭 활성화시 키입력

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
                                //소모품 사용 선택지 호출
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
                } //아이템 활성화시 키입력

                if (Input.GetKeyUp(KeyCode.Z)) //중복실행방지
                    preventExec = false;
            }
        }
    }
}
