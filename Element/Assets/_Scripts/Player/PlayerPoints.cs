using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    public void UpdatePoint(int deltaPoint)
    {
        GameStateManager.Instance.IncreasePoints(deltaPoint);
    }
}
