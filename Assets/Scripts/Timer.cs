using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeRemaining;
    private TextMeshProUGUI timerText;

    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime();
        }
    }

    void DisplayTime()
    {
        TimeSpan ts = TimeSpan.FromSeconds(timeRemaining);
        timerText.text = ts.ToString(@"mm\:ss");
    }

}
