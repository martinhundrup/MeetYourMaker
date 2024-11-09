using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  similar to slime, but constant speed when moving, and more likely to go to player
public class Zombie : Enemy
{

    [SerializeField] protected float jumpInterval; // the base time between jumps.
    [SerializeField] protected float intervalMargin; // the +/- time the interval is randomized to
    private bool isJumping;
    private Animator animator;



    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(JumpTimer());
    }

    // Continuously jump at semi random intervals.
    private IEnumerator JumpTimer()
    {
        while (true)
        {
            var dir = FindDirection();

            yield return new WaitForSeconds(jumpInterval + Random.Range(-intervalMargin, intervalMargin));

            rb.velocity = dir * speed;

            animator.Play("run");
            if (dir.x > 0)
            {
                sr.flipX = true;
            }
            else 
            { 
                sr.flipX = false;
            }

            yield return new WaitForSeconds(jumpInterval + Random.Range(-intervalMargin, intervalMargin));

            animator.Play("idle");
            rb.velocity = Vector2.zero;
        }
    }

    private Vector2 FindDirection()
    {
        if (Random.Range(0, 1f) > aggression)
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        else
            return (player.transform.position - transform.position).normalized;
    }
}
