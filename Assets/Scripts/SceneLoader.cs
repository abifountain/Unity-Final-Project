using UnityEngine;
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine.SceneManagement; 

public class SceneLoader : MonoBehaviour
{
    public string sceneName; 
    [SerializeField] Animator transitionAnim; 
    public void LoadScene(string name)
    {
        sceneName = name; 
        StartCoroutine(LoadLevel());
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag ==  "Player") 
        {
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End"); 
        yield return new WaitForSeconds(1); 
        if (MusicManager.CheckBackgroundMusic()) 
        {
            MusicManager.StopBackgroundMusic(); 
        }
        MusicManager.StopBackgroundMusic(); 
        SceneManager.LoadScene(sceneName);
        transitionAnim.SetTrigger("Start"); 
    }
}
