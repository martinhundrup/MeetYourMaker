using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : Enemy
{
    [SerializeField] private GameObject bubble;
    [SerializeField] private TypewriterEffect typewriter;
    [SerializeField] private float dist;
    [SerializeField] private List<string> dialogue; // will choose one at random
    private PlayerController player;
    private bool isInRange = false;

    new private void Awake()
    {
        base.Awake();
        bubble.SetActive(false);
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= dist && !isInRange)
        {
            isInRange = true;
            bubble.SetActive(true);

            StartCoroutine(typewriter.SetText(dialogue[UnityEngine.Random.Range(0, dialogue.Count)]));
        }
        else if (Vector2.Distance(transform.position, player.transform.position) > dist)
        {
            isInRange = false;
            bubble.SetActive(false);
        }
    }
}
