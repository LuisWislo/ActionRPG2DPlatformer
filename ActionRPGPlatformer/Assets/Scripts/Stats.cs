using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int health, attack, defense, lvl, currExp, maxExp, maxHealth;
    private Player ply;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        ply = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        health = ply.health;
        attack = ply.attack;
        defense = ply.defense;
        lvl = ply.lvl;
        currExp = ply.currExp;
        maxExp = ply.maxExp;
        maxHealth = ply.maxHealth;
    }
}
