using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Door : MonoBehaviour
{
    private AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = FindObjectOfType<AudioManager>();
        audio.Stop("FirstBoss");
        audio.Play("SecondLevel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<Player>().hasKey)
            {
                FindObjectOfType<LevelManager>().LoadLevel(5);
            }
        }
    }
}
