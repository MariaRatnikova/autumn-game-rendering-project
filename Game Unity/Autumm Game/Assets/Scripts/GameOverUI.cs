using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public string levelSceneName = "TestSzene Marija";
    public string menuSceneName = "StartMenu";
    public string cutsceneSceneName = "CutsceneComic";   

    public void StartGame()
    {
        SceneManager.LoadScene(cutsceneSceneName);        
    }

    public void Retry()
    {
        SceneManager.LoadScene(levelSceneName);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
