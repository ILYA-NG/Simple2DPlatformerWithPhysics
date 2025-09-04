using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    bool gameHasEnded = false;
    public float restartDelay = 1f;
    public GameObject completeLevelUI;
    public int maxHealth = 5;
    public int currentHealth;
    public HealthBar healthBar;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // Инициализация здоровья при первом создании GameManager
            if (currentHealth <= 0)
            {
                currentHealth = maxHealth;
            }

        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        UpdateHealthBar();
    }

    public void TakeDamage()
    {
        currentHealth = Mathf.Max(0, currentHealth - 1);
        UpdateHealthBar();
        if (currentHealth == 0)
        {
            EndGame();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.UpdateHealth(currentHealth);
        }
        else
        {
            // Попытка найти HealthBar в загруженной сцене
            healthBar = FindObjectOfType<HealthBar>();
            if (healthBar != null)
            {
                healthBar.UpdateHealth(currentHealth);
            }
        }
    }

    public void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Этот метод вызывается при загрузке новой сцены, чтобы обновить HealthBar в новой сцене
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // При загрузке новой сцены ищем HealthBar и обновляем его
        healthBar = FindObjectOfType<HealthBar>();
        if (healthBar != null)
        {
            healthBar.UpdateHealth(currentHealth);
        }
    }
}