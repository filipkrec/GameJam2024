using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    protected ObstacleType _type = ObstacleType.None;
    public ObstacleType Type => _type;
}