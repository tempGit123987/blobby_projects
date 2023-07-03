using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxy : MonoBehaviour
{

    public GameObject player; //player game object
    public SpriteRenderer boxy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.x < player.transform.position.x)
        {
            boxy.flipX = true;
        }
        else
        //if(this.transform.position.x < player.transform.position.x)
        {
            boxy.flipX = false;
        }
    }
}
