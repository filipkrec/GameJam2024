using DG.Tweening;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints = null;
    [SerializeField] private float _pathDuration = 0.0f;
    [SerializeField] private bool _shouldLookAtTargetPosition = true;

    private Vector3[] _positions = null;

    private void Awake()
    {
        _positions = new Vector3[_waypoints.Length];
        for (int index = 0; index < _waypoints.Length; index++) _positions[index] = _waypoints[index].position;

        if (_shouldLookAtTargetPosition)
        {
            transform
                .DOPath(_positions, _pathDuration)
                .OnWaypointChange((index) => transform.LookAt(_positions[index + 1 > _positions.Length - 1 ? 0 : index + 1]))
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }
        else
        {
            transform
                .DOPath(_positions, _pathDuration)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }
    }
}
