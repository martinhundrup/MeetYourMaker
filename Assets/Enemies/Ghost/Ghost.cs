using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    [SerializeField] protected float interval; // the base time between jumps.
    [SerializeField] protected float randomness; // the +/- time the interval is randomized to
    private Animator animator;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(TransparencyTimer());
    }

    private void Update()
    {
        rb.velocity = FindDirection() * speed;

        GetComponent<SpriteRenderer>().flipX = rb.velocity.x > 0;
    }

    // Continuously jump at semi random intervals.
    private IEnumerator TransparencyTimer()
    {
        while (true)
        {
            var dir = FindDirection();

            yield return new WaitForSeconds(interval + Random.Range(-randomness, randomness));

            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);

            yield return new WaitForSeconds(interval + Random.Range(-randomness, randomness));

            GetComponent<Collider2D>().enabled = true;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private Vector2 FindDirection()
    {
        return (player.transform.position - transform.position).normalized;
    }
}
