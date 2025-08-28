using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthBar : MonoBehaviour
{
    public GameObject heartPrefab; // префаб с Image сердца
    public int maxHealth = 5;
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;

    private List<Image> hearts = new List<Image>();

    private int currentHealth;

    void Start()
    {
        //currentHealth = maxHealth;
        CreateHearts();
        //UpdateHearts();
    }
    void CreateHearts()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject heart = Instantiate(heartPrefab, transform);
            heart.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * 30, 0);
            RectTransform rt = heart.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(30, 30); // меняем размер: ширина и высота
            hearts.Add(heart.GetComponent<Image>());
        }
    }
    public void UpdateHealth(int health)
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].sprite = (i < health) ? fullHeartSprite : emptyHeartSprite;
        }
       // UpdateHearts();
    }
    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeartSprite;
            }
            else
            {
                hearts[i].sprite = emptyHeartSprite;
            }
        }
    }
}
