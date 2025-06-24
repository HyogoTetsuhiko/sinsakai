using UnityEngine;
using UnityEngine.SceneManagement; 
public class RetryButtonController : MonoBehaviour
{
    public void LoadScene()
    {
        GameManager.instance.ResetGameState();
        SceneManager.LoadScene("Title");
    }
}