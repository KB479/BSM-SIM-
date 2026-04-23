using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class InteractionUnit : MonoBehaviour, IInteractable
{
    [Header("Kimlik")]
    public TaskType type;
    public string unitName;

    [Header("Arayüz")]
    public TextMeshProUGUI taskTextUI; 

    private Queue<TaskSO> taskQueue = new Queue<TaskSO>();

    private TaskSO currentTask;

    private bool isProcessingResult = false;

    private void Start()
    {
        TaskManager.Instance.OnDailyTasksReady += TaskManager_OnDailyTasksReady;

        UpdateUI("Yeni gün bekleniyor...");
    }

    private void OnDestroy()
    {
        if (TaskManager.Instance != null)
        {
            TaskManager.Instance.OnDailyTasksReady -= TaskManager_OnDailyTasksReady;
        }
    }

    private void TaskManager_OnDailyTasksReady(object sender, EventArgs e)
    {
        List<TaskSO> allDailyTasks = TaskManager.Instance.GetActiveDailyTasks();

        taskQueue.Clear();

        foreach (TaskSO task in allDailyTasks)
        {
            if (task.type == type)
            {
                taskQueue.Enqueue(task);
            }
        }

        LoadNextTask();
    }

    private void LoadNextTask()
    {
        if (taskQueue.Count > 0)
        {
            currentTask = taskQueue.Dequeue();
            UpdateUI($"<b>{unitName}</b>\nGörev: {currentTask.taskName}\n<size=70%>[E] Çöz - [R] Hata Bildir</size>");
        }
        else
        {
            currentTask = null;
            UpdateUI($"<b>{unitName}</b>\nBekleyen iþ yok.");
        }
    }


    public void ResolveCurrentTask(bool isSuccess)
    {
        if (currentTask == null || isProcessingResult) return;

        StartCoroutine(ShowResultRoutine(isSuccess));
    }

    private IEnumerator ShowResultRoutine(bool isSuccess)
    {
        isProcessingResult = true;

        TaskManager.Instance.ProcessTaskResult(currentTask, isSuccess);

        string resultMark = isSuccess ? "<color=green> BAÞARILI</color>" : "<color=red> BAÞARISIZ</color>";
        UpdateUI($"<b>{unitName}</b>\n{currentTask.taskName}\n{resultMark}");

        yield return new WaitForSeconds(1.5f);

        isProcessingResult = false;
        LoadNextTask();
    }

    private void UpdateUI(string message)
    {
        if (taskTextUI != null)
        {
            taskTextUI.text = message;
        }
    }
}