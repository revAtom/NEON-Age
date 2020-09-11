using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    #region Variables
    [BoxGroup("Camera Follow")]
    [GUIColor(.7f, .3f, .35f)]
    [SceneObjectsOnly]
    public Transform target;

    [BoxGroup("Camera Follow")]
    [GUIColor(.7f, .3f, .35f)]
    public Vector3 offset;
    #endregion

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;

        transform.position = desiredPosition;

       transform.LookAt(target);
    }
}
