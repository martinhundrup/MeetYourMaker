using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    private bool hasStarted = false;
    [SerializeField] private TypewriterEffect text;

    private void Awake()
    {
        text.SetText("press left click to continue");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire"))
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
