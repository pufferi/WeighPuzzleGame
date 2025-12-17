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
}
