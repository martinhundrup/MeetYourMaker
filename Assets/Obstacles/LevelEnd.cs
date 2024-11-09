using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float dist = 0.6f;
    private PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameEvents.LevelEnd();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Vector2.Distance(transform.position, player.transform.position) < dist)
        {
            sr.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                GameEvents.LevelEnd();
            }
        }
        else
        {
            sr.enabled = false;
        }
    }

 
}
