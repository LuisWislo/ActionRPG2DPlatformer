using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float startConstraint;
    [SerializeField] float endConstraint;
    Vector3 camPos;


    // Start is called before the first frame update
    void Start()
    {
        camPos = new Vector3(target.position.x, -4f, -10f);
    }

    // Update is called once per frame
    void Update()
    {
        camPos.x = Mathf.Clamp(target.position.x, startConstraint, endConstraint) + 1;
        transform.position = camPos;

    }
}
