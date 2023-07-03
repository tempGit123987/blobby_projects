using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* this script (currently) handles the bulk of the game.

included things are:
player movement (jump and left/right)
a check if the player goes below a certain point (specifically a certain y location in world space)
the counter for collectables (including how many based on what level is loaded),
detection for hitting the collectables and warp point 

*/

public class blob_movement : MonoBehaviour
{

    public float speed = 5.0f; //speed of movement
    public float jumpForce = 5.0f; //the vertical jump force
    public Rigidbody blob; //blobby's rigidbody
    private bool isGrounded; //bool to check if the player is grounded
    private bool isLastLVL = false; //bool to check if its the last level
    private bool isPaused = false; //bool to check if the game is paused
    public GameObject quitButton; // canvas game object of the quit menu
    

    public Vector3 jump;

    private GameObject testText1; //unused, removed?
    private GameObject testText2; //unused, removed
    private GameObject collection; //game object of players amount collected
    private  int totalLVLcollect; //number of level collectables

    private byte collectCounter = 0; //players current collectable number

    public AudioSource collectableSFX; //audio for collecting collectables
    public Animator animator; //blobby's animator

    private float distanceGround; //distance to the ground

    public SpriteRenderer Blobby;

    // Start is called before the first frame update
    void Start()
    {
        blob = GetComponent<Rigidbody>(); //sets blob to the rigidbody of player
        jump = new Vector3(0.0f, 2.0f, 0.0f); //jump force applied to vertical position
        testText1 = GameObject.Find("testText01"); //not used, remove?
        testText2 = GameObject.Find("testText02"); //not used, remove?
        collection = GameObject.Find("collection"); //text box for collectables
        isGrounded = true; //sets player to be grounded

        //sets the distance to the ground from the bottom of the collider (character)
        distanceGround = GetComponent<Collider>().bounds.extents.y;
        
        //figures out the level and sets the total amount of collecatable needed
        if(SceneManager.GetActiveScene().name == "level01" || SceneManager.GetActiveScene().name == "level02" ||
            SceneManager.GetActiveScene().name == "level03" || SceneManager.GetActiveScene().name == "level04" ||
            SceneManager.GetActiveScene().name == "level05")
        {
            totalLVLcollect = 5;
            collection.GetComponent<Text>().text = collectCounter.ToString() + "/" + totalLVLcollect;
        }
        else if (SceneManager.GetActiveScene().name == "level06" || SceneManager.GetActiveScene().name == "level07"
                || SceneManager.GetActiveScene().name == "level08" || SceneManager.GetActiveScene().name == "level09"
                || SceneManager.GetActiveScene().name == "level10")
        {
            totalLVLcollect = 10;
            collection.GetComponent<Text>().text = collectCounter.ToString() + "/" + totalLVLcollect;
        }
        else if (SceneManager.GetActiveScene().name == "level11" || SceneManager.GetActiveScene().name == "level12"
                || SceneManager.GetActiveScene().name == "level13" || SceneManager.GetActiveScene().name == "level14"
                || SceneManager.GetActiveScene().name == "level15")
        {
            totalLVLcollect = 15;
            collection.GetComponent<Text>().text = collectCounter.ToString() + "/" + totalLVLcollect;
        }
        else if(SceneManager.GetActiveScene().name == "level16")
        {
            totalLVLcollect = -1;
            isLastLVL = true;
            collection.GetComponent<Text>().text = "-------";
            animator.SetBool("isLastLevel", true);
        }
        
        animator.SetBool("isFalling", false);
        animator.SetBool("isRising", false);

    }

    void FixedUpdate()
    {
        //handles the left and right input of Blobby
        if(Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * speed); //move right
        }

        if(Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * Time.deltaTime * speed); //move left
        }
    }

    //creates a Raycast to check if the player is colliding with anything below it
    //if so, they can jump
    void CheckGroundStatus() {
        if(Physics.Raycast(transform.position, Vector3.down, distanceGround + 0.1f))
        {
            animator.SetBool("isRising", true);
            animator.SetBool("isFalling", false);
            blob.AddForce(jump * jumpForce, ForceMode.Impulse); //jump
        }


    }

    // Update is called once per frame
    void Update()
    {
        //checks if the player is falling
        if(blob.velocity.y < 0)
        {
            animator.SetBool("isFalling", true); //falling animation plays
            animator.SetBool("isRising", false); //makes sure rising animation doesn't play
        }
        if(blob.velocity.y == 0) //if they are not falling
        {
            animator.SetBool("isFalling", false); //make sure falling animation isn't playing
            animator.SetBool("isRising", false); //same as above but as rising animation
        }

        //used for animation
        animator.SetFloat("Speed", 0); //idle animation

        if(Input.GetKey(KeyCode.D) && !isPaused) //used for playing movement animation
        {
            animator.SetFloat("Speed", 1);
            Blobby.flipX = false;
            
        }

        if(Input.GetKey(KeyCode.A) && !isPaused) //used for playing movement animation
        {
            animator.SetFloat("Speed", 1);
            Blobby.flipX = true;
            
        }

        if(Input.GetKeyDown(KeyCode.Space) && isPaused == false)
        {
            CheckGroundStatus(); //checks if the player is grounded
        }

        //reloads the level if R is hit AND its not the last level
        if(Input.GetKey(KeyCode.R) && !isLastLVL)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }

        //reloads current level if the player falls below a certain point AND is not the last level
        if(this.transform.position.y < -15.0f && !isLastLVL)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }

        //changes the counter to a different text when the total amount is collected
        if(collectCounter == totalLVLcollect && isPaused == false)
        {
            collection.GetComponent<Text>().text = "Go To Goal!";
        }

        //pause menu
        if(Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            Time.timeScale = 0;
            collection.GetComponent<Text>().text = "Paused";
            isPaused = true;
            quitButton.SetActive(true);

        } else if(Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            Time.timeScale = 1;
            if(SceneManager.GetActiveScene().name != "level16")
            {
            collection.GetComponent<Text>().text = collectCounter.ToString() + "/" + totalLVLcollect;
            }
            else
            {
                collection.GetComponent<Text>().text = "-------";
            }
            isPaused = false;
            quitButton.SetActive(false);
        }

       
    }

    /*originaly used for the jumping but switched for raycast, this is now only
    used for checking if the player is on a moving platform.
    */
    void OnCollisionEnter(Collision ground)
    {        

        if(ground.gameObject.tag == "jumpground")
        {
            isGrounded = true;   
        }
        
    }

    void OnCollisionLeave()
    {
        isGrounded = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        //when player picks up a collectable, adds one to the counter, destroys collectable
        //and updates the display.
        if(collider.gameObject.tag == "collect")
        {
            collectCounter++;
            collectableSFX.Play();
            Destroy(collider.gameObject);
            collection.GetComponent<Text>().text = collectCounter.ToString() + "/" + totalLVLcollect;
        }


        //if player touches the end level warp AND has all collectables
        //warps to next level CURRENTLY ONLY DISPLAYS TEXT
        if(collider.gameObject.tag == "endWarp" && collectCounter == totalLVLcollect)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

   

}
