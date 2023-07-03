using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This scripts parents the player to the platform he is landing on, this is done
to make sure the player moves with the platform.

add this script to the jumpground plane on plaforms. */

public class platformMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "blob")
        {
            other.transform.parent = transform;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "blob")
        {
            other.transform.parent = null;
        }
    }
}
