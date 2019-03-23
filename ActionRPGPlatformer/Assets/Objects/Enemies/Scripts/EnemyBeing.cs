using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeing : MonoBehaviour
{
    public GameObject deathParticle;
    private float maxScale;
    private float barConstant;
    public Transform healthbar;
    public GameObject wholeBar;
    public bool canHurt;

    // RPG Stuff
    public int maxHealth, health, attack, defense, expGiven;

    void Start()
    {
        health = maxHealth;
        maxScale = healthbar.localScale.x;
        //Debug.Log("maxLife: "+maxLife);
        barConstant = maxScale / maxHealth;
        canHurt = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.L) && canHurt)
        {
            removeLifePoints(25f);
        }
        Debug.Log("canHurt= " + canHurt);
        //Debug.Log(life);*/
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Attack") && canHurt)
        {
            Player ply = FindObjectOfType<Player>();
            health = health - (ply.attack - defense);
            healthbar.localScale -= new Vector3((ply.attack - defense) * barConstant, 0f, 0f);
            if (health <= 0)
            {
                Destroy(wholeBar);
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
            } else
            {
                if (name == "Patroller")
                {
                    StartCoroutine(GetComponent<Patroller>().Hit());
                }
            }
        }
    }

    public IEnumerator Die()
    {
        //Debug.Log("Enemy dying");
        enabled = false;
        GetComponent<Renderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
