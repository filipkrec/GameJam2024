using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ShampooBottle : Obstacle
{
    [SerializeField] private List<Transform> _waypoints = new List<Transform>();
    [SerializeField] private float _loopTime = 0.0f;
    private List<Vector3> _positions = new List<Vector3>();

    private void Awake()
    {
        _type = ObstacleType.ShampooBottle;

        foreach(Transform waypoint in _waypoints)
        {
            _positions.Add(waypoint.position);
        }

        _positions.Add(transform.position);

        Sequence sequence = DOTween.Sequence();

        float[] distanceWeights = new float[_waypoints.Count + 1];
        float totalDistance = 0f;

        for (int i = 0; i < _positions.Count; i++)
        {
            int next = i + 1;
            if(i == _positions.Count - 1)
            {
                next = 0;
            }

            distanceWeights[i] = Vector3.Distance(_positions[i], _positions[next]);
            totalDistance += distanceWeights[i];
        }

        for(int i = 0; i < _positions.Count; ++i)
        {
            distanceWeights[i] = distanceWeights[i] / totalDistance;
        }

        for (int index = 0; index < _positions.Count; index++)
        {
            float duration = distanceWeights[index] * _loopTime;
            sequence.Append(transform.DOMove(_positions[index], duration));
        }

        sequence.SetLoops(-1);
        sequence.Play();
    }
}
