using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class credits_screen : MonoBehaviour
{
    private bool canExit = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreditCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey && canExit == true)
        {
            //SceneManager.LoadScene("main_menu");
        }
    }

    IEnumerator CreditCountdown()
    {
        yield return new WaitForSeconds(35);
        SceneManager.LoadScene("main_menu");
        canExit = true;
    }
}
