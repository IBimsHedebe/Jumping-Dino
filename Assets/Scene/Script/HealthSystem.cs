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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0; // Prevent health from going below zero
        }
        UpdateHealthIcons();
        if (currentHealth == 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Prevent health from exceeding max health
        }
        UpdateHealthIcons();
    }

    private void UpdateHealthIcons()
    {
        for (int i = 0; i < healthIcons.Length; i++)
        {
            if (i < currentHealth)
            {
                healthIcons[i].sprite = fullHealthIcon; // Set to full health icon
            }
            else
            {
                healthIcons[i].sprite = emptyHealthIcon; // Set to empty health icon
            }
        }
    }

    private void Die()
    {
        SceneManager.LoadScene("Game_Over_Scene"); // Load Game Over screen
    }
}
