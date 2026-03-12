using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ComicCutscene : MonoBehaviour
{
    [Header("UI")]
    public Image panelImage;

    [Header("Comic Frames (Sprites)")]
    public Sprite[] frames;

    [Header("Flow")]
    public string nextSceneName = "MainLevel";
    public bool advanceOnClickOrKey = true;
    public KeyCode advanceKey = KeyCode.Space;

    [Header("Optional Auto-Play")]
    public bool autoPlay = false;
    public float secondsPerFrame = 2.0f;

    int index = 0;
    float timer = 0f;

    void Start()
    {
        if (panelImage == null || frames == null || frames.Length == 0)
        {
            Debug.LogError("ComicCutscene: assign panelImage and frames in Inspector.");
            enabled = false;
            return;
        }

        ShowFrame(0);
    }

    void Update()
    {
        if (autoPlay)
        {
            timer += Time.deltaTime;
            if (timer >= secondsPerFrame)
            {
                timer = 0f;
                Next();
            }
            return;
        }

        if (!advanceOnClickOrKey) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(advanceKey) || Input.GetKeyDown(KeyCode.Return))
        {
            Next();
        }
    }

    void ShowFrame(int i)
    {
        index = Mathf.Clamp(i, 0, frames.Length - 1);
        panelImage.sprite = frames[index];
    }

    void Next()
    {
        if (index < frames.Length - 1)
        {
            ShowFrame(index + 1);
        }
        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}

