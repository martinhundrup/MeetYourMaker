using System;
using System.Collections;
using UnityEngine;
using TMPro;
using System.ComponentModel;


[RequireComponent(typeof(TextMeshProUGUI))]
public class TypewriterEffect : MonoBehaviour
{
    private TextMeshProUGUI textBox;
    [SerializeField] private GameObject idleArrow;

    private void Awake()
    {
        textBox = GetComponent<TextMeshProUGUI>();
        if (idleArrow != null )
            idleArrow.SetActive(false);
    }

    // sets the text, and when input is pressed, erases the text and returns
    public IEnumerator SetTextWaitForInput(string _text)
    {
        //bool typewriterFinished = false;
        yield return StartCoroutine(Typewrite(_text));
        if (idleArrow != null)
            idleArrow.SetActive(true);

        while (!Input.GetButtonDown("Continue"))
        {
            yield return null;
        }
        if (idleArrow != null)
            idleArrow.SetActive(false);
        SFXManager.instance.PlayClick();
        textBox.text = string.Empty;
        yield return null;
    }

    // just sets the text, returns when it's done typing out
    public IEnumerator SetText(string _text)
    {
        yield return StartCoroutine(Typewrite(_text));
    }

    private IEnumerator Typewrite(string _text)
    {
        if (textBox == null) { textBox = GetComponent<TextMeshProUGUI>(); }
        textBox.maxVisibleCharacters = 0;
        textBox.text = _text;
        int maxChars = _text.Length;

        int i = 0;
        for (; i <= maxChars; i++)
        {
            textBox.maxVisibleCharacters = i;
            yield return new WaitForSeconds(0.01f);
        }

        textBox.maxVisibleCharacters = i;
        yield return null;
    }

}
