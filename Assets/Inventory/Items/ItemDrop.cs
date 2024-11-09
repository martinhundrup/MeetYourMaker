using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// an in-world item
public class ItemDrop : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private ItemData itemData;
    private static readonly System.Random globalRandom = new System.Random();
    private bool hasInit = false;

    public ItemData ItemData
    {
        get { return itemData; }
    }

    public void Init(ItemData _item)
    {
        float seed = (globalRandom.Next(0, 201) - 100) / 100f;

        itemData = _item;
        sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = _item.Sprite;

        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.mass = 0.0001f;
        rb.drag = 0;
        rb.angularDrag = 0;
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        var col = gameObject.AddComponent<CircleCollider2D>();
        col.radius = 0.25f;

        rb.AddForce((seed * Vector2.one).normalized * 0.05f);
        hasInit = true;
    }

    private void FixedUpdate()
    {
        if (hasInit) 
            rb.velocity = rb.velocity * 0.8f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameEvents.ItemPickedUp(itemData);
            Destroy(gameObject);
        }
    }
}
