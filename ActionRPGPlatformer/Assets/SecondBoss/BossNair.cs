using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNair : MonoBehaviour
{
    private SecondBoss boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = FindObjectOfType<SecondBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((boss.grounded && boss.GetComponent<Rigidbody2D>().velocity.y <= 0) || !boss.nairing)
        {
            //boss.nairing = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().takeDamage(boss.self.attack);
        }
    }
}
