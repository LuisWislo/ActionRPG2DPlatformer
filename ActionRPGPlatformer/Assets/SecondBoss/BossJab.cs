using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJab : MonoBehaviour
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
        if (!boss.jabbing || !boss.grounded)
        {
            //boss.jabbing = false;
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
