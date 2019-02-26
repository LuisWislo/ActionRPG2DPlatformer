using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] bool isFast;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Action());
    }

    IEnumerator Action()
    {
        float wait = isFast ? 0.2f : 0.4f;
        yield return new WaitForSeconds(wait);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" || collision.tag == "Projectile")
        {
            Destroy(collision.gameObject);
        }
    }
}
