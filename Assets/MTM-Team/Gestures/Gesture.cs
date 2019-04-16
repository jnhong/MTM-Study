using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gesture
{
    LinkedList<GameObject> hitBoxes; // the actual hitboxes are game objects
    LinkedListNode<GameObject> currentNode; // hitbox to be hit in sequence
    string label;

    public Gesture()
    {
        hitBoxes = new LinkedList<GameObject>();
        currentNode = null;
        label = "unlabeled";
    }

    public void setLabel(string label)
    {
        this.label = label;
    }

    public void resetSequence()
    {
        currentNode = hitBoxes.First;
    }

    public void hit(GameObject hitBox)
    {
        // check if box it is the one in the sequence expect (currentNode)
        if (hitBox == currentNode.Value)
        {
            if (currentNode.Next == null) // sequence completed
            {
                currentNode = hitBoxes.First;
                onSequenceCompletion();
            } else
            {
                currentNode = currentNode.Next;
            }
        }
        // else ignore
    }

    public void onSequenceCompletion()
    {
        // do something
        Debug.Log("Gesture Completed");
    }

    public void addHitBox(GameObject hitBox)
    {
        hitBoxes.AddLast(hitBox);
        hitBox.GetComponent<HitBox>().setGesture(this);
    }

    public GameObject getLastHitBox()
    {
        return hitBoxes.Last.Value;
    }

    public void disableGesture()
    {
        foreach (GameObject hitBox in hitBoxes)
        {
            hitBox.SetActive(false);
        }
    }

    public void deleteGesture()
    {
        foreach (GameObject hitBox in hitBoxes)
        {
            Object.Destroy(hitBox);
        }
    }

}
