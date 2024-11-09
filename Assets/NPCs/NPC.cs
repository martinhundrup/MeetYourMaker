using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject bubble;
    [SerializeField] private TypewriterEffect typewriter;
    [SerializeField] private float dist;
    [SerializeField] private List<string> dialogue; // will choose one at random
    private PlayerController player;
    private bool isInRange = false;

    private void Awake()
    {
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
