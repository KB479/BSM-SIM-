using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class InteractionUnit : MonoBehaviour
{
    [Header("Kimlik ")]
    public TaskType myType;    
    public string unitName;   

    [Header("Arayüz")]
    public TextMeshProUGUI taskTextUI; 

    private Queue<TaskSO> myTaskQueue = new Queue<TaskSO>();

    private TaskSO currentTask;

    private bool isProcessingResult = false;


    private void Start()
    {

        TaskManager.Instance.OnDailyTasksReady += TaskManager_OnDailyTasksReady;



    }

    private void TaskManager_OnDailyTasksReady(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}