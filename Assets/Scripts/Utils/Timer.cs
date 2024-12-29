using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    public float timeRemaining;
    private Label textElement;
    public bool isActive = false;

    void Update()
    {
        if (timeRemaining > 0 && isActive)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime();
        }
    }

    public void Run()
    {
        if (!isActive)
        {
            isActive = true;
        }
    }

    public void Stop()
    {
        if (isActive)
        {
            isActive = false;
        }
    }

    void DisplayTime()
    {
        if (textElement == null)
        {
            textElement = GameUI.Instance.waveCountdown;
        }
        TimeSpan ts = TimeSpan.FromSeconds(timeRemaining);
        textElement.text = ts.ToString(@"mm\:ss\:ff");
    }

}
