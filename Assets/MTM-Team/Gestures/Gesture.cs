using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gesture
{
    LinkedList<GameObject> hitBoxes; // the actual hitboxes are game objects
    LinkedListNode<GameObject> currentNode; // hitbox to be hit in sequence
    List<Vector3> points;
    LineRenderer lineRenderer;

    public string label;

    public Gesture()
    {
        hitBoxes = new LinkedList<GameObject>();
        currentNode = null;
        label = "unlabeled";
        points = new List<Vector3>();
        lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.material.color = Color.yellow;
    }

    public string getLabel()
    {
        return label;
    }

    public void setLabel(string label)
    {
        this.label = label;
    }

    public void resetSequence()
    {
        if (currentNode != null)
        {
            currentNode.Value.GetComponent<HitBox>().unhighlight();
        }
        currentNode = hitBoxes.First;
        currentNode.Value.GetComponent<HitBox>().highlight();
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
                currentNode.Value.GetComponent<HitBox>().unhighlight();
                currentNode = currentNode.Next;
                currentNode.Value.GetComponent<HitBox>().highlight();
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
        points.Add(hitBox.transform.position);
        if (hitBoxes.Count == 1)
        {
            hitBox.GetComponent<HitBox>().highlight();
        } else // draw lines
        {
            updateLine();
        }
    }

    private void updateLine()
    {
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    public GameObject getLastHitBox()
    {
        return hitBoxes.Last.Value;
    }

    public void enableGesture()
    {
        foreach (GameObject hitBox in hitBoxes)
        {
            hitBox.SetActive(true);
        }
        resetSequence();
        updateLine();
    }

    public void disableGesture()
    {
        foreach (GameObject hitBox in hitBoxes)
        {
            hitBox.SetActive(false);
        }
        lineRenderer.positionCount = 0;
    }

    public void deleteGesture()
    {
        foreach (GameObject hitBox in hitBoxes)
        {
            Object.Destroy(hitBox);
        }
        hitBoxes.Clear();
        lineRenderer.positionCount = 0;
        GameObject.Destroy(lineRenderer.gameObject);
        lineRenderer = null;
    }

}
