using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private DuckLeg leftLeg;
    [SerializeField] private DuckLeg rightLeg;

    [Header("Input")]
    [SerializeField] private KeyCode KeyCodeLeft;
    [SerializeField] private KeyCode KeyCodeRight;

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0f, transform.localRotation.eulerAngles.y, 0f));
        leftLeg.MoveLeg(KeyCodeLeft);
        rightLeg.MoveLeg(KeyCodeRight);
    }
}
