/*

The MIT License (MIT)

Copyright (c) 2015-2017 Secret Lab Pty. Ltd. and Yarn Spinner contributors.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Yarn.Unity.Example {
    public class PlayerCharacter : MonoBehaviour {

        public float moveSpeed = 1.0f;

        public float interactionRadius = 2.0f;

        public float movementFromButtons {get;set;}

        /// Draw the range at which we'll start talking to people.
        void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;

            // Flatten the sphere into a disk, which looks nicer in 2D games
            Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, new Vector3(1,1,0));

            // Need to draw at position zero because we set position in the line above
            Gizmos.DrawWireSphere(Vector3.zero, interactionRadius);
        }

        /// Update is called once per frame
        void Update () {
            var animator = GetComponent<Animator>();

            // Remove all player control when we're in dialogue
            if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true) {
                return;
            }

            // Move the player, clamping them to within the boundaries 
            // of the level.
            var movement = Input.GetAxis("Horizontal");
            var curAnim =animator.GetCurrentAnimatorStateInfo(0);
            if (movement<0) {
                GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX=true;
                if(!curAnim.IsName("pip_walk") && !curAnim.IsName("pip_jump"))
                   animator.Play("pip_walk", 0, 1);
            }
            if (movement>0) {
                GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX=false;
                if (!curAnim.IsName("pip_walk") && !curAnim.IsName("pip_jump"))
                    animator.Play("pip_walk", 0, 1);
            }
            movement += movementFromButtons;
            movement *= (moveSpeed * Time.deltaTime);

            var newPosition = transform.position;
            newPosition.x += movement;

            transform.position = newPosition;

            // Detect if we want to start a conversation
            if (Input.GetKeyDown(KeyCode.Space)) {
                CheckForNearbyNPC ();
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                CheckForNearbyItem ();
            }
        }

        // Check for dialogue item
        public void CheckForNearbyItem() {
            var allItems = new List<draggableItemScript> (FindObjectsOfType<draggableItemScript> ());
            var target = allItems.Find (delegate (draggableItemScript p) {
                return string.IsNullOrEmpty (p.talkToNode) == false && // has a conversation node?
                (p.transform.position - Input.mousePosition)// is in range?
                .magnitude <= interactionRadius;
            });

            if (target != null)
            {
                // Kick off the dialogue at this node.
                FindObjectOfType<DialogueRunner>().StartDialogue(target.talkToNode);

            }
        }

        /// Find all DialogueParticipants
        /** Filter them to those that have a Yarn start node and are in range; 
         * then start a conversation with the first one
         */
        public void CheckForNearbyNPC ()
        {
            var allParticipants = new List<NPC> (FindObjectsOfType<NPC> ());
            var target = allParticipants.Find (delegate (NPC p) {
                return string.IsNullOrEmpty (p.talkToNode) == false && // has a conversation node?
                (p.transform.position - this.transform.position)// is in range?
                .magnitude <= interactionRadius;
            });
            
            // set target as inactive

            if (target != null)
            {
                // Kick off the dialogue at this node.
                FindObjectOfType<DialogueRunner>().StartDialogue(target.talkToNode);

            
                string targetName = target.name;
                char[] delimiters = { '(', ')', ' '};
                string[] words = targetName.Split(delimiters);
                string dragTargetName = words[3];
                GameObject targ = GameObject.Find(targetName);
                GameObject dragTarget = null;
                //Draggable is inactive, have to use the complicated search.
                var finder = Resources.FindObjectsOfTypeAll<GameObject>(); 
                foreach (var findy in finder)
                {
                    if (findy.name == dragTargetName)
                    {
                        dragTarget = findy;
                        break;
                    }
                }
                if (targ && dragTarget)
                {
                    dragTarget.GetComponent<draggableItemScript>().FindMe(dragTargetName);
                    dragTarget.SetActive(true);
                    targ.SetActive(false);

                }
            }
        }
    }
}
