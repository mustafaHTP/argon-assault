using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneBuildIndex + 1);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
