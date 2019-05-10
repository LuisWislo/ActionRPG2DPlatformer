using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1ChangeLvl : MonoBehaviour
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
        GetComponent<LevelManager>().LoadLevel(4);
    }
}
