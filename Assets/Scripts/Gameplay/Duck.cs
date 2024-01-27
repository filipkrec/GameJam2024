using System;
using UnityEngine;

public class Duck : SingletonBehaviour<Duck>
{
    public const string DUCK_TAG = "Duck";

    [SerializeField] private Obstacles _obstacles = null;

    [SerializeField] private DeathMessage _deathMessagePrefab = null;
    [SerializeField] private Vector3 _deathMessageSpawnOffset = Vector3.zero;
    [SerializeField] private float _deathMessageAppearDelay = 0.0f;

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
        AudioManager.Instance.PlaySoundEffectByType(SoundEffectType.Duck);

        string message = _obstacles.GetDeathMessageByObstacleType(obstacleType);
        if (message == string.Empty) Debug.LogWarning("Warning: no data or message for obstacle of type " + obstacleType.ToString());
        else
        {
            DeathMessage deathMessage = Instantiate(_deathMessagePrefab, transform.position + _deathMessageSpawnOffset, Quaternion.identity);
            deathMessage.SetUpMessage(message);
            deathMessage.Appear(transform.position, _deathMessageAppearDelay);
        }

        // TODO: animate duck death
        // TODO: connect to movement controller to stop movement and animate death
        _onDeathAction?.Invoke();
    }
}
