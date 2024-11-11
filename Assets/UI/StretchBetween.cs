using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchBetween : MonoBehaviour
{
    [SerializeField] private Transform pointA; // The starting point
    private Vector3 pointB; // The ending point
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.drawMode = SpriteDrawMode.Tiled; // Ensure the sprite is in Tiled mode
        }
    }

    private void Update()
    {
        if (pointA != null && spriteRenderer != null)
        {
            pointB = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pointB.z = 0; // Set z to 0 to stay in 2D plane

            // Calculate the direction and distance between the points
            Vector3 direction = pointB - pointA.position;
            float distance = direction.magnitude;

            // Position the object at the midpoint between pointA and pointB
            transform.position = pointA.position + direction / 2;

            // Rotate the object to face pointB in 2D
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Adjust the size of the SpriteRenderer to stretch it between the two points
            spriteRenderer.size = new Vector2(distance, spriteRenderer.size.y);
        }
    }
}
