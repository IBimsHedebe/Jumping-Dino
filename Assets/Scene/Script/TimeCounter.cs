using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    public float timeCounter = 0f;
    
    [Header("UI Elements")]
    public TextMeshProUGUI timeDisplayTMP; // TextMeshPro UI element
    
    [Header("Display Settings")]
    public string timeFormat = "Time: {0:F1}s"; // Format: "Time: 12.3s"
    public bool showMinutes = false; // Option to show minutes:seconds format

    void Update()
    {
        timeCounter += Time.deltaTime;
        UpdateTimeDisplay();
    }
    
    void UpdateTimeDisplay()
    {
        if (timeDisplayTMP != null)
        {
            if (showMinutes)
            {
                int minutes = Mathf.FloorToInt(timeCounter / 60f);
                int seconds = Mathf.FloorToInt(timeCounter % 60f);
                timeDisplayTMP.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                timeDisplayTMP.text = string.Format(timeFormat, timeCounter);
            }
        }
    }
    
    public void ResetTimer()
    {
        timeCounter = 0f;
        UpdateTimeDisplay();
    }
    
    public string GetFormattedTime()
    {
        if (showMinutes)
        {
            int minutes = Mathf.FloorToInt(timeCounter / 60f);
            int seconds = Mathf.FloorToInt(timeCounter % 60f);
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            return string.Format("{0:F1}s", timeCounter);
        }
    }
}
