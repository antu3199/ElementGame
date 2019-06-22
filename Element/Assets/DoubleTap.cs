using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTap : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            print("Use power!");
        }
    }
}