using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseUI : MonoBehaviour
{
    public GameObject pauseCanvas;
    public TMP_Text timeText;
    public string levelSceneName = "TestSzene Marija";
    public string menuSceneName = "StartMenu";

    bool isPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseCanvas.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
        if (isPaused) UpdateTime();
    }

    public void Resume()
    {
        isPaused = false;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelSceneName);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }

    void UpdateTime()
    {
        if (GameManager.Instance == null) return;
        float t = GameManager.Instance.GetTimeLeft();
        int m = Mathf.FloorToInt(t / 60f);
        int s = Mathf.FloorToInt(t % 60f);
        timeText.text = $"Time left: {m:00}:{s:00}";
    }
}