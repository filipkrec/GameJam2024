using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BathBomb : Obstacle
{
    [SerializeField] private Vector3 _bombDirection = Vector3.zero;
    [SerializeField] private Vector2 _bombSpeedRange = Vector2.zero;
    [SerializeField] private Vector2 _lifetimeRange = Vector2.zero;

    private Rigidbody _rigidbody = null;

    private float _bombSpeed = 0.0f;
    private float _lifetime = 0.0f;
    private Vector3 _startingPosition = Vector3.zero;

    private float _timer = 0.0f;

    private void Awake()
    {
        _type = ObstacleType.BathBomb;

        _rigidbody = GetComponent<Rigidbody>();

        _bombSpeed = Random.Range(_bombSpeedRange.x, _bombSpeedRange.y);
        _lifetime = Random.Range(_lifetimeRange.x, _lifetimeRange.y);
        _startingPosition = transform.position;

        RestartBomb();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _lifetime)
        {
            _timer = 0.0f;
            RestartBomb();
        }
    }

    private void RestartBomb()
    {
        _rigidbody.velocity = Vector3.zero;
        transform.position = _startingPosition;
        _rigidbody.velocity = _bombDirection * _bombSpeed;
    }
}
