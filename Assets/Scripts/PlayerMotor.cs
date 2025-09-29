using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    [Header("Movement Settings")]
    public float speed = 2.5f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        print("is grounded or not : " + controller.isGrounded);

        playerVelocity.y = -2f; // ensures no "hanging in air" bug

    }

    void Update()
    {

        //print("is grounded or not from update : " + controller.isGrounded);
        //print("value of player velocity :" + playerVelocity.y);
        isGrounded = controller.isGrounded;

        // Reset downward velocity when grounded
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        else if (!isGrounded && playerVelocity.y > -2f)
        {
            // ensure character doesn’t float mid-air
            playerVelocity.y += gravity * Time.deltaTime;
        }
    }

    // Called every frame by InputManager to move the player
    public void processMove(Vector2 input)
    {
        // Movement on XZ plane
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f); // avoid faster diagonal

        // Move relative to player’s orientation
        Vector3 move = transform.TransformDirection(moveDirection) * speed;

        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;



        // Combine horizontal + vertical movement
        Vector3 finalMove = move + (playerVelocity.y * Vector3.up);

        controller.Move(finalMove * Time.deltaTime);
    }

    // Jump request (called by InputManager)
    public void jump()
    {
        if (isGrounded)
        {
            // Correct Unity jump formula
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
    }
}



