using System;
using UnityEngine;

public class Duck : SingletonBehaviour<Duck>
{
    public const string _DUCK_DEATH_ANIMATOR_KEY = "IsDuckDead";
    public const string _DUCK_VICTORY_ANIMATOR_KEY = "IsDuckWin";

    public const string DUCK_TAG = "Duck";

    [SerializeField] private Obstacles _obstacles = null;

    [SerializeField] private VictoryMessagesScriptableObject _victoryMessages;
    [SerializeField] private DeathMessage _deathMessagePrefab = null;
    [SerializeField] private Vector3 _deathMessageSpawnOffset = Vector3.zero;
    [SerializeField] private float _deathMessageAppearDelay = 0.0f;

    private bool _isDead = false;

    private Action _onDeathAction = null;
    private Action _onWinAction = null;

    protected override void Awake()
    {
        base.Awake();
        transform.tag = DUCK_TAG;
    }

    private void OnDestroy()
    {
        _onDeathAction = null;
        _onWinAction = null;
    }

    public void SetOnDeathAction(Action onDeathAction)
    {
        _onDeathAction += onDeathAction;
    }

    public void SetOnWinAction(Action onWinAction)
    {
        _onWinAction += onWinAction;
    }

    public void Collide(ObstacleType obstacleType)
    {
        if (_isDead) return;
        _isDead = true;

        AudioManager.Instance.PlaySoundEffectByType(SoundEffectType.Duck);

        string message = _obstacles.GetDeathMessageByObstacleType(obstacleType);
        if (message == string.Empty) Debug.LogWarning("Warning: no data or message for obstacle of type " + obstacleType.ToString());
        else
        {
            ShowMessage(message);
        }

        Animator animator = GetComponentInChildren<Animator>(); // the animator is on the child object
        if (animator != null) animator.SetBool(_DUCK_DEATH_ANIMATOR_KEY, true);

        _onDeathAction?.Invoke();
    }

    public void Win()
    {
        ShowMessage(_victoryMessages.GetRandomMessage());
        Animator animator = GetComponentInChildren<Animator>(); // the animator is on the child object
        if (animator != null) animator.SetBool(_DUCK_VICTORY_ANIMATOR_KEY, true);
        _onWinAction?.Invoke();
    }

    public void ShowMessage(string _message)
    {
        DeathMessage deathMessage = Instantiate(_deathMessagePrefab, transform.position + _deathMessageSpawnOffset, _deathMessagePrefab.transform.rotation);
        deathMessage.SetUpMessage(_message);
        deathMessage.Appear(transform.position, _deathMessageAppearDelay);
    }
}
