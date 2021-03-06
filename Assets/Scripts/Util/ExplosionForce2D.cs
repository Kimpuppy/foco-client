using UnityEngine;
using System.Collections;

public class ExplosionForce2D : MonoBehaviour {
    public static void AddExplosionForce(Rigidbody2D body, float expForce, Vector3 expPosition, float expRadius) {
        var dir = (body.transform.position - expPosition);
        float calc = 1 - (dir.magnitude / expRadius);
        if (calc <= 0) {
            calc = 0;
        }

        body.AddForce(dir.normalized * expForce * calc, ForceMode2D.Impulse);
    }
}