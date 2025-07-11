using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Points")]
    public int points = 0;
    public TextMeshProUGUI pointsText;

    [Header("Lives")]
    public int maxLives = 5;
    private int currentLives;

    [Header("UI")]
    public TextMeshProUGUI livesText;
    public GameObject gameOverPanel;

    [Header("Bonus Points")]
    public bool isBonusActive = false;
    public float bonusDuration = 3f;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI();
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
        UpdatePointsUI();
    }


    public void AddPoints(int amount)
    {
        if (isBonusActive)
        {
            amount *= 2;
            Debug.Log("BONUS ACTIVO: Duplicando puntos!");
        }
        else
        {
            Debug.Log("BONUS NO activo.");
        }


        points += amount;
        UpdatePointsUI();

        // Guardar puntos en laa sesión actual si hay usuario activo
        if (SessionManager.Instance != null && !string.IsNullOrEmpty(SessionManager.Instance.currentUser))
        {
            SessionManager.Instance.SavePoints(points);
        }
    }

    public void ActivateBonus()
    {
        StartCoroutine(BonusCoroutine());
    }

    private IEnumerator BonusCoroutine()
    {
        isBonusActive = true;
        Debug.Log("BONUS ACTIVADO!");
        yield return new WaitForSeconds(bonusDuration);
        isBonusActive = false;
        Debug.Log("BONUS FINALIZADO");
    }

    public void LoseLife()
    {
        if (currentLives <= 0) return;

        currentLives--;
        currentLives = Mathf.Max(0, currentLives);
        UpdateLivesUI();

        if (currentLives == 0)
        {
            GameOver();
        }
    }

    public void LoseAllLives()
    {
        currentLives = 0;
        UpdateLivesUI();
        GameOver();
    }

    public void UpdatePointsUI()
    {
        pointsText.text = "Points: " + points;
    }

    void UpdateLivesUI()
    {
        livesText.text = "Lives: " + Mathf.Max(currentLives, 0);
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
