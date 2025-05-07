using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Plant : MonoBehaviour
{
    public GrowthStage[] growthStages;

    public int currentStageIndex = 0;
    public int hoursInCurrentStage = 0;
    public bool isFullyGrown = false;
    private Action<int> _onHourChangedHandler;

    public YieldResult yieldResult;

    protected virtual void OnEnable()
    {
        _onHourChangedHandler = (timePassed) => Grow(timePassed); // Assign the anonymous function
        TimeManager.Instance.OnHourChanged += _onHourChangedHandler; // Subscribe

    }

    protected virtual void OnDisable()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnHourChanged -= _onHourChangedHandler;
        }
    }

    protected virtual void Start()
    {
        UpdatePlantModel();
    }

    private void UpdatePlantModel()
    {
        foreach (var stage in growthStages)
        {
            stage.model.SetActive(false);
        }

        growthStages[currentStageIndex].model.SetActive(true);
    }

    public virtual int Grow(int hoursPassed)
    {
        if (isFullyGrown)
        {
            return hoursPassed;
        }

        int remainderTotal = hoursPassed;
        while (currentStageIndex < growthStages.Length - 1)
        {
            int remainderInCurrentStage = growthStages[currentStageIndex].durationHours - hoursInCurrentStage;
            int hoursAdded = Mathf.Min(remainderInCurrentStage, remainderTotal);
            hoursInCurrentStage += hoursAdded;
            remainderTotal -= hoursAdded;
            if (hoursInCurrentStage == growthStages[currentStageIndex].durationHours)
            {
                currentStageIndex += 1;
            }
            if (remainderTotal <= 0)
            {
                UpdatePlantModel();
                return remainderTotal;
            }
        }

        isFullyGrown = true;
        return remainderTotal;
    }

    public abstract List<InventoryItem> Harvest(float efficency_multiplier);
}
