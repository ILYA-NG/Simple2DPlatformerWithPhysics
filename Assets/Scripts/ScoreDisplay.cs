using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        UpdateScoreText(0);
    }

    void UpdateScoreText(int score)
    {
        if (scoreText != null)
        {
            string levelName = SceneManager.GetActiveScene().name;   
            scoreText.text = levelName + "\nScore: " + score.ToString();
            scoreText.color = new Color(0f, 0f, 0.5f);
        }
    }

    public void UpdateScore(int currentScore)
    {
        UpdateScoreText(currentScore);
    }
}
