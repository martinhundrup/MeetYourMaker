using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] GameObject fadingCanvas;
    [SerializeField] List<GameObject> elements = new List<GameObject>();
    public static HUDController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this);

        fadingCanvas.SetActive(false);
    }

    public void EnableHUD(bool _bool)
    {
        foreach (var element in elements)
        {
            element.SetActive(_bool);
        }
    }

    public void DisableFadeCanvas()
    {
        fadingCanvas.SetActive(false);
    }

    public IEnumerator FadeToClear()
    {
        fadingCanvas.SetActive(true);
        fadingCanvas.GetComponent<Animator>().Play("FadeToClear");
        yield return new WaitForSeconds(0.35f);
        fadingCanvas.SetActive(false);
    }

    public IEnumerator FadeToOpaque()
    {
        fadingCanvas.SetActive(true);
        fadingCanvas.GetComponent<Animator>().Play("FadeToOpaque");
        yield return new WaitForSeconds(0.35f);
    }
}
