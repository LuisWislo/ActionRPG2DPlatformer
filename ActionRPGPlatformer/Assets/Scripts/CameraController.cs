﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Player player;
    private float distance;
    public float maxDist;
    public float moveTowSpeed;
    public float yOffset;

    public float cameraSpeed;
    private Vector3 vel = new Vector3(1f, 1f, 1f);

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
            distance = Mathf.Sqrt(((player.transform.position.x - transform.position.x) * (player.transform.position.x - transform.position.x)) + ((player.transform.position.y - transform.position.y) * (player.transform.position.y - transform.position.y)));
            if (distance > maxDist)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position - new Vector3(0, yOffset, 10), moveTowSpeed * Time.deltaTime);
            }


            /*
            if(transform.position.x - player.transform.position.x > 1.5f)
            {
                transform.position = new Vector3(transform.position.x - cameraSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                //transform.position = Vector3.SmoothDamp(transform.position, target, ref vel, 0.02f);
            } else if (transform.position.x - player.transform.position.x < 1)
            {
                transform.position = new Vector3(transform.position.x + cameraSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }

            if (transform.position.y - player.transform.position.y > 1.5f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 1 * cameraSpeed * Time.deltaTime, transform.position.z);
            }
            else if (transform.position.y - player.transform.position.y < 1)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + cameraSpeed * Time.deltaTime, transform.position.z);
            }*/

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
        transform.position = player.transform.position + new Vector3(4, 4, -10f);
    }
}
