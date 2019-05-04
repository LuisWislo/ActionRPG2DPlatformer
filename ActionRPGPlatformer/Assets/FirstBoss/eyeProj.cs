using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeProj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10, ForceMode2D.Impulse);
        StartCoroutine(Die());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
