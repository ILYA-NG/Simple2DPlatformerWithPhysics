using UnityEngine;
using TMPro;
using UnityEngine.UI;  // обязательно подключить пространство имён для TextMeshPro
using UnityEngine.SceneManagement;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            string levelName = SceneManager.GetActiveScene().name;   
            scoreText.text = "" + levelName + "\nScore: " + score.ToString();
            scoreText.color = new Color(0f, 0f, 0.5f);
        }
    }
}
