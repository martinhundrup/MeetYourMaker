using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornDrop : Acorn
{
    private static readonly System.Random globalRandom = new System.Random();
    new protected void Awake()
    {
        base.Awake();

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
}
