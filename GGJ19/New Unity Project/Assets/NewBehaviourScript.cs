using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
  public GameObject player;
  public GameObject platform;
  private BoxCollider2D playerBox;
  private BoxCollider2D platformBox;

  //movement variables
  public int playerSpeedY = 1200;
  public float playerSpeedX = 1.0f;
  private float playerMoveX;

  // Start is called before the first frame update
  void Start()
  {
    player      = GameObject.Find("Player");
    platform    = GameObject.Find("platform"); // platform is also the ground
    playerBox   = player.GetComponent<BoxCollider2D>();
    platformBox = platform.GetComponent<BoxCollider2D>();
  }

  // Update is called once per frame
  void Update()
  {
    PlayerMovement();
  }

  void PlayerMovement() {
    // controls
    if (Input.GetKeyDown(KeyCode.UpArrow)) {
      if (playerBox.IsTouching(platformBox)) {
        Jump();
      }
    }
    // Move the player, clamping them to within the boundaries 
    // of the level.
    var playerMoveX = Input.GetAxis("Horizontal");
    playerMoveX *= (playerSpeedX * Time.deltaTime);

    var newPosition = transform.position;
    newPosition.x += playerMoveX;

    transform.position = newPosition;


    // player direction

    // physics
    player.GetComponent<Rigidbody2D>().velocity = new Vector2 (playerMoveX * playerSpeedX, player.GetComponent<Rigidbody2D>().velocity.y);
  }

  void Jump() {
    GetComponent<Rigidbody2D>().AddForce(Vector2.up*playerSpeedY);
  }
}
