using UnityEngine;
using UnityEngine.InputSystem; 

public class VerticalPlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb; 
    public float moveSpeed = 5f; 
    float verticalMovement; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
    }
    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, verticalMovement * moveSpeed); 
    }

    public void Move(InputAction.CallbackContext context) 
    {
        verticalMovement = context.ReadValue<Vector2>().y; 
    }
}
