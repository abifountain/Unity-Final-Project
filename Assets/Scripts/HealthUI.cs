using UnityEngine;
using UnityEngine.UI; 
using System.Collections.Generic;

public class HealthUI : MonoBehaviour
{
    public Image heartPrefab; 
    public Sprite fullHeartSprite; 
    public Sprite emptyHeartSprite; 

    private List<Image> hearts = new List<Image>(); 

    public void SetMaxHearts(int maxHearts) 
    {
        foreach(Image heart in hearts) 
        {
            Destroy(heart.gameObject); 
        }
        hearts.Clear(); 

        for (int i = 0; i < maxHearts; i++) 
        {
            Image newHeart = Instantiate(heartPrefab, transform); 
            newHeart.sprite = fullHeartSprite; 
            hearts.Add(newHeart);
        }
    }

    public void UpdateHearts(int currentHealth) 
    {
        for (int i = 0; i < hearts.Count; i++) 
        {
            if (i < currentHealth) 
            {
                hearts[i].sprite = fullHeartSprite; 
            }
            else 
            {
                hearts[i].sprite = emptyHeartSprite; 
            }
        }
    }
}
