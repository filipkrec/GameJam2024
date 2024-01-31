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

    [SerializeField] private Transform _cameraTransform = null;
    [SerializeField] private Duck _duck = null;

    private bool _followDuck = true;

    private void Start()
    {
        _duck.OnWinAction += SetEndLevelCam;
        _duck.OnDeathAction += SetEndLevelCam;

        _cameraTransform.position = _duck.transform.position + _followOffset;
        _cameraTransform.LookAt(_duck.transform.position);
    }

    private void FixedUpdate()
    {
        if (_followDuck)
        {
            Vector3 finalPosition = _duck.transform.position + _followOffset;
            _cameraTransform.position = Vector3.SmoothDamp(_cameraTransform.position, finalPosition, ref _followVelocity, _followSmoothTime);
        }
    }

    private void SetEndLevelCam()
    {
        _followDuck = false;
        _cameraTransform.transform.position = _duck.transform.transform.position + _followOffset;
        Vector3 duckPosition = _duck.transform.position;
        _cameraTransform
            .DOMove(duckPosition + _defeatPositionOffset, _defeatAnimationDuration)
            .OnUpdate(() => _cameraTransform.LookAt(duckPosition));
    }
}
