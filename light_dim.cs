using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class light_dim : MonoBehaviour
{

    [SerializeField] private Light lvlLight;
    [SerializeField] private GameObject player;
    [SerializeField] private SpriteRenderer Blobby;
    private Color color;
    public AudioSource splat;
    private bool didSplat;

    // Start is called before the first frame update
    void Start()
    {
        //lvlLight = this.GetComponent<Light>();
        //player = GameObject.Find("player");
        didSplat = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y < 0)
        {
            lvlLight.intensity = (lvlLight.intensity - .0075f);
            color = Blobby.color;
            color.a = color.a - .01f;
            Blobby.color = color;
        }

        if(player.transform.position.y < -80 && didSplat == false)
        {
            splat.Play();
            didSplat = true;
        }

        if(player.transform.position.y < -150)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
