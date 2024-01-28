using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Follow Values")]
    [SerializeField] private Vector3 _followOffset = Vector3.zero;
    [SerializeField] private Vector3 _followVelocity = Vector3.zero;
    [SerializeField] private float _followSmoothTime = 0.0f;

    [Header("Defeat Position Values")]
    [SerializeField] private Vector3 _defeatPositionOffset = Vector3.zero;
    [SerializeField] private float _defeatAnimationDuration = 0.0f;

    private Transform _cameraTransform = null;
    private Transform _duckTransform = null;

    private bool _isDuckAlive = true;

    private void Start()
    {
        _duckTransform = Duck.Instance.transform;
        Duck.Instance.SetOnDeathAction(() => _isDuckAlive = false);
        Duck.Instance.SetOnDeathAction(SwitchToDefeatPosition);

        Duck.Instance.SetOnWinAction(() => _isDuckAlive = false);
        Duck.Instance.SetOnWinAction(SwitchToDefeatPosition);

        _cameraTransform = Camera.main.transform;
        _cameraTransform.position = _duckTransform.position + _followOffset;
        _cameraTransform.LookAt(_duckTransform.position);
    }

    private void FixedUpdate()
    {
        if (_isDuckAlive)
        {
            Vector3 finalPosition = _duckTransform.position + _followOffset;
            _cameraTransform.position = Vector3.SmoothDamp(_cameraTransform.position, finalPosition, ref _followVelocity, _followSmoothTime);
        }
    }

    private void SwitchToDefeatPosition()
    {
        _cameraTransform.transform.position = _duckTransform.transform.position + _followOffset;
        Vector3 duckPosition = _duckTransform.position;
        _cameraTransform
            .DOMove(duckPosition + _defeatPositionOffset, _defeatAnimationDuration)
            .OnUpdate(() => _cameraTransform.LookAt(duckPosition));
    }
}
