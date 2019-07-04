using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// General enemy movement: Moves randomly around the player
public class EnemyMovement : MonoBehaviour {

  [SerializeField] protected Transform player;
  [SerializeField] protected float playerMovementRange = 7;
  [SerializeField] protected float randomMovementRange = 10;
  [SerializeField] protected float splineSpeed = 0.2f;
  [SerializeField] protected float correctedDistance = 3;

  // Spline is used to generate a smooth path 
  protected BezierCurve currentPath;

  void Start() {
    this.currentPath = new BezierCurve();
    this.currentPath.Initialize(this.transform);
    this.getNextMoves(this.transform.position);
  } 

  
  // Get the next path, based on randomization
  private void getNextMoves(Vector3 initialPos) {
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

  // Correct movement to not allow it to be too close to the player
  private Vector3 CorrectMovement(Vector3 og) {

    if (Vector3.Distance(og, this.player.position) <= 3) {
      int rngX = Random.Range(0, 2);
      int rngY = Random.Range(0, 2);
      og.x += rngX == 0 ? -correctedDistance * 2 : correctedDistance * 2;
      og.y += rngY == 0 ? -correctedDistance * 2 : correctedDistance * 2;
    }

    return og;
  }
	
  // Move the player along the path
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
