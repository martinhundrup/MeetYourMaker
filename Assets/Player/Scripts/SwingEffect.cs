using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingEffect : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sr;

    private bool isSwinging;

    [SerializeField] private float distanceFromCenter = 2.0f; // Set the distance from the center
    [SerializeField] private float orbitSpeed = 5.0f;         // Speed of orbiting
    [SerializeField] private GameObject hitbox;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        animator.Play("Rest");
    }

    private void Update()
    {
        if (!isSwinging)
        {
            OrbitAroundLocalZero();
            RotateOutwardsFromCenter();
        }
    }

    public void Swing(WeaponData weapon)
    {
        StartCoroutine(SwingCoroutine(weapon));
    }

    private IEnumerator SwingCoroutine(WeaponData weapon)
    {
        animator.Play("Swing");
        isSwinging = true;
        var _hitbox = Instantiate(hitbox, this.transform);
        _hitbox.transform.localPosition = Vector2.zero;
        _hitbox.GetComponent<Hitbox>().SetStats(weapon);

        yield return new WaitForSeconds(weapon.Duration);

        animator.Play("Rest");
        Destroy(_hitbox);
        isSwinging = false;
    }

    void OrbitAroundLocalZero()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = (mousePosition - transform.parent.position).normalized;
        Vector3 targetPosition = direction * distanceFromCenter;

        float angle = orbitSpeed * Time.deltaTime;
        Vector3 orbitPosition = Quaternion.Euler(0, 0, angle) * targetPosition;
        transform.localPosition = orbitPosition;
    }

    void RotateOutwardsFromCenter()
    {
        Vector3 direction = transform.localPosition.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 180);
    }
}
