using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberSystem : MonoBehaviour
{
    private AudioManager theAudio;
    public string key_sound; //방향키사운드
    public string enter_sound; //결정키 사운드
    public string cancel_sound; //오답&취고 사운드
    public string correct_sound; //정답 사운드

    private int count; //배열크기 자릿수
    private int seletedTextBox; // 선택된 자릿수
    private int result; //플레이어가 입력한 값
    private int correctNumber; //정답

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
            else if (Input.GetKeyDown(KeyCode.Z)) //결정
            {
                theAudio.Play(key_sound);
                keyinput = false;
                StartCoroutine(OXCoroutine());
            }
            else if (Input.GetKeyDown(KeyCode.X)) //취소
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
        Debug.Log($"우리가 낸 답 {result}, 정답 {correctNumber}");
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
        int temp = int.Parse(Number_text[seletedTextBox].text); //선택된 자리수의 텍스트를 int형식으로 변형
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
