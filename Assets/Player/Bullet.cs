using System.Drawing;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    public void Initialize(Vector2 start, Vector2 direction, int random, float speed)
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = start;
       
        direction.Normalize();

        // Generate a random angle offset within the specified spread angle
        float randomAngle = Random.Range(-random, random);
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
