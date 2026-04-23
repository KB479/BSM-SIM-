using UnityEngine;

[CreateAssetMenu(fileName = "TaskScriptableObject", menuName = "ScriptableObjects/TaskScriptableObject")]
public class TaskSO : ScriptableObject
{
    [Header("Görev Kimliði")]
    public string taskID; 
    public string taskName;

    [Header("Görev Özellikleri")]
    public TaskType type;
    public TaskDifficulty difficulty;

    [TextArea(3, 5)]
    public string description; 

    [Header("Sonuç Etkileri")]
    public int successCreditReward = 10;
    public int failCreditPenalty = 15;

}