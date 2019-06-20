using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    private Vector3 prevMousePos = Vector3.zero;
    public float maxSpeed = 1.0f;

    // Use this for initialization
    void Start()
    {
        prevMousePos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButton(0)) {
			Vector3 deltaMouse = Input.mousePosition - prevMousePos;
      deltaMouse = new Vector3(Mathf.Clamp(deltaMouse.x, -this.maxSpeed, this.maxSpeed), Mathf.Clamp(deltaMouse.y, -this.maxSpeed, this.maxSpeed), deltaMouse.z);
			this.transform.position += deltaMouse * movementSpeed * Time.deltaTime;
		}

		this.prevMousePos = Input.mousePosition;
    }
}
