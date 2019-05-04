using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(int index)
    {
        Debug.Log("load something");
        switch (index)
        { 
            case 1: SceneManager.LoadScene("Demo"); break;
            case 2: SceneManager.LoadScene("Settings"); break;
            case 3: SceneManager.LoadScene("Credits"); break;
            case 4: SceneManager.LoadScene("FirstBoss"); break;
        }
    }
}
