using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornDrop : Acorn
{
    new protected void Awake()
    {
        base.Awake();

        float seed = (UnityEngine.Random.Range(0, 201) - 100) / 100f;

        rb.mass = 0.0001f;
        rb.drag = 0;
        rb.angularDrag = 0;
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        rb.AddForce((seed * Vector2.one).normalized * 0.05f);
    }
}
