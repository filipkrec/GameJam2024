using UnityEngine;
using UnityEngine.UI;

public class DuckLeg : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private MovementConfigScriptableObject movementConfig;

    [Header("Setup")]
    [SerializeField] private bool torqueClockwise;

    [Header("Test")]
    [SerializeField] private Image imagePreview;

    private float timer;
    private float cooldown;

    public void MoveLeg(KeyCode code)
    {
        if (cooldown <= 0f)
        {
            if (Input.GetKey(code) && timer < movementConfig.MaxHoldTime)
            {
                timer += Time.deltaTime;
                rigidBody.AddForce(transform.rotation * Vector3.forward * movementConfig.ForceMultiplier * Time.deltaTime, ForceMode.Force);

                float torqueMultiplier = torqueClockwise ? 1 : -1;
                rigidBody.AddTorque(Vector3.up * torqueMultiplier * movementConfig.TorqueMultiplier * Time.deltaTime, ForceMode.Force);
                FlashColor(Color.green);
            }
            else if (Input.GetKeyUp(code))
            {
                timer = 0f;
                cooldown = movementConfig.LegCooldown;
            }
            else if (timer > movementConfig.MaxHoldTime)
            {
                FlashColor(Color.red);
            }
            else
            {
                FlashColor(Color.white);
            }
        }
        else
        {
            FlashColor(Color.gray);
            cooldown -= Time.deltaTime;
        }
    }

    private void FlashColor(Color _color)
    {
        imagePreview.color = _color;
    }
}
