using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentPoints;

    public float playerSlideSpeed = 1.3f;

    [Header("Points")]
    public int points = 0;
    public TextMeshProUGUI pointsText;

    [Header("Lives")]
    public int maxLives = 5;
    private int currentLives;

    [Header("UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI livesText;

    private Button restartButton;
    private Button menuButton;

    [Header("Bonus Points")]
    public bool isBonusActive = false;
    public float bonusDuration = 3f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persistir entre escenas
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Referencias de UI
        pointsText = GameObject.Find("PointsText")?.GetComponent<TextMeshProUGUI>();
        livesText = GameObject.Find("LivesText")?.GetComponent<TextMeshProUGUI>();
        gameOverPanel = GameObject.Find("GameOverPanel");

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Referencias de botones
        restartButton = GameObject.Find("RestartButton")?.GetComponent<Button>();
        menuButton = GameObject.Find("MenuButton")?.GetComponent<Button>();

        // Asignar acciones a botones
        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartGame);
        }

        if (menuButton != null)
        {
            menuButton.onClick.RemoveAllListeners();
            menuButton.onClick.AddListener(ReturnToMenu);
        }

        // Reset de juego
        currentLives = maxLives;
        points = 0;
        UpdateLivesUI();
        UpdatePointsUI();

        Time.timeScale = 1f;
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
        if (pointsText != null)
            pointsText.text = "Points: " + points;
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = "Lives: " + Mathf.Max(currentLives, 0);
    }

    void GameOver()
    {
        coinsManager.Instance.AgregarPuntosPartida(currentPoints);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Esperar un frame para que los botones estén listos
        StartCoroutine(PauseAfterUIUpdate());
    }

    private IEnumerator PauseAfterUIUpdate()
    {
        yield return null;
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

