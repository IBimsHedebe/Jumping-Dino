using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add TextMeshPro support
using System.Collections.Generic;

public class PlatformTracker : MonoBehaviour
{
    private HashSet<GameObject> touchedPlatforms = new HashSet<GameObject>();
    public int UniquePlatformCount => touchedPlatforms.Count;
    
    [Header("UI Elements")]
    public Text platformCountText; // Legacy UI text
    public TextMeshProUGUI platformCountTMP; // TextMeshPro text (recommended)
    
    [Header("Events")]
    public UnityEngine.Events.UnityEvent<int> OnPlatformCountChanged;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            if (touchedPlatforms.Add(collision.collider.gameObject))
            {
                Debug.Log("Touched new platform! Total: " + touchedPlatforms.Count);
                UpdateUI();
                OnPlatformCountChanged?.Invoke(touchedPlatforms.Count);
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            if (touchedPlatforms.Add(other.gameObject))
            {
                Debug.Log("Touched new platform! Total: " + touchedPlatforms.Count);
                UpdateUI();
                OnPlatformCountChanged?.Invoke(touchedPlatforms.Count);
            }
        }
    }
    
    void UpdateUI()
    {
        string displayText = "Platforms: " + touchedPlatforms.Count;
        
        if (platformCountText != null)
        {
            platformCountText.text = displayText;
        }
        
        if (platformCountTMP != null)
        {
            platformCountTMP.text = displayText;
        }
    }
    
    public void ResetCount()
    {
        touchedPlatforms.Clear();
        UpdateUI();
        OnPlatformCountChanged?.Invoke(0);
        Debug.Log("Platform count reset!");
    }
    
    public bool HasTouchedPlatform(GameObject platform)
    {
        return touchedPlatforms.Contains(platform);
    }
}
