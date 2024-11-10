using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        StartCoroutine(OnDeath());
    }

    IEnumerator OnDeath()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
