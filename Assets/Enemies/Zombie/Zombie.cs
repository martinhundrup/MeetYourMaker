using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  similar to slime, but constant speed when moving, and more likely to go to player
public class Zombie : Enemy
{

    [SerializeField] protected float jumpInterval; // the base time between jumps.
    [SerializeField] protected float intervalMargin; // the +/- time the interval is randomized to
    [SerializeField] private bool isGunner = false; // always faces player
    private bool isJumping;
    protected Animator animator;

    new private void OnDestroy()
    {
        base.OnDestroy();
        if (Health <= 0)
            SFXManager.instance.PlayZombieDeath();
    }

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(JumpTimer());
    }

    private void Update()
    {
        if (isGunner)
            sr.flipX = player.transform.position.x > this.transform.position.x;
    }

    // Continuously jump at semi random intervals.
    protected virtual IEnumerator JumpTimer()
    {
        while (true)
        {
            var dir = FindDirection();

            yield return new WaitForSeconds(jumpInterval + Random.Range(-intervalMargin, intervalMargin));

            rb.velocity = dir * speed;

            animator.Play("run");
            if (!isGunner)
            {
                if (dir.x > 0)
                {
                    sr.flipX = true;
                }
                else
                {
                    sr.flipX = false;
                }
            }

            yield return new WaitForSeconds(jumpInterval + Random.Range(-intervalMargin, intervalMargin));

            animator.Play("idle");
            rb.velocity = Vector2.zero;
        }
    }

    protected Vector2 FindDirection()
    {
        if (Random.Range(0, 1f) > aggression)
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        else
            return (player.transform.position - transform.position).normalized;
    }
}
