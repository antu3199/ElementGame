using UnityEngine;

// Reference: https://catlikecoding.com/unity/tutorials/curves-and-splines/
// For interpolating smooth paths based off of 3 or 4 points.
public class BezierCurve {
  public Transform relativeTransform;
	public Vector3[] points = new Vector3[4];

  public void Initialize(Transform relativeTransform) {
    this.relativeTransform = relativeTransform;
  }
	
	public Vector3 GetPoint (Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
		t = Mathf.Clamp01(t);
		float OneMinusT = 1f - t;
		return
			OneMinusT * OneMinusT * OneMinusT * p0 +
			3f * OneMinusT * OneMinusT * t * p1 +
			3f * OneMinusT * t * t * p2 +
			t * t * t * p3;
	}

	public Vector3 GetFirstDerivative (Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
		t = Mathf.Clamp01(t);
		float oneMinusT = 1f - t;
		return
			3f * oneMinusT * oneMinusT * (p1 - p0) +
			6f * oneMinusT * t * (p2 - p1) +
			3f * t * t * (p3 - p2);
	}

	public Vector3 GetPoint (float t) {
		return GetPoint(points[0], points[1], points[2], points[3], t);
	}
	
	public Vector3 GetVelocity (float t) {
		return GetFirstDerivative(points[0], points[1], points[2], points[3], t);
	}
	
	public Vector3 GetDirection (float t) {
		return GetVelocity(t).normalized;
	}
}