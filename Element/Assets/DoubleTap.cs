using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTap : MonoBehaviour
{

    int TapCount;
    public float MaxDubbleTapTime = 5;
    float NewTime;

    void Start()
    {
        TapCount = 0;
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                TapCount += 1;
            }

            if (TapCount == 1)
            {

                NewTime = Time.time + MaxDubbleTapTime;
            }
            else if (TapCount == 2 && Time.time <= NewTime)
            {
                print("Dubble tap");

                TapCount = 0;
            }

        }
        if (Time.time > NewTime)
        {
            TapCount = 0;
        }
    }
}