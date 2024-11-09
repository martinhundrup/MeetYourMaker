using System.Drawing;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float angleRange = 0;
    private Rigidbody2D rb;

    private void Awake()
    {
       // GameStats.OnGameRestart += OnRestart;

        rb = GetComponent<Rigidbody2D>();
        transform.position = GameObject.FindGameObjectWithTag("BulletSpawner").transform.position;
        Debug.Log(transform.position);

        // get mouse pos
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = mousePosition - transform.position;
        direction.Normalize();

        // Generate a random angle offset within the specified spread angle
        float randomAngle = Random.Range(-angleRange, angleRange);
        Vector3 spreadDirection = Quaternion.Euler(0, 0, randomAngle) * direction;

        rb.velocity = spreadDirection.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnRestart()
    {
        //GameStats.OnGameRestart -= OnRestart;
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        //GameStats.OnGameRestart -= OnRestart;
    }
}
