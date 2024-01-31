using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Obstacles Scriptable Object", menuName = "Scriptable Objects/Obstacles Scriptable Object")]
public class ObstaclesScriptableObject : ScriptableObject
{
    [Serializable]
    private class ObstacleData
    {
        public ObstacleType Type = ObstacleType.None;
        public string[] DeathMessages = null;
    }

    [SerializeField] private List<ObstacleData> _obstacles = new List<ObstacleData>();

    public string GetDeathMessageByObstacleType(ObstacleType type)
    {
        if (type == ObstacleType.None) return string.Empty;

        ObstacleData targetData = _obstacles.Find((data) => data.Type == type);
        if (targetData != null && targetData.DeathMessages.Length > 0) return targetData.DeathMessages[targetData.DeathMessages.Length > 1 ? UnityEngine.Random.Range(0, targetData.DeathMessages.Length) : 0]; 
        return string.Empty;
    }
}
