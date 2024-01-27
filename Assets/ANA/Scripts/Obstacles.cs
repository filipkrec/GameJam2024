using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Obstacles Scriptable Object", menuName = "Scriptable Objects/New Obstacles Scriptable Object")]
public class Obstacles : ScriptableObject
{
    [Serializable]
    private class ObstacleData
    {
        public ObstacleType Type = ObstacleType.None;
        public string[] Messages = null;
    }

    [SerializeField] private List<ObstacleData> _obstacles = new List<ObstacleData>();

    public string GetObstacleMessageByType(ObstacleType type)
    {
        if (type == ObstacleType.None) return string.Empty;

        ObstacleData targetData = _obstacles.Find((data) => data.Type == type);
        if (targetData != null && targetData.Messages.Length > 0) return targetData.Messages[targetData.Messages.Length > 1 ? UnityEngine.Random.Range(0, targetData.Messages.Length) : 0]; 
        return string.Empty;
    }
}
