using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    private bool hasStarted = false;
    private bool ready = false;
    [SerializeField] private TypewriterEffect text;
    [SerializeField] private TypewriterEffect menuText;
    [SerializeField] private TypewriterEffect subtitleText;

    private void Awake()
    {
        StartCoroutine(Intro());
    }

    private IEnumerator Intro()
    {
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(menuText.SetText("shroomwood"));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(subtitleText.SetText("rip and tEar"));
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(text.SetText("press left click to continue"));
        ready = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire") && ready && !hasStarted)
        {
            hasStarted = true;
            StartCoroutine(OnStartPressed());
        }
    }
    public IEnumerator OnStartPressed()
    {
        text.gameObject.SetActive(false);
        SFXManager.instance.PlayFire();
        yield return new WaitForSeconds(0.5f);
        GameEvents.GameStart();
    }
}
