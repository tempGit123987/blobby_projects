using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credit_roll : MonoBehaviour
{

    public GameObject credits;
    // Start is called before the first frame update
    void Start()
    {
        //credits.transform.position.y = -400;
    }

    // Update is called once per frame
    void Update()
    {
        if(credits.transform.position.y == -400)
        {
            Debug.Log("Test");
        }
    }
}
