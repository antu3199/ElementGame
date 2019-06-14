using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

  [SerializeField] protected Transform player;
  [SerializeField] protected float playerMovementRange;
  [SerializeField] protected float randomMovementRange;
  [SerializeField] protected float splineSpeed;
  [SerializeField] protected float correctedDistance = 3;
  protected BezierCurve currentPath;

  void Start() {
    this.currentPath = new BezierCurve();
    this.currentPath.Initialize(this.transform);
    this.getNextMoves(this.transform.position);
  } 

  void getNextMoves(Vector3 initialPos) {
    float playerX = this.player.position.x;
    float playerY = this.player.position.y;
    float movementX = Random.Range(playerX - playerMovementRange, playerY + playerMovementRange);
    float movementY = Random.Range(playerY - playerMovementRange, playerY + playerMovementRange);
    
    Vector3 v0 = initialPos;
    Vector3 v1 = CorrectMovement(new Vector3(movementX, movementY, 0));
    Vector3 v2 = CorrectMovement(new Vector3(v1.x + Random.Range(-randomMovementRange, randomMovementRange), v1.y + Random.Range(-randomMovementRange, randomMovementRange), 0));
    Vector3 v3 = CorrectMovement(new Vector3(v2.x + Random.Range(-randomMovementRange, randomMovementRange), v2.y + Random.Range(-randomMovementRange, randomMovementRange), 0));

    this.currentPath.points[0] = v0;
    this.currentPath.points[1] = v1;
    this.currentPath.points[2] = v2;
    this.currentPath.points[3] = v3;
    this.StartCoroutine(this.slideAlongPath());
  }

  Vector3 CorrectMovement(Vector3 og) {

    if (Vector3.Distance(og, this.player.position) <= 3) {
      int rngX = Random.Range(0, 2);
      int rngY = Random.Range(0, 2);
      og.x += rngX == 0 ? -correctedDistance * 2 : correctedDistance * 2;
      og.y += rngY == 0 ? -correctedDistance * 2 : correctedDistance * 2;
    }

    return og;
  }
	
	private IEnumerator slideAlongPath() {

    float t = 0;
    this.transform.position = this.currentPath.GetPoint(t);
    yield return null;

    float biasedModifier = Mathf.Clamp(0.5f + 1/Vector3.Distance(this.transform.position, this.currentPath.points[1]), 0, 1);
    float biasedSpeed = splineSpeed * biasedModifier;

    while (t <= 1) {
      t += biasedSpeed * Time.deltaTime;
      this.transform.position = this.currentPath.GetPoint(t);
      yield return null;
    }

    yield return null;
    this.getNextMoves(this.transform.position);
  }
}
