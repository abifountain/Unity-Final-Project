using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage; 
    public PlayerHealth playerHealth; 
    public PlayerMovement playerMovement; 

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag ==  "Player") 
        {
            playerMovement.KBCounter = playerMovement.KBTotalTime; 

            if (collision.transform.position.x <= transform.position.x) 
            {
                playerMovement.KnockFromRight = true; 
            }
            if (collision.transform.position.x >= transform.position.x) 
            {
                playerMovement.KnockFromRight = false; 
            }

            playerHealth.TakeDamage(damage); 
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag ==  "Player") 
        {
            playerMovement.KBCounter = playerMovement.KBTotalTime; 

            if (collision.transform.position.x <= transform.position.x) 
            {
                playerMovement.KnockFromRight = true; 
            }
            if (collision.transform.position.x >= transform.position.x) 
            {
                playerMovement.KnockFromRight = false; 
            }

            playerHealth.TakeDamage(damage); 
        }
    }
}
