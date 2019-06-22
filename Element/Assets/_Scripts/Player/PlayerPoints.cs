using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    public float points;
    public float maxPoints = 10;

    public void UpdatePoint(float deltaPoint)
    {
        this.points = points + deltaPoint;
        Debug.Log("Point is now: " + this.points);
    }
}
