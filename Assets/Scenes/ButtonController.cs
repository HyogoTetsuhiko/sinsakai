using UnityEngine;
using UnityEngine.SceneManagement; 
public class ButtonController : MonoBehaviour
{
    public void LoadScene(string Mainscene)
    {
        SceneManager.LoadScene(Mainscene);
    }
}