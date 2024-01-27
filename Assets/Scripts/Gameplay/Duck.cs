using System;
using UnityEngine;

public class Duck : SingletonBehaviour<Duck>
{
    public const string DUCK_TAG = "Duck";

    [SerializeField] private Obstacles _obstacles = null;

    private Action _onDeathAction = null;

    protected override void Awake()
    {
        base.Awake();
        transform.tag = DUCK_TAG;
    }

    private void OnDestroy()
    {
        _onDeathAction = null;
    }

    public void SetOnDeathAction(Action onDeathAction)
    {
        _onDeathAction += onDeathAction;
    }

    public void Collide(ObstacleType obstacleType)
    {
        string message = _obstacles.GetObstacleMessageByType(obstacleType);
        if (message == string.Empty) Debug.LogWarning("Warning: no data or message for obstacle of type " + obstacleType.ToString());
        else
        {
            // TODO: spawn message
        }

        // TODO: connect to movement controller to stop movement and animate death
        _onDeathAction?.Invoke();
    }
}
