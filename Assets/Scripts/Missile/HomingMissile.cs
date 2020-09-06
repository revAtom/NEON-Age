using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class HomingMissile : MonoBehaviour
{
    [BoxGroup("Transform")]
    [GUIColor(.43f, .44f, .66f)]
    public Transform missileTarget, missileLocalTransform;

    private Rigidbody missileRb;

    [BoxGroup("Speed")]
    [GUIColor(.40f, .1f, .65f)]
    public float missileSpeed, missileTurnSpeed;

    private void Awake()
    {
        missileRb = GetComponent<Rigidbody>();

        missileTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        missileLocalTransform = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        if (!missileTarget)
            return;

        missileRb.velocity = missileLocalTransform.forward * missileSpeed;

        var torpedoTargetRot = Quaternion.LookRotation(missileTarget.position - missileLocalTransform.position);

        missileRb.MoveRotation(Quaternion.RotateTowards(missileLocalTransform.rotation, torpedoTargetRot, missileTurnSpeed));

        if (missileRb.velocity.magnitude > missileSpeed)
            missileRb.velocity = Vector3.ClampMagnitude(missileRb.velocity, missileSpeed);
    }
}
