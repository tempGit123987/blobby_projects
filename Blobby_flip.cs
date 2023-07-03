using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blobby_flip : MonoBehaviour
{

    public SpriteRenderer charBlobby;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            charBlobby.flipX = false;
        }

        if(Input.GetKey(KeyCode.A))
        {
            charBlobby.flipX = true;
        }
    }
}
