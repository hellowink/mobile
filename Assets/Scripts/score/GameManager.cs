using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Lives")]
    public int maxLives = 5;
    private int currentLives;

    [Header("UI")]
    public TextMeshProUGUI livesText;
    public GameObject gameOverPanel;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI();
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f; // Asegura que el tiempo corre al inicio
        GameState.ResetPoints();
    }

    public void LoseLife()
    {
        if (currentLives <= 0) return;

        currentLives--;
        currentLives = Mathf.Max(0, currentLives); // Protecci�n extra
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

    void UpdateLivesUI()
    {
        livesText.text = "Lives: " + Mathf.Max(currentLives, 0);
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Pausa el juego
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        GameState.ResetPoints(); // Reinicia puntos visibles
                                 // GameState.ResetCoins(); // Solo si quer�s borrar monedas de esta sesi�n

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f; // Reanuda el juego
        SceneManager.LoadScene("Menu");
    }
}