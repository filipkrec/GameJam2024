using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Duck : MonoBehaviour
{
    public const string LAST_LEVEL_KEY = "LastLevel";

    public const string DUCK_DEATH_ANIMATOR_KEY = "IsDuckDead";
    public const string DUCK_VICTORY_ANIMATOR_KEY = "IsDuckWin";

    public const string DUCK_TAG = "Duck";

    [Header("Refs")]
    [SerializeField] private ObstaclesScriptableObject _obstacles = null;
    [SerializeField] private VictoryMessagesScriptableObject _victoryMessages;
    [SerializeField] private Animator _animator;
    [SerializeField] private DuckMovement _movement;
    [SerializeField] private PauseMenuManager _pauseMenuManager;

    //TODO Separate this out
    [SerializeField] private DeathMessage _deathMessagePrefab = null;
    [SerializeField] private Vector3 _deathMessageSpawnOffset = Vector3.zero;
    [SerializeField] private float _deathMessageAppearDelay = 0.0f;

    public Action OnDeathAction = null;
    public Action OnWinAction = null;

    private void Awake()
    {
        transform.tag = DUCK_TAG;
        PlayerPrefs.SetInt(LAST_LEVEL_KEY, SceneManager.GetActiveScene().buildIndex);

        OnDeathAction += _movement.DisableMovementAndCollision;
        OnWinAction += _movement.DisableMovementAndCollision;
    }

    public void Collide(ObstacleType obstacleType)
    {
        AudioManager.Instance.PlaySoundEffectByType(SoundEffectType.Duck);

        string message = _obstacles.GetDeathMessageByObstacleType(obstacleType);
        if (message == string.Empty) Debug.LogWarning("Warning: no data or message for obstacle of type " + obstacleType.ToString());
        else
        {
            ShowMessage(message);
        }

        _animator.SetBool(DUCK_DEATH_ANIMATOR_KEY, true);

        OnDeathAction?.Invoke();
    }

    public void Win()
    {
        ShowMessage(_victoryMessages.GetRandomMessage(), false);
        _animator.SetBool(DUCK_VICTORY_ANIMATOR_KEY, true);
        OnWinAction?.Invoke();
    }

    public void ShowMessage(string _message, bool showPopup = true)
    {
        DeathMessage deathMessage = Instantiate(_deathMessagePrefab, transform.position + _deathMessageSpawnOffset, _deathMessagePrefab.transform.rotation);
        deathMessage.SetUpMessage(_message, showPopup);
        deathMessage.Appear(transform.position, _deathMessageAppearDelay);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Obstacle obstacle))
        {
            Collide(obstacle.Type);
        }
    }
}
