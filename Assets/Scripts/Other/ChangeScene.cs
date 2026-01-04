using UnityEngine;
using UnityEngine.SceneManagement; 

public class ChangeScene : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

   public void ChangeSceneto(int idx)
    {
        SceneManager.LoadScene(idx);
    }

    public void ChangeToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
