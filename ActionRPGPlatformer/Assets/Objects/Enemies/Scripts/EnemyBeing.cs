using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeing : MonoBehaviour
{
    
    public bool canGetGurt;
    public bool isProjectile;

    public string hurt;

    //Effects
    public GameObject deathParticle;

    // For Enemy Healthbar
    private float maxScale;
    private float barConstant;
    public Transform healthbar;
    public GameObject wholeBar;

    private AudioManager audio;

    // RPG Stuff
    public int health, attack, defense, expGiven;
    public int maxHealth;

    void Start()
    {
        audio = FindObjectOfType<AudioManager>();
        maxHealth = health;
        maxScale = healthbar.localScale.x;
        barConstant = maxScale / maxHealth;

        
        canGetGurt = true;
    }

    private void UpdatePlayerStats(Player ply)
    {
        if (ply.currExp >= ply.maxExp)
        {
            ply.lvl++;
            ply.currExp = (ply.currExp - ply.maxExp);
            ply.maxExp = (int)Mathf.Floor(ply.maxExp * 1.2f);
            ply.attack = (int)Mathf.Floor(ply.attack * 1.2f);
            ply.defense = (int)Mathf.Floor(ply.defense * 1.2f);
            ply.health = ply.maxHealth;
            ply.healthbar.localScale = new Vector3(maxScale, 1f, 1f);
            ply.UpdateExpBar(0, true);

            if (ply.currExp >= ply.maxExp)
            {
                UpdatePlayerStats(ply);
            }
        }
        else
        {
            ply.UpdateExpBar(expGiven, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((collider.CompareTag("Attack") || collider.CompareTag("plyProjectile")) && canGetGurt)
        {
            audio.Play(hurt);
            Player ply = FindObjectOfType<Player>();
            int damage = 1;
            if (collider.CompareTag("plyProjectile"))
            {
                damage = (int)Mathf.Floor(ply.attack / 2) - defense;
                Destroy(collider.gameObject);
            } else
            {
                damage = ply.attack - defense;
            }
            if (damage <= 0)
            {
                damage = 1;
            }
            health = health - damage;
            if (!isProjectile)
            {
                healthbar.localScale -= new Vector3(damage * barConstant, 0f, 0f);
            }
               

            if (health <= 0)
            {
                canGetGurt = false;
                Instantiate(deathParticle, transform);
                ply.currExp += expGiven;
                UpdatePlayerStats(ply);
                StartCoroutine(Die());
            }

            else
            {
                StartCoroutine(Hit());
            }
        }
    }

    public IEnumerator Hit()
    {
        canGetGurt = false;
        for (int i = 0; i < 5; i++)
        {
            GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(.1f);
            GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(.1f);
        }
        canGetGurt = true;
    }

    public IEnumerator Die()
    {
        if(!isProjectile)
            Destroy(wholeBar);
        enabled = false;
        GetComponent<Renderer>().enabled = false;
        if(!isProjectile)
            GetComponent<BoxCollider2D>().enabled = false;
        else
        {
            GetComponent<CircleCollider2D>().enabled = false;
        }
        yield return new WaitForSeconds(1);
        if (isProjectile)
            Destroy(gameObject);
        else
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
