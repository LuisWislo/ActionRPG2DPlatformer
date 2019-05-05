using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSecondBoss : MonoBehaviour
{
    private Player player;
    public float cameraSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.enabled)
        {
            if (player.transform.position.x - transform.position.x > 2)
            {
                if (transform.position.x >= 13)
                {
                    transform.position = new Vector3(13, 0, -10);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x + cameraSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                }
            } else if(player.transform.position.x - transform.position.x < -2)
            {
                if (transform.position.x <= 0)
                {
                    transform.position = new Vector3(0, 0, -10);
                } else
                {
                    transform.position = new Vector3(transform.position.x - cameraSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                }
            }
        }
    }

    public void SetRespawnLocation()
    {
        transform.position = new Vector3(0, 0, -10);
    }
}
