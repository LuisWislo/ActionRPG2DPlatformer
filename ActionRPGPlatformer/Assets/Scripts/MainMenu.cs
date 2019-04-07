using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private LevelManager manager;
    public Camera cam;
    public Button[] buttons;
    public TextMeshProUGUI[] btnText;
    public TMP_FontAsset[] fontColors;
    
    private int currentSelection;
    private float horizontal;
    private bool canMove = true;
    private Vector3 camInit = new Vector3(-23f, 0f, -10f);
    private Vector3 camEnd = new Vector3(11.81f, 0f, -10f);
    private AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = FindObjectOfType<AudioManager>();
        audio.Play("MenuSong");
        manager = GetComponent<LevelManager>();
        currentSelection = 0;
        cam.transform.position = camInit;
    }

    // Update is called once per frame
    void Update()
    {
        btnText[0].font = fontColors[3];
        btnText[1].font = fontColors[3];
        btnText[2].font = fontColors[3];

        cam.transform.Translate(new Vector3(0.01f,0f,0f),Space.World);
        if (Vector3.Distance(cam.transform.position, camEnd) < 0.01f)
        {
            cam.transform.position = camInit;
        }
        horizontal = Input.GetAxisRaw("Vertical");
        Poll();
        //Debug.Log(currentSelection);
        HighLightButton();
        ButtonSelection();
    }

    private void ButtonSelection()
    {
        if (Input.GetButtonDown("Jump"))
        {
            audio.Stop("MenuSong");
            manager.LoadLevel(currentSelection + 1);
        }
    }

    private void HighLightButton()
    {
        btnText[currentSelection].font = fontColors[currentSelection];

    }



    private void Poll()
    {
        if(horizontal == 1f && canMove)
        {
            StartCoroutine(MoveThroughButtons(true));
        }
        else if(horizontal == -1 && canMove)
        {
            StartCoroutine(MoveThroughButtons(false));
        }
    }


    IEnumerator MoveThroughButtons(bool up)
    {
        canMove = false;

        if (up)
        {
            currentSelection = (int)Mathf.Clamp(--currentSelection, 0, buttons.Length - 1);
            yield return new WaitForSeconds(0.1f);
            canMove = true;
        }
        else
        {
            currentSelection = (int)Mathf.Clamp(++currentSelection, 0, buttons.Length - 1);
            yield return new WaitForSeconds(0.1f);
            canMove = true;
        }

        
    }

}
