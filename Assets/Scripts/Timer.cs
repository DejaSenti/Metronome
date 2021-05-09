using System;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public UnityEvent ElapsedEvent;

    private float duration;
    private float elapsed;

    private void Awake()
    {
        if (ElapsedEvent == null)
        {
            ElapsedEvent = new UnityEvent();
        }
    }

    public void StartTimer(float duration)
    {
        this.duration = duration;
        elapsed = 0;
        enabled = true;
    }

    private void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed >= duration)
        {
            ResetTimer();
            ElapsedEvent.Invoke();
        }
    }

    public void ResetTimer()
    {
        duration = 0;
        elapsed = 0;
        enabled = false;
    }
}