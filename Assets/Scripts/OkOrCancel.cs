using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OkOrCancel : MonoBehaviour
{
    AudioManager theAudio;
    public string key_sound;
    public string enter_sound;
    public string cancel_sound;

    public GameObject up_Panel;
    public GameObject down_Panel;

    public Text up_text;
    public Text down_text;

    public bool activated;
    bool keyInput;
    bool result = true;

    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
    }

    public void Selected()
    {
        theAudio.Play(key_sound);
        result = !result;
        if (result)
        {
            up_Panel.gameObject.SetActive(false);
            down_Panel.gameObject.SetActive(true);
        }
        else
        {
            up_Panel.gameObject.SetActive(true);
            down_Panel.gameObject.SetActive(false);
        }
    }

    public void ShowTwoChoice(string _upText, string _downText)
    {
        activated = true;
        result = true;
        up_text.text = _upText;
        down_text.text = _downText;

        up_Panel.gameObject.SetActive(false);
        down_Panel.gameObject.SetActive(true);

        StartCoroutine(ShowTwoChoiceCoroutine());
    }

    IEnumerator ShowTwoChoiceCoroutine()
    {
        yield return new WaitForSeconds(0.01f);
        keyInput = true;
    }

    public bool GetResult()
    {
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        if(keyInput)
        {
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                Selected();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Selected();
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                theAudio.Play(enter_sound);
                keyInput = false;
                activated = false;
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                theAudio.Play(cancel_sound);
                keyInput = false;
                activated = false;
                result = false  ;
            }
        }
    }
}
