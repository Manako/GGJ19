using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transitionhome : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var finder = Resources.FindObjectsOfTypeAll<GameObject>();
        var notfound = false;
        foreach (var findy in finder)
        {
            if (findy.tag == "Findable")
            {
                if (!findy.GetComponent<draggableItemScript>().hasBeenFound)
                {
                    notfound = true;
                    break;
                }
            }
        }
        if (notfound)
        {
            SceneManager.LoadScene("End2");
        }
        else
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
