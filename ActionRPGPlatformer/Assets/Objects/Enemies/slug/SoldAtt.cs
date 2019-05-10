using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldAtt : MonoBehaviour
{
    private EnemyBeing self;

    // Start is called before the first frame update
    void Start()
    {
        self = GameObject.Find("soldier").GetComponent<EnemyBeing>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().takeDamage(self.attack);
        }
    }
}
