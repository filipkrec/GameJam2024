using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ShampooBottle : Obstacle
{
    [SerializeField] private List<Vector3> _waypoints = new List<Vector3>();
    [SerializeField] private float _speedPerOneUnit = 0.0f;

    private void Awake()
    {
        _type = ObstacleType.ShampooBottle;
        transform.position = _waypoints[0];

        Sequence sequence = DOTween.Sequence();
        float duration = 0.0f;

        for (int index = 1; index < _waypoints.Count; index++)
        {
            duration = _speedPerOneUnit;
            sequence.Append(transform.DOMove(_waypoints[index], duration));
        }
        duration = _speedPerOneUnit;
        sequence.Append(transform.DOMove(_waypoints[0], duration));

        sequence.SetLoops(-1);
        sequence.Play();
    }
}
