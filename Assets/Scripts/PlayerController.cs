using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Vector2 movement;
    Rigidbody rigidBody;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        //Debug.Log(movement);

    }
    void HandleMovement()
    {
        Vector3 currentPosition = rigidBody.position;
        Vector3 moveDireciton = new Vector3(movement.x, 0f, movement.y);
        Vector3 newPosition = currentPosition + moveDireciton * (moveSpeed * Time.fixedDeltaTime);
        rigidBody.MovePosition(newPosition);
    }
}
