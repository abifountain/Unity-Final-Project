using UnityEngine;
using System; 
using System.Collections; 
using System.Collections.Generic; 

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5; 
    public int currentHealth; 
    public HealthUI healthUI; 
    public static event Action OnPlayerDied; 
    public GameObject gameOverScreen; 

    private SpriteRenderer spriteRenderer; 

    void Start()
    {
        currentHealth = maxHealth; 
        healthUI.SetMaxHearts(maxHealth); 
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; 
        healthUI.UpdateHearts(currentHealth); 
        if (currentHealth <= 0) 
        {
            OnPlayerDied?.Invoke(); 
        }
    }

    public void RegainHealth() 
    {
        currentHealth = maxHealth; 
        healthUI.SetMaxHearts(maxHealth); 
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red; 
        yield return new WaitForSeconds(0.2f); 
        spriteRenderer.color = Color.white; 
    }
}
