using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public Image[] healthIcons; // Array of health icon images
    public Sprite fullHealthIcon;
    public Sprite emptyHealthIcon;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthIcons();
    }

    void UpdateHealthIcons()
    {
        for (int i = 0; i < healthIcons.Length; i++)
        {
            if (i < currentHealth)
            {
                healthIcons[i].sprite = fullHealthIcon;
            }
            else
            {
                healthIcons[i].sprite = emptyHealthIcon;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthIcons();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle player death (e.g., show game over screen, reset level, etc.)
        Debug.Log("Player has died.");
        SceneManager.LoadScene("GameOver");
    }
}