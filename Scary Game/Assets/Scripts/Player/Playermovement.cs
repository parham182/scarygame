using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playermovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Transform camera;
    [SerializeField] float gravity = -20f;
    private CharacterController controller;

    Vector2 moveInput;
    private Vector3 velocity;

    public bool canMove = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Update()
    {


        playerMove();
    }

    private void playerMove()
    {
        if (!canMove) return;
        
        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;
        forward.y = 0f;
        right.y = 0f;

        Vector3 direction = (right * moveInput.x + forward * moveInput.y);

        Vector3 move = direction * moveSpeed * Time.deltaTime;
        controller.Move(move);

        velocity.y += gravity * Time.deltaTime;
        Vector3 finalMove = velocity * Time.deltaTime;
        controller.Move(finalMove);
    }
}
