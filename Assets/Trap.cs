using UnityEngine;

public class Trap : MonoBehaviour
{
    public float bounceForce = 10f; 

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HandlePlayerBounce(collision.gameObject); 
        }
    }

    private void HandlePlayerBounce(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>(); 

        if(rb)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); 
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse); 
        }
    }
}
