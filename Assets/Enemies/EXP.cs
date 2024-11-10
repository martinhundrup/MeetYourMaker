using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP : MonoBehaviour
{

    [SerializeField] private GameObject indicator;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private bool hasInit = false;
    private Transform playerTransform;

    public float attractionDistance = 5f; // Distance within which the EXP moves towards the player
    public float attractionSpeed = 2f;    // Speed at which the EXP moves towards the player

    private void Awake()
    {
        float seed = (UnityEngine.Random.Range(0, 201) - 100) / 100f;

        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.mass = 0.0001f;
        rb.drag = 0;
        rb.angularDrag = 0;
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        rb.AddForce((seed * Vector2.one).normalized * 0.05f);
        hasInit = true;

        // Find the player in the scene by tag or set playerTransform manually if you have a reference
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void FixedUpdate()
    {
        if (hasInit)
        {
            rb.velocity = rb.velocity * 0.8f;

            if (playerTransform != null)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

                // If within attraction distance, move towards the player
                if (distanceToPlayer < attractionDistance)
                {
                    Vector2 direction = (playerTransform.position - transform.position).normalized;
                    rb.velocity = direction * attractionSpeed;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DataDictionary.PlayerStats.EXP++;
            var obj = Instantiate(indicator);
            obj.transform.position = this.transform.position;
            Destroy(gameObject);
        }
    }
}