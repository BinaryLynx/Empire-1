using System;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{

    // Time settings (adjustable in inspector)
    [Header("Time Settings")]
    [SerializeField] private float _realSecondsPerGameMinute = 0.5f;
    [SerializeField] private int _startDay = 1;
    [SerializeField] private int _startHour = 6;
    [SerializeField] private int _startMinute = 0;

    // Current time state
    private int _totalMinutes;
    private float _minuteTimer;

    // Time speed multiplier (1 = normal speed)
    private float _timeMultiplier = 1f;

    // Events with time change information
    public event Action<int> OnMinuteChanged;
    public event Action<int> OnHourChanged;
    public event Action<int> OnDayChanged;

    // Properties for external access
    public int Minute => _totalMinutes % 60;
    public int Hour => (_totalMinutes / 60) % 24;
    public int Day => _totalMinutes / (60 * 24);
    public float TimeMultiplier => _timeMultiplier;
    public int TotalMinutes => _totalMinutes;

    protected override void Awake()
    {
        base.Awake();
        _totalMinutes = _startMinute + (_startHour * 60) + (_startDay * 60 * 24);
    }

    private void Update()
    {
        _minuteTimer += Time.deltaTime * _timeMultiplier;

        if (_minuteTimer >= _realSecondsPerGameMinute)
        {
            AdvanceTime(1); // Advance by 1 minute
            _minuteTimer = 0f;
        }
    }

    public void AdvanceTime(int minutesToAdvance)
    {
        if (minutesToAdvance <= 0) return;

        int previousDay = Day;
        int previousHour = Hour;
        int previousMinute = Minute;

        _totalMinutes += minutesToAdvance;

        // Calculate how many full minutes/hours/days have passed
        int minutesPassed = minutesToAdvance;
        int hoursPassed = (Hour - previousHour + 24) % 24;
        int daysPassed = Day - previousDay;

        // Fire minute changed event with total minutes passed
        OnMinuteChanged(minutesPassed);

        // Fire hour changed event if hours changed
        if (Hour != previousHour)
        {
            OnHourChanged(hoursPassed);
        }

        // Fire day changed event if days changed
        if (Day != previousDay)
        {
            OnDayChanged(daysPassed);
        }
    }

    // ... (rest of the methods remain the same as previous version)
    public void SetTimeMultiplier(float multiplier)
    {
        _timeMultiplier = Mathf.Max(0, multiplier);
    }
    public void PauseTime()
    {
        SetTimeMultiplier(0f);
    }

    public void ResumeTime()
    {
        SetTimeMultiplier(1f);
    }
    public string GetTimeString()
    {
        return $"{Hour:00}:{Minute:00}";
    }

    public string GetDateString()
    {
        return $"Day {Day + 1}";
    }

    public float GetDayProgress()
    {
        return (Hour + Minute / 60f) / 24f;
    }

    public bool IsDaytime()
    {
        // Assuming daytime is between 6AM and 8PM
        return Hour >= 6 && Hour < 20;
    }
    // Save/Load functionality
    public TimeSaveData GetSaveData()
    {
        return new TimeSaveData
        {
            totalMinutes = _totalMinutes,
            minuteTimer = _minuteTimer,
            timeMultiplier = _timeMultiplier
        };
    }

    public void LoadFromSaveData(TimeSaveData data)
    {
        _totalMinutes = data.totalMinutes;
        _minuteTimer = data.minuteTimer;
        _timeMultiplier = data.timeMultiplier;

        // Trigger all change events to ensure systems are updated
        // OnMinuteChanged();
        // OnHourChanged();
        // OnDayChanged();
    }
}

[Serializable]
public class TimeSaveData
{
    public int totalMinutes;
    public float minuteTimer;
    public float timeMultiplier;
}