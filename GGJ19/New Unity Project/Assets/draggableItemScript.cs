using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draggableItemScript : MonoBehaviour
{
    public string itemName = "";
    public bool hasBeenFound = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hasBeenFound){
            Debug.Log("item has been found! "+ itemName);
            GameObject.Find(itemName).SetActive(true);
        }
    }
}
