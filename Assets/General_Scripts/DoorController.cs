using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private RoomController roomController;
    private Collider2D _collider;
    private Animator animator;

    private void Awake()
    {
        roomController = GetComponentInParent<RoomController>();
        roomController.OnPlayerEnteredRoom += PlayerEnter;

        _collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        _collider.enabled = false;
    }

    private void PlayerEnter(bool _entered)
    {
        if (_entered)
        {
            animator.Play("close");
            _collider.enabled = true;
        }
        else
        {
            animator.Play("open");
            _collider.enabled = false;
            GetComponent<SpriteRenderer>().sortingOrder -= 1;
        }
        
    }
}
