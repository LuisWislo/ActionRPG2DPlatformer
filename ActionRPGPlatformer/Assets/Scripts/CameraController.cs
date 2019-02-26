using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
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
            if(transform.position.x - player.transform.position.x > 6)
            {
                transform.position = new Vector3(transform.position.x - cameraSpeed, transform.position.y, transform.position.z);
            } else if (transform.position.x - player.transform.position.x < 0)
            {
                transform.position = new Vector3(transform.position.x + cameraSpeed, transform.position.y, transform.position.z);
            }

            if (transform.position.y - player.transform.position.y > 2)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 3 * cameraSpeed, transform.position.z);
            }
            else if (transform.position.y - player.transform.position.y < 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + cameraSpeed, transform.position.z);
            }

            /*if (transform.position.x - player.transform.position.x > 6)
            {
                transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * cameraSpeed);
            }
            else if (transform.position.x - player.transform.position.x < 0)
            {
                transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * cameraSpeed);
            }

            if (transform.position.y - player.transform.position.y > 2)
            {
                transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * cameraSpeed);
            }
            else if (transform.position.y - player.transform.position.y < 0)
            {
                transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * cameraSpeed);
            }*/
        }
    }

    public void SetRespawnLoc()
    {
        transform.position = player.transform.position + new Vector3(6f, 0f, -10f);
    }
}
