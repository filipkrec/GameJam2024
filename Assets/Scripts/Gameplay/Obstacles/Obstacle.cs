using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    protected ObstacleType _type = ObstacleType.None;

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(Duck.DUCK_TAG)) Duck.Instance.Collide(_type);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(Duck.DUCK_TAG)) Duck.Instance.Collide(_type);
    }
}