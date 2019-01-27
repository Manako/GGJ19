using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> platforms;
    public GameObject ground;
    private BoxCollider2D playerBox;
    private List<BoxCollider2D> platformBoxes;
    private BoxCollider2D groundBox;

    //movement variables
    public int playerSpeedY = 1200;
    public float playerSpeedX = 1.0f;
    private float playerMoveX;


    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        ground = GameObject.Find("ground");
        playerBox = player.GetComponent<BoxCollider2D>();
        groundBox = ground.GetComponent<BoxCollider2D>();
        platforms = new List<GameObject>();
        platformBoxes = new List<BoxCollider2D>();
        foreach (GameObject go in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            Debug.Log(go.name);
            if (go.name == "platform")
            {
                platforms.Add(go);
                platformBoxes.Add(go.GetComponent<BoxCollider2D>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        // controls
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if ((playerBox.IsTouching(groundBox)))
            {
                Jump();
            }
            foreach (var platformBox in platformBoxes)
            {
                if (playerBox.IsTouching(platformBox))
                {
                    Jump();
                    break;
                }
            }
        }
        // Move the player, clamping them to within the boundaries 
        // of the level.
        var playerMoveX = Input.GetAxis("Horizontal");
        playerMoveX *= (playerSpeedX * Time.deltaTime);

        var newPosition = transform.position;
        newPosition.x += playerMoveX;
        animator.Play("pip_walk");
        transform.position = newPosition;


        // player direction

        // physics
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(playerMoveX * playerSpeedX, player.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerSpeedY);
        animator.Play("pip_jump", 0, 1);
    }
}
