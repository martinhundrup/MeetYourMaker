using UnityEngine;

public class Shotgun : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set Z to 0 if you're working in a 2D space

        // Calculate the direction from the object to the mouse
        Vector3 direction = mousePosition - transform.position;

        // Calculate the angle in degrees and rotate the object to face the mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
