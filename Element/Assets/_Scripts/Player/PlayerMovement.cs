using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float movementSpeed = 1.0f;

  private Vector3 pivot;
  private Vector3 cur;
  public LineRenderer line;
  public Camera curCamera;

  public float maxDistance = 2f;

  public Canvas parentCanvas;

  // Use this for initialization
  void Start()
  {
    this.line.gameObject.SetActive(false);
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      this.pivot = Input.mousePosition;

      Vector3 curPivot = this.getScreenPoint(this.pivot);
      line.SetPosition(0, curPivot);
      line.SetPosition(1, curPivot);
      this.line.gameObject.SetActive(true);
    }
    else if (Input.GetMouseButton(0))
    {
      this.cur = Input.mousePosition;
      Vector3 pivotPos = this.getScreenPoint(this.pivot);
      Vector3 curPos = this.getScreenPoint(this.cur);
      Vector3 delta = (curPos - pivotPos);
      if (Vector3.Distance(pivotPos, curPos) >= maxDistance)
      {
        Vector3 dir = delta.normalized;
        curPos = pivotPos + dir * this.maxDistance;
      }

      line.SetPosition(0, pivotPos);
      line.SetPosition(1, curPos);
      line.transform.position = curPos;

      this.transform.position += delta * movementSpeed * Time.deltaTime;
    }
    else if (Input.GetMouseButtonUp(0))
    {
      this.line.gameObject.SetActive(false);
    }
  }

  Vector3 getScreenPoint(Vector3 movePo)
  {
    Vector3 screenPos = this.curCamera.ScreenToWorldPoint(movePo);
    screenPos = new Vector3(screenPos.x, screenPos.y, 0);
    return screenPos;
  }
}
