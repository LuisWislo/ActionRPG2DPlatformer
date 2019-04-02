using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeing : MonoBehaviour
{
    
    public bool canGetGurt;

    //Effects
    public GameObject deathParticle;

    // For Enemy Healthbar
    private float maxScale;
    private float barConstant;
    public Transform healthbar;
    public GameObject wholeBar;
    
    // RPG Stuff
    public int health, attack, defense, expGiven;
    private int maxHealth;

    void Start()
    {
        //health = maxHealth;
        maxHealth = health;
        maxScale = healthbar.localScale.x;
        //Debug.Log("maxLife: "+maxLife);
        barConstant = maxScale / maxHealth;
        canGetGurt = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Attack") && canGetGurt)
        {
            Player ply = FindObjectOfType<Player>();
            health = health - (ply.attack - defense);
            healthbar.localScale -= new Vector3((ply.attack - defense) * barConstant, 0f, 0f);

            if (health <= 0)
            {
                canGetGurt = false;
                Instantiate(deathParticle, transform);
                ply.currExp += expGiven;

                if (ply.currExp >= ply.maxExp)
                {
                    ply.lvl++;
                    ply.currExp = (ply.currExp - ply.maxExp);
                    ply.maxExp = (int)Mathf.Floor(ply.maxExp * 1.2f);
                    ply.attack = (int)Mathf.Floor(ply.attack * 1.2f);
                    ply.defense = (int)Mathf.Floor(ply.defense * 1.2f);
                }

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
        Destroy(wholeBar);
        enabled = false;
        GetComponent<Renderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject.transform.parent.gameObject);

    }
}
