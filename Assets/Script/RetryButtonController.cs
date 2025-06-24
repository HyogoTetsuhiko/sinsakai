using UnityEngine;
using UnityEngine.SceneManagement; 
public class RetryButtonController : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Title");
    }
}