using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberSystem : MonoBehaviour
{
    private AudioManager theAudio;
    public string key_sound; //����Ű����
    public string enter_sound; //����Ű ����
    public string cancel_sound; //����&��� ����
    public string correct_sound; //���� ����

    private int count; //�迭ũ�� �ڸ���
    private int seletedTextBox; // ���õ� �ڸ���
    private int result; //�÷��̾ �Է��� ��
    private int correctNumber; //����

    public string tempNumber;

    public GameObject superobject;
    public GameObject[] panel;
    public Text[] Number_text;

    public Animator anim;

    public bool activated;
    private bool keyinput;
    private bool correctFlag;

    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
    }

    public void ShowNumber(int _currectNumber)
    {
        correctNumber = _currectNumber;
        activated = true;
        correctFlag = false;

        string temp = correctNumber.ToString();
        for (int i = 0; i < temp.Length; i++)
        {
            count = i;
            panel[i].SetActive(true);
            Number_text[i].text = "0";
        }

        superobject.transform.position = new Vector3(superobject.transform.position.x + (30 * count), superobject.transform.position.y, superobject.transform.position.z);

        seletedTextBox = 0;
        result = 0;
        SetColor();
        anim.SetBool("Appear", true);
        keyinput = true;
    }

    public bool GetResult()
    {
        return correctFlag;
    }

    // Update is called once per frame
    void Update()
    {
        if (keyinput)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                theAudio.Play(key_sound);
                SetNumber("DOWN");
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                theAudio.Play(key_sound);
                SetNumber("UP");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                theAudio.Play(key_sound);

                if (seletedTextBox < count)
                    seletedTextBox++;
                else
                    seletedTextBox = count;
                SetColor();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                theAudio.Play(key_sound);
                if (seletedTextBox > 0)
                    seletedTextBox--;
                else
                    seletedTextBox = 0;
                SetColor();
            }
            else if (Input.GetKeyDown(KeyCode.Z)) //����
            {
                theAudio.Play(key_sound);
                keyinput = false;
                StartCoroutine(OXCoroutine());
            }
            else if (Input.GetKeyDown(KeyCode.X)) //���
            {
                theAudio.Play(key_sound);
                keyinput = false;
                StartCoroutine(ExitCoroutine());
            }
        }
    }

    IEnumerator OXCoroutine()
    {
        Color color = Number_text[0].color;
        color.a = 1f;

        for (int i = count; i >= 0; i--)
        {
            Number_text[i].color = color;
            tempNumber += Number_text[i].text;
        }

        yield return new WaitForSeconds(1f);

        result = int.Parse(tempNumber);
        if (result == correctNumber)
        {
            theAudio.Play(correct_sound);
            correctFlag = true;
        }
        else
        {
            theAudio.Play(cancel_sound);
            correctFlag = false;
        }
        Debug.Log($"�츮�� �� �� {result}, ���� {correctNumber}");
        StartCoroutine(ExitCoroutine());
    }
    IEnumerator ExitCoroutine()
    {
        result = 0;
        tempNumber = "";
        anim.SetBool("Appear", false);

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i <= count; i++)
        {
            panel[i].SetActive(false);
        }
        superobject.transform.position = new Vector3(superobject.transform.position.x - (30 * count), superobject.transform.position.y, superobject.transform.position.z);

        activated = false;
    }

    public void SetNumber(string _arrow)
    {
        int temp = int.Parse(Number_text[seletedTextBox].text); //���õ� �ڸ����� �ؽ�Ʈ�� int�������� ����
        if (_arrow == "DOWN")
        {
            if (temp == 0)
                temp = 9;
            else
                temp--;
        }
        if (_arrow == "UP")
        {
            if (temp == 9)
                temp = 0;
            else
                temp++;
        }
        Number_text[seletedTextBox].text = temp.ToString();
    }

    public void SetColor()
    {
        Color color = Number_text[0].color;
        color.a = 0.3f;
        for (int i = 0; i <= count; i++)
        {
            Number_text[i].color = color;
        }
        color.a = 1f;
        Number_text[seletedTextBox].color = color;
    }
}
