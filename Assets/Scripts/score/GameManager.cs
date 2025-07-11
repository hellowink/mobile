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
            DontDestroyOnLoad(gameObject);
            Debug.Log("CoinsManager inicializado");
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
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
        pointsText = GameObject.Find("PointsText")?.GetComponent<TextMeshProUGUI>();
        livesText = GameObject.Find("LivesText")?.GetComponent<TextMeshProUGUI>();
        gameOverPanel = GameObject.Find("GameOverPanel");

        if (scene.name == "Menu")
        {
            if (coinsManager.Instance != null)
            {
                int totalPuntos = coinsManager.Instance.ObtenerPuntosTotales();
                Debug.Log($"[Menu] Total de puntos guardados: {totalPuntos}");
            }
            else
            {
                Debug.LogWarning("[Menu] coinsManager.Instance es null");
            }
        }

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

        // Reset de juego y sincronización
        currentLives = maxLives;

        // Leer puntos guardados desde coinsManager
        points = coinsManager.Instance != null ? coinsManager.Instance.ObtenerPuntosTotales() : 0;

        UpdateLivesUI();
        UpdatePointsUI();

        Time.timeScale = 1f;
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

        if (coinsManager.Instance != null)
        {
            coinsManager.Instance.AgregarPuntosPartida(amount);
            Debug.Log("[GameManager] Suma a coinsManager: " + coinsManager.Instance.ObtenerPuntosTotales());
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
        if (coinsManager.Instance != null)
        {
            coinsManager.Instance.AgregarPuntosPartida(points);
            Debug.Log($"[GameOver] Puntos sumados esta partida: {points}");
            Debug.Log($"[GameOver] Total acumulado en coinsManager: {coinsManager.Instance.ObtenerPuntosTotales()}");
        }
        else
        {
            Debug.LogWarning("[GameOver] coinsManager.Instance es null");
        }

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

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

