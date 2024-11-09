using System.Drawing;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private Hitbox hb;
    [SerializeField] private float speed = 10;
    [SerializeField] private float damage = 1;
    [SerializeField] private float spread = 10;
    [SerializeField] private float size = 1;
    [SerializeField] private bool bounces = false;
    [SerializeField] private bool piercing = false;
    [SerializeField] private float knockbackForce = 0f;
    [SerializeField] private float stunTime = 0f;

    public void Initialize(Vector2 start, Vector2 direction, bool isPlayer)
    {
        if (isPlayer)
        {
            PlayerStats player = DataDictionary.PlayerStats;

            speed = player.BulletSpeed;
            size = player.BulletSize;
            speed = player.BulletSpeed;
            spread = player.BulletSpread;
            damage = player.BulletDamage;
            bounces = player.Bounces;
            piercing = player.Piercing;
            knockbackForce = player.KnockbackForce;
            stunTime = player.StunTime;
        }

        rb = GetComponent<Rigidbody2D>();
        hb = GetComponent<Hitbox>();
        transform.position = start;
        transform.localScale = Vector3.one * size;

        direction.Normalize();

        hb.Damage = damage;
        hb.KnockbackForce = knockbackForce;
        hb.StunTime = stunTime;

        // Generate a random angle offset within the specified spread angle
        float randomAngle = Random.Range(-spread, spread);
        Vector3 spreadDirection = Quaternion.Euler(0, 0, randomAngle) * direction;

        rb.velocity = spreadDirection.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (piercing)
        {
            if (collision.CompareTag("Wall Tilemap") || collision.CompareTag("Top Tilemap"))
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (collision.CompareTag("Enemy") || collision.CompareTag("Obstacle")
                || collision.CompareTag("Wall Tilemap") || collision.CompareTag("Top Tilemap"))
            {
                Destroy(this.gameObject);
            }
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
