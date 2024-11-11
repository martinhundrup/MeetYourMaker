using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    protected Rigidbody2D rb;
    private Transform playerTransform;
    [SerializeField] private float attractionDistance = 2;
    [SerializeField] private float attractionSpeed = 2;
    private PlayerStats playerStats;
    private static readonly System.Random globalRandom = new System.Random();

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = FindObjectOfType<PlayerController>().transform;
        playerStats = DataDictionary.PlayerStats;

        // Generate a random angle in radians
        float randomAngle = (float)(globalRandom.NextDouble() * 2 * Mathf.PI);

        // Calculate a random direction vector using the angle
        Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;

        rb.mass = 0.0001f;
        rb.drag = 0;
        rb.angularDrag = 0;
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Apply force in the random direction
        rb.AddForce(randomDirection * 0.05f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerStats.PlayerHealth < playerStats.PlayerMaxHealth)
        {
            SFXManager.instance.PlayAmmoCollect();
            DataDictionary.PlayerStats.PlayerHealth += 1;
            var obj = Instantiate(indicator);
            obj.transform.position = this.transform.position;
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {

        rb.velocity = rb.velocity * 0.8f;

        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            // If within attraction distance, move towards the player
            if (distanceToPlayer < attractionDistance && playerStats.PlayerHealth < playerStats.PlayerMaxHealth)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                rb.velocity = direction * attractionSpeed;
            }
        }
    }
}
