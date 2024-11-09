using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float dist = 0.6f;
    private PlayerController player;
    private static LevelEnd instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        instance = this;

        player = FindObjectOfType<PlayerController>();        
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < dist)
        {
            sr.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                instance = null;
                //Destroy(this.gameObject);

                GameEvents.LevelEnd();
            }
        }
        else
        {
            sr.enabled = false;
        }
    } 
}
