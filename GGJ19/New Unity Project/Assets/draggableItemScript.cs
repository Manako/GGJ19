using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class draggableItemScript : MonoBehaviour
{
    public bool hasBeenFound = false;

    public string itemName = "";

    [FormerlySerializedAs("startNode")]
    public string talkToNode = "";

    [Header("Optional")]
    public TextAsset scriptToLoad;

    public void FindMe(string name)
    {
        hasBeenFound = true;
        itemName = name;
        Debug.Log("item has been found! " + itemName);
    }
    // Start is called before the first frame update
    void Start() {
            if (scriptToLoad != null) {
                FindObjectOfType<Yarn.Unity.DialogueRunner>().AddScript(scriptToLoad);
            }

    }

    // Update is called once per frame
    void Update() {

    }
}
