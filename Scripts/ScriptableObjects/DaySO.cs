using System.Collections.Generic;
using UnityEngine;

// Editörde listelenebilmesi için Serializable olmasý þart
[System.Serializable]
public struct TaskRequirement
{
    public TaskType type;
    public TaskDifficulty difficulty;
    public int count; // Bu türden ve zorluktan kaç tane görev isteniyor?
}

[CreateAssetMenu(fileName = "DayScriptableObjects", menuName = "ScriptableObjects/DayScriptableObjects")]

public class DaySO : ScriptableObject
{
    public int dayIndex; // Örn: 1, 2, 3
    public string dayTitle; // Örn: "Oryantasyon Günü" veya "Kriz Günü"

    [Header("Günün Görev Havuzu Kurallarý")]
    public List<TaskRequirement> taskRequirements;

    // Ýleride buraya: O günkü baþlangýç saati, o güne özel bir kriz eventi var mý (bool) gibi þeyler eklenebilir.
}