using UnityEngine;

public class CameraBehavior : BaseBehavior {
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void LateUpdate() {
        if (target != null) {
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z) + offset;
            var moveMag = (desiredPosition - transform.position).magnitude; // adds an easing function depending on the length of these two vectors
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * Mathf.Max(moveMag, 1));
            transform.position = smoothedPosition;
        }
    }
}
