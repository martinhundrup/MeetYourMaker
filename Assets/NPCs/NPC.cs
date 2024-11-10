using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPC : Enemy
{
    [SerializeField] private GameObject bubble;
    private TypewriterEffect typewriter;
    [SerializeField] private float dist;
    [SerializeField] private List<string> dialogue; // will choose one at random
    private bool isInRange = false;

    new private void Awake()
    {
        base.Awake();
        typewriter = GetComponentInChildren<TypewriterEffect>();
        bubble.SetActive(false);
        player = FindObjectOfType<PlayerController>();
        GetComponentInChildren<Canvas>().enabled = true;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= dist && !isInRange)
        {
            isInRange = true;
            bubble.SetActive(true);

            StartCoroutine(typewriter.SetText(dialogue[UnityEngine.Random.Range(0, dialogue.Count)]));
            LayoutRebuilder.ForceRebuildLayoutImmediate(bubble.transform as RectTransform);
        }
        else if (Vector2.Distance(transform.position, player.transform.position) > dist)
        {
            isInRange = false;
            bubble.SetActive(false);
        }
    }
}
