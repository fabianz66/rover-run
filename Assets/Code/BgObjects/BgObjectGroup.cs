using System;
using System.Collections.Generic;
using UnityEngine;

public class BgObjectGroup
{
    public readonly String name; //The name of this area. Shown on top of the screen.
    public readonly int startPositionX;
    public readonly int length;
    public readonly int endPositionX;    
    private int nextObjectPositionX; //At which position we should show the next object
    private int objectsDistance; //At which position we should show the next object
    private readonly Queue<BgObject> bgObjects = new Queue<BgObject>(); //Objects 
    
    //Constructor
    public BgObjectGroup(String name, int startPositionX, int length)
    {
        this.name = name;
        this.startPositionX = startPositionX;
        this.length = length;
        this.nextObjectPositionX = startPositionX;
        this.endPositionX = startPositionX + length;
    }

    // Adds an object to the area. The are shown FIFO starting at startPosition and then
    // equally distant depending on the number of objects
    public void AddBgObject(BgObject.TYPE type, String text, Sprite sprite)
    {
        //Add Object
        BgObject o = new BgObject(type, text, sprite);
        bgObjects.Enqueue(o);

        //Update the distance between objects
        objectsDistance = this.length / this.bgObjects.Count;
    }

    //Gets the next object that must be palced on screen based on position
    //or null if none must be shown at this position.
    public BgObject NextObject(float position)
    {
        if (position >= nextObjectPositionX && bgObjects.Count > 0) {
            nextObjectPositionX += objectsDistance;
            return bgObjects.Dequeue();
        }
        return null;
    }

    //If a position in X is inside this area
    public bool IsInside(float positionX) {
        return (startPositionX < positionX) && (positionX <= endPositionX);
    }
}
