using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : PlayerInput
{
    #region Variables
    private Rigidbody playerRb;

    [TabGroup("Movement")]
    [GUIColor(.3f, 1, .6f)]
    [Range(0, 3000)]
    public float speedForce = 1, steerPower = 1500, Speed;

    [TabGroup("Visualise")]
    [GUIColor(.8f, .3f, .3f)]
    [SceneObjectsOnly]
    public Animator playerVisualiseAnim;

    [TabGroup("Gravity")]
    [GUIColor(.788f, .31f, .788f)]
    public LayerMask waterLayer;

    [TabGroup("Gravity")]
    [GUIColor(.788f, .31f, .788f)]
    [Range(0, 2)]
    public float gravityScale, rayDistance, Drag;

    [TabGroup("Gravity")]
    [GUIColor(.788f, .31f, .788f)]
    [ShowInInspector]
    private bool isGrounded;

    [TabGroup("Gravity")]
    [GUIColor(.788f, .31f, .788f)]
    [ChildGameObjectsOnly]
    public Transform boatBottom;

    private float speedBooster, speedUnBooster;
    #endregion
    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();

        Speed = speedForce;

        speedBooster = Speed * 4;
        speedUnBooster = Speed;
    }
    #region Animation
    private void Update()
    {
        if (Steer() != 0)
        {
            playerVisualiseAnim.SetBool("IsTurn", true);

            playerVisualiseAnim.SetFloat("angleOfTurn", Steer());

            speedForce = Speed / 1.2f;
        }
        else
        {
            playerRb.angularVelocity = Vector3.zero;

            speedForce = Speed;

            playerVisualiseAnim.SetBool("IsTurn", false);
        }
    }
    #endregion
    private void FixedUpdate()
    {
        #region Gravity
        isGrounded = false;

        RaycastHit hit;

        if (Physics.Raycast(boatBottom.position, -transform.up, out hit, rayDistance, waterLayer))
        {
            isGrounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
        #endregion

        #region Movement
        this.gameObject.transform.rotation = playerRb.rotation;

        if (isGrounded)
        {
            playerRb.drag = Drag;

            playerRb.AddTorque(transform.up * Steer() * steerPower * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
        else
        {
            playerRb.drag = Drag / 2f;

            playerRb.AddForce(Physics.gravity * Time.fixedDeltaTime * gravityScale * 10000f);
        }

        playerRb.AddRelativeForce(Vector3.forward * Speed * Time.fixedDeltaTime * 1000f);

        if (playerRb.velocity.magnitude > Speed)
            playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, Speed);

        #endregion
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rump"))
        {
            playerRb.drag = .5f;

            Speed = speedBooster;
        }

        else if (collision.gameObject.CompareTag("Missile"))
            Debug.Log("Dead");
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rump"))
        {
            playerRb.drag = 1f;

            Speed = speedUnBooster;
        }
    }
}
