using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TMP_Text nutCounterText;
    public TMP_Text timerText;
    public Image timeBarFill;

    [Header("Game Settings")]
    public int nutsToWin = 10;
    public float timeLimitSeconds = 120f;

    [Header("Fog (Herbstnebel)")]
    [SerializeField] private Material fogMaterial;     
    [SerializeField] private float fogDelay = 30f;     
    [SerializeField] private float fogFadeDuration = 10f; 
    [SerializeField] private float fogTargetOpacity = 0.1f;

    private static readonly int FogOpacityID = Shader.PropertyToID("_FogOpacity");

    private int nutsDeposited = 0;
    private float timeLeft;
    private bool gameOver = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        timeLeft = timeLimitSeconds;
        UpdateNutUI();
        UpdateTimerUI();
        UpdateTimeBarUI();

        if (fogMaterial != null)
        {
            fogMaterial.SetFloat(FogOpacityID, 0f);
            StartCoroutine(FadeInFogAfterDelay());
        }
        else
        {
            Debug.LogWarning("FogMaterial ist im GameManager nicht gesetzt (Fog bleibt aus).");
        }
    }

    private void Update()
    {
        if (gameOver) return;

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0) timeLeft = 0;

        UpdateTimerUI();
        UpdateTimeBarUI();

        if (timeLeft <= 0)
        {
            Lose();
        }
    }

    public void DepositNut()
    {
        if (gameOver) return;

        nutsDeposited++;
        UpdateNutUI();

        if (nutsDeposited >= nutsToWin)
        {
            Win();
        }
    }

    private void UpdateNutUI()
    {
        if (nutCounterText != null)
            nutCounterText.text = $"Nüsse: {nutsDeposited}/{nutsToWin}";
    }

    private void UpdateTimerUI()
    {
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);

        timerText.text = $"Zeit: {minutes:00}:{seconds:00}";
    }

    private void UpdateTimeBarUI()
    {
        if (timeBarFill == null) return;

        float t = Mathf.Clamp01(timeLeft / timeLimitSeconds);
        timeBarFill.fillAmount = t;
    }

    private void Win()
    {
        gameOver = true;
        Debug.Log("GEWONNEN!");
        SceneManager.LoadScene("Victory");   
    }

    private void Lose()
    {
        gameOver = true;
        Debug.Log("VERLOREN (Zeit abgelaufen)!");
        SceneManager.LoadScene("GameOver");  // 
    }
    public void LoseByDeath()
    {
        if (gameOver) return;
        gameOver = true;
        Debug.Log("VERLOREN (Spieler gestorben)!");
        SceneManager.LoadScene("GameOver");
    }
    private IEnumerator FadeInFogAfterDelay()
    {
        yield return new WaitForSeconds(fogDelay);

        float t = 0f;
        while (t < fogFadeDuration)
        {
            t += Time.deltaTime;
            float v = Mathf.Lerp(0f, fogTargetOpacity, t / fogFadeDuration);
            fogMaterial.SetFloat(FogOpacityID, v);
            yield return null;
        }

       
        fogMaterial.SetFloat(FogOpacityID, fogTargetOpacity);
    }

    public float GetTimeLeft()
    {
    return timeLeft;
    }   
}