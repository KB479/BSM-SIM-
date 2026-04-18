using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    public static TaskManager Instance { get; private set; }

    [Header("Database")]
    public TaskDatabaseSO taskDatabase;

    [Header("Task Pools (Inspector'dan izlemek için)")]
    [SerializeField] private List<TaskSO> availableTasksPool = new List<TaskSO>();
    [SerializeField] private List<TaskSO> activeDailyTasks = new List<TaskSO>();

    public event EventHandler OnDailyTasksReady; 


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There are more than one Task Manager Instance!"); 
        }

        Instance = this;
    }

    private void Start()
    {
        if (taskDatabase != null)
        {
            availableTasksPool = new List<TaskSO>(taskDatabase.allTasks);
        }
        else
        {
            Debug.LogError("TaskDatabase atanmamýþ!");
        }


        GameManager.Instance.OnNewDayStarted += GameManager_OnNewDayStarted;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnNewDayStarted -= GameManager_OnNewDayStarted;
        }
    }



    private void GameManager_OnNewDayStarted(object sender, GameManager.OnNewDayStartedEventArgs e)
    {
        GenerateDailyTasks(e.currentDaySO);
    }

    private void GenerateDailyTasks(DaySO daySO)
    {
        activeDailyTasks.Clear(); 

        if (daySO == null) return;

        foreach (TaskRequirement req in daySO.taskRequirements)
        {
            List<TaskSO> matchingTasks = availableTasksPool.FindAll(t => t.type == req.type && t.difficulty == req.difficulty);

            for (int i = 0; i < req.count; i++)
            {
                if (matchingTasks.Count > 0)
                {
       
                    TaskSO selectedTask = matchingTasks[0];

                    activeDailyTasks.Add(selectedTask);

                    availableTasksPool.Remove(selectedTask);
                    matchingTasks.Remove(selectedTask);
                }
                else
                {
                    Debug.LogWarning($"Havuzda yeterli {req.difficulty} seviye {req.type} görevi kalmadý!");
                }
            }
        }

        Debug.Log($"Görevler hazýrlandý! O gün için toplam: {activeDailyTasks.Count} görev listelendi.");


        OnDailyTasksReady?.Invoke(this, EventArgs.Empty);
    }


    public void ProcessTaskResult(TaskSO completedTask, bool isSuccess)
    {
        int creditChange = isSuccess ? completedTask.successCreditReward : -completedTask.failCreditPenalty;

        GameManager.Instance.ModifyCredit(creditChange);

        activeDailyTasks.Remove(completedTask);

        Debug.Log($"Görev Sonuçlandý: {completedTask.taskName} | Baþarý: {isSuccess} | Kalan Görev: {activeDailyTasks.Count}");

        if (activeDailyTasks.Count == 0)
        {
            GameManager.Instance.OnAllTasksCompletedForToday();
        }
    }

    public List<TaskSO> GetActiveDailyTasks()
    {
        return activeDailyTasks;
    }



}
