using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2Lvl : MonoBehaviour
{
    public GameObject boss;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null)
        {
            StartCoroutine(Lvl());
        }
    }

    IEnumerator Lvl()
    {
        yield return new WaitForSeconds(5);
        Stats stats = FindObjectOfType<Stats>();
        stats.health = 100;
        stats.attack = 20;
        stats.defense = 10;
        stats.lvl = 1;
        stats.currExp = 0;
        stats.maxExp = 100;
        stats.maxHealth = 100;
        GetComponent<LevelManager>().LoadLevel(0);
    }
}
