using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private bool explode;

    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        explode = false;
        StartCoroutine(ExplodeTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (explode)
        {
            if (col.tag.Equals("Player"))
            {
                col.GetComponent<Player>().takeDamage(damage);
            }
        }
    }

    IEnumerator ExplodeTimer()
    {
        yield return new WaitForSeconds(2);
        explode = true;
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
