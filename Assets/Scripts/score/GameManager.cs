using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float playerSlideSpeed = 1.3f;

    [Header("Points")]
    public int points = 0;
    public TextMeshProUGUI pointsText;

    [Header("Lives")]
    public int maxLives = 5;
    private int currentLives;

    [Header("UI")]
    public GameObject gameOverPanel;

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

    void Start()
    {
        // En Start podés hacer cosas que solo querés al principio,
        // pero el reset lo hacemos en OnSceneLoaded para que sea seguro
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Actualizar referencias UI buscando por nombre en la escena cargada
        pointsText = GameObject.Find("PointsText")?.GetComponent<TextMeshProUGUI>();
        livesText = GameObject.Find("LivesText")?.GetComponent<TextMeshProUGUI>();
        gameOverPanel = GameObject.Find("GameOverPanel");

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Reiniciar valores de vidas y puntos al cargar escena
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

    public TextMeshProUGUI livesText; // Lo agregué aquí para que compile

    void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = "Lives: " + Mathf.Max(currentLives, 0);
    }

    void GameOver()
    {
        if (gameOverPanel != null)
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
