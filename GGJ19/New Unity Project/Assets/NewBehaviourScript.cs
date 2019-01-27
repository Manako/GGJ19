using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
  public GameObject player;
  public GameObject platform;
  public GameObject ground;
  private BoxCollider2D playerBox;
  private BoxCollider2D platformBox;
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
    player      = GameObject.Find("Player");
    platform    = GameObject.Find("platform");
    ground      = GameObject.Find("ground");
    playerBox   = player.GetComponent<BoxCollider2D>();
    platformBox = platform.GetComponent<BoxCollider2D>();
    groundBox   = ground.GetComponent<BoxCollider2D>();
  }

  // Update is called once per frame
  void Update()
  {
    PlayerMovement();
  }

  void PlayerMovement() {
    // controls
    if (Input.GetKeyDown(KeyCode.UpArrow)) {
      if ( (playerBox.IsTouching(platformBox)) || (playerBox.IsTouching(groundBox)) ) {
        Jump();
      }
    }
    // Move the player, clamping them to within the boundaries 
    // of the level.
    var playerMoveX = Input.GetAxis("Horizontal");
    playerMoveX *= (playerSpeedX * Time.deltaTime);

    var newPosition = transform.position;
    newPosition.x += playerMoveX;
   animator.SetInteger("animState", 1);
   transform.position = newPosition;


    // player direction

    // physics
    player.GetComponent<Rigidbody2D>().velocity = new Vector2 (playerMoveX * playerSpeedX, player.GetComponent<Rigidbody2D>().velocity.y);
  }

  void Jump() {
    GetComponent<Rigidbody2D>().AddForce(Vector2.up*playerSpeedY);
    animator.SetInteger("animState", 2);
    }
}
