using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 0.8f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [SerializeField] float jumpForce = 8.0f;
    [SerializeField] float gravity = -9.81f;

    [SerializeField] public AudioSource jumpeffect;


    float verticalVelocity = 0.0f;
    bool isGrounded = true;

    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;
    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMousoeDeltaVelocity = Vector2.zero;
    private Animator animator;

    private Vector3 lastPosition;

    private bool isMovingForward = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        lastPosition = transform.position;

        // if (lockCursor){
        //     Cursor.lockState = CursorLockMode.Locked;
        //     Cursor.visible = false;

        // }   
    }

    void Update()
    {
        // Check if the player has moved to a new position
        if (Time.timeScale == 0f) {
            return;
        }

        UpdateMouseLook();
        UpdateMovement();

        if (transform.position != lastPosition || Input.GetKey(KeyCode.W))
        {
            isMovingForward = true;

        }
        else
        {
            isMovingForward = false;
            animator.SetBool("isRunning", isMovingForward);
        }

        lastPosition = transform.position;


    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMousoeDeltaVelocity, mouseSmoothTime);

        // Reverse the vertical mouse rotation direction
        float mouseYRotation = -currentMouseDelta.y * mouseSensitivity;  // Invert the sign for vertical rotation
        cameraPitch = Mathf.Clamp(cameraPitch + mouseYRotation, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        // Check if the player is moving forward.
        isMovingForward = currentDir.y > 0.0f;

        if (isMovingForward)
        {
        }

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed;

        // Jump logic
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Trigger the "isJumping" animation when the player jumps.
            jumpeffect.Play();
            animator.SetTrigger("isJumping");

            verticalVelocity = jumpForce;
            isGrounded = false;
        }

        verticalVelocity += gravity * Time.deltaTime;

        velocity.y = verticalVelocity;

        CollisionFlags flags = controller.Move(velocity * Time.deltaTime);

        isGrounded = (flags & CollisionFlags.Below) != 0;

        // Trigger the "isRunning" animation when the player moves forward.

        animator.SetBool("isRunning", isMovingForward);
    }
}