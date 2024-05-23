using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public Animator AnimatorController;

    public float walkingSpeed, walkingBackSpeed, runningSpeed, rotationSpeed, lowerSpeed;
    public float baseJumpForce, maxJumpForce;

    private int jumpCount = 0;
    private bool isGrounded = true;

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    private void Update()
    {
        HandleRunning();
        HandleJumping();
    }

    private void HandleMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 verticalSpeed = Vector3.zero;

        if (verticalInput > 0)
        {
            verticalSpeed = transform.forward * walkingSpeed;
            AnimatorController.SetBool("isWalking", true);
            AnimatorController.SetBool("isWalkingBack", false);
        }
        else if (verticalInput < 0)
        {
            verticalSpeed = -transform.forward * walkingBackSpeed;
            AnimatorController.SetBool("isWalkingBack", true);
            AnimatorController.SetBool("isWalking", false);
        }
        else
        {
            AnimatorController.SetBool("isWalking", false);
            AnimatorController.SetBool("isWalkingBack", false);
        }

        Rigidbody.velocity = new Vector3(verticalSpeed.x, Rigidbody.velocity.y, verticalSpeed.z);
    }

    private void HandleRotation()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {
            transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void HandleRunning()
    {
        bool isSprinting = Input.GetButton("Sprint");
        float verticalInput = Input.GetAxis("Vertical");

        if (isSprinting && verticalInput > 0)
        {
            walkingSpeed = runningSpeed;
            AnimatorController.SetBool("isRunning", true);
            AnimatorController.SetBool("isWalking", false);
        }
        else if (!isSprinting || verticalInput == 0)
        {
            walkingSpeed = lowerSpeed;
            AnimatorController.SetBool("isRunning", false);
        }
    }

    private void HandleJumping()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Rigidbody.AddForce(Vector3.up * baseJumpForce, ForceMode.VelocityChange);
            AnimatorController.SetBool("isJumping", true);
            AnimatorController.SetBool("isFalling", false);
            jumpCount = 1;
        }
        else if (!isGrounded && jumpCount == 1)
        {
            Rigidbody.AddForce(Vector3.up * maxJumpForce, ForceMode.VelocityChange);
            jumpCount = 2;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
            AnimatorController.SetBool("isJumping", false);
            AnimatorController.SetBool("isFalling", false);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            if (Rigidbody.velocity.y < 0)
            {
                AnimatorController.SetBool("isFalling", true);
            }
        }
    }
}
