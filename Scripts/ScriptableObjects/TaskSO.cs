using UnityEngine;

[CreateAssetMenu(fileName = "TaskScriptableObject", menuName = "ScriptableObjects/TaskScriptableObject")]
public class TaskSO : ScriptableObject
{
    [Header("Görev Kimliði")]
    public string taskID; // Örn: "SW-001" (Ýleride kayýt sistemi yaparsan çok iþe yarar)
    public string taskName;

    [Header("Görev Özellikleri")]
    public TaskType type;
    public TaskDifficulty difficulty;

    [TextArea(3, 5)]
    public string description; // "Þirketin ana sunucusuna sýzýldý, güvenlik duvarýný onar!"

    [Header("Sonuç Etkileri")]
    public int successCreditReward = 10;
    public int failCreditPenalty = 15;

    // Ýleride buraya: "Gereken Süre", "Ýlgili Departman Morali Etkisi" gibi þeyler eklenebilir.
}