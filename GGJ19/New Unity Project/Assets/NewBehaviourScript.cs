using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject player;
    public GameObject platform;
    private BoxCollider2D playerBox;
    private BoxCollider2D platformBox;
    // Start is called before the first frame update
    void Start()
    {
        player      = GameObject.Find("Player");
        platform    = GameObject.Find("platform");
        playerBox   = player.GetComponent<BoxCollider2D>();
        platformBox = platform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (playerBox.IsTouching(platformBox))
            {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,500));
            }
        }
    }
}
