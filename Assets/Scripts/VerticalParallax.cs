using UnityEngine;

public class VerticalParallax : MonoBehaviour
{
    private float startPos, length; 
    public GameObject cam; 
    public float parallaxEffect; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position.y; 
        length = GetComponent<SpriteRenderer>().bounds.size.y; 
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = cam.transform.position.y * parallaxEffect; 
        float movement = cam.transform.position.y * (1 - parallaxEffect); 
        transform.position = new Vector3(transform.position.x, startPos + distance, transform.position.z);
        
        if (movement > startPos + length)
        {
            startPos += length;
        }
        else if (movement < startPos - length) 
        {
            startPos -= length; 
        }
    }
}
