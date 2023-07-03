using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* this script lets a platform move vertically between two points.
Can set individual start and end points as well as speed.

attach this script to the platform you wish to move. */

public class platformVer : MonoBehaviour
{

    public float speed = 1.0f;
    public float minY = 0.0f;
    public float maxY = 1.0f;
    private float _timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        this.transform.position = new Vector3(transform.position.x, Mathf.Lerp(minY, maxY, Mathf.PingPong(_timer * speed, 1)), transform.position.z);
    }
}
