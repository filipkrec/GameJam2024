using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class SoapBar : Obstacle
{
    [SerializeField] private Vector2 _lifetimeRange = Vector2.zero;
    [SerializeField] private Rigidbody _rigidbody;

    private float _lifetime = 0.0f;
    private Vector3 _startingPosition = Vector3.zero;

    private float _timer = 0.0f;

    private void Awake()
    {
        _type = ObstacleType.SoapBar;

        _lifetime = Random.Range(_lifetimeRange.x, _lifetimeRange.y);
        _startingPosition = transform.position;

        Respawn();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _lifetime)
        {
            _timer = 0.0f;
            Respawn();
        }
    }

    private void Respawn()
    {
        _rigidbody.velocity = Vector3.zero;
        transform.position = _startingPosition;
    }
}
