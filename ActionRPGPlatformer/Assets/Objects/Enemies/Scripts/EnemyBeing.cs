using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeing : MonoBehaviour
{
    public GameObject deathParticle;
    public float life;
    private float maxLife;
    private float maxScale;
    private float barConstant;
    public Transform healthbar;
    public GameObject wholeBar;
    public bool canHurt;

    

    void Start()
    {
        maxLife = life;
        maxScale = healthbar.localScale.x;
        //Debug.Log("maxLife: "+maxLife);
        barConstant = maxScale / maxLife;
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
        if (collider.CompareTag("Attack")&&canHurt)
        {
            removeLifePoints(25f);
        }
    }


    
    public void removeLifePoints(float lifePoints)
    {
        //Debug.Log("Enemy got hit!");
        this.life -= lifePoints;
        //Debug.Log("Remaining life: " + life);
        healthbar.localScale -= new Vector3(lifePoints * barConstant, 0f, 0f);

        if(this.life<=0)
        {

            if (name == "Patroller")
            {
                //StopCoroutine(GetComponent<Patroller>().Hit());
                Destroy(wholeBar);
                Instantiate(deathParticle, transform);
                StartCoroutine(GetComponent<Patroller>().Die());
            }
        }
        else
        {
            if (name == "Patroller")
            {
                StartCoroutine(GetComponent<Patroller>().Hit());
            }
            
        }
    }


    

}
