using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform bodyTransform;
    public Transform cameraTransform;
    public Rigidbody playerRigidBody;
    public float speed;
    public float defaultSpeed;
    public float yawRotationSpeed;
    public float pitchRotationSpeed;
    public CapsuleCollider bodyCollider;
    public float defaultHeight;
    public bool isSprint;
    public float force;
    public float defaultForce;

    private Vector3 directionIntent;
    private bool wantToJump;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 targetDirection = new Vector3(1, 0, 0);
        cameraTransform.rotation = Quaternion.LookRotation(targetDirection);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            directionIntent += Vector3.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            directionIntent += Vector3.back;
        }

        if (Input.GetKey(KeyCode.A))
        {
            directionIntent += Vector3.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            directionIntent += Vector3.right;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprint = true;
            speed = defaultSpeed * 2;
        }
        else
        {
            isSprint = false;
            speed = defaultSpeed;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            bodyCollider.height = defaultHeight / 2;
        }
        else
        {
            bodyCollider.height = defaultHeight;
        }
        
        var mouseXDelta = Input.GetAxis("Mouse X");

        bodyTransform.Rotate(Vector3.up, Time.deltaTime * yawRotationSpeed * mouseXDelta);

        var mouseYDelta = Input.GetAxis("Mouse Y");

        var rotation = cameraTransform.localRotation;

        var rotationX = rotation.eulerAngles.x;

        rotationX += -Time.deltaTime * pitchRotationSpeed * mouseYDelta;

        var unClampedRotationX = rotationX;

        if (unClampedRotationX >= 180)
        {
            unClampedRotationX -= 360;
        }

        var clampedRotationX = Mathf.Clamp(unClampedRotationX, -60, 60);

        cameraTransform.localRotation =
            Quaternion.Euler(new Vector3(
                clampedRotationX,
                rotation.eulerAngles.y,
                rotation.eulerAngles.z
            ));

        if (Input.GetKeyDown(KeyCode.Space) &&
            Physics.SphereCast(bodyTransform.position + Vector3.up * (0.1f + 0.45f),
                0.45f,
                Vector3.down, 
                out var _hitInfo,
                0.11f)
            )
        {
            wantToJump = true;
        }
    }

    private void FixedUpdate()
    {
        var normalizedDirection = directionIntent.normalized;
        bodyTransform.position += bodyTransform.rotation * normalizedDirection * (Time.deltaTime * speed);
        directionIntent = Vector3.zero;
        
        if (wantToJump)
        {
            if (isSprint)
            {
                force = defaultForce * 2;
            }
            else
            {
                force = defaultForce;
            }
            playerRigidBody.AddForce(
                Vector3.up * force, ForceMode.VelocityChange
                );
            wantToJump = false;
        }
    }
}