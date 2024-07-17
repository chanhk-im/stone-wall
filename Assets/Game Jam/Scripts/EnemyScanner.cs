using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearstTarget;

    void FixedUpdate() {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearstTarget = GetNearst();
    }

    Transform GetNearst() {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit2D target in targets) {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float currDiff = Vector3.Distance(targetPos, myPos);

            if (currDiff < diff) {
                diff = currDiff; 
                result = target.transform;
            }
        }

        return result;
    }
}
