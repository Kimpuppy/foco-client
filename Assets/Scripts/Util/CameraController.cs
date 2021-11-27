using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform Target = null;
    public float Speed = 5.0f;
    public float Scale = 5.0f;
    public Vector3 Offset = Vector3.zero;

    private Camera cameraObject;
    public Camera CameraObject => cameraObject;

    private Vector3 anchorPoint = Vector3.zero;
    private float shakePower = 0.0f;
    private float shakeAmount = 7.5f;

    private bool isXShake = true;
    private bool isYShake = true;

    private float angleAmount = 0.0f;

    private float scaleAmount = 1.0f;

    private void Start() {
        cameraObject = GetComponent<Camera>();
    }

    private void LateUpdate() {
        if (Target != null) {
            anchorPoint = Vector3.Lerp(anchorPoint, Target.position, Speed * Time.deltaTime);
        }

        shakePower -= shakePower / shakeAmount;

        Vector3 shakeVec = Vector3.zero;
        shakeVec.x = (isXShake) ? Random.Range(-shakePower, shakePower) : 0.0f;
        shakeVec.y = (isYShake) ? Random.Range(-shakePower, shakePower) : 0.0f;
        transform.position = anchorPoint + Offset + shakeVec;
        
        transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0.0f, 0.0f, angleAmount), 5.0f * Time.deltaTime);
        cameraObject.orthographicSize = Mathf.Lerp(cameraObject.orthographicSize, Scale * scaleAmount, 5.0f * Time.deltaTime);
    }

    public void Shake(float power, float amount, bool xshake = true, bool yshake = true) {
        shakePower = power;
        shakeAmount = amount;
        isXShake = xshake;
        isYShake = yshake;
    }

    public void SetAngle(float angle) {
        angleAmount = angle;
    }

    public void Rotating(float angle) {
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, angle);
    }

    public void SetScale(float scale) {
        scaleAmount = scale;
    }

    public void Scaling(float scale) {
        cameraObject.orthographicSize = Scale * scale;
    }
}