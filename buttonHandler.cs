using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playGame()
    {
        //Debug.Log("load level 01");
        SceneManager.LoadScene("level01");
    }

    public void QuitOnClick()
    {
        //Debug.Log("end game");
        Application.Quit();
    }
}
