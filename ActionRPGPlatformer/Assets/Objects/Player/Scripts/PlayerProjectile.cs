using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Die(3.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Die(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            StopAllCoroutines();
            StartCoroutine(Die(0.1f));
        }
    }
}
