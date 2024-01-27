using System;
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

    public Action<DuckLeg> MovementStarted;
    public Action<DuckLeg> MovementStopped;

    private float timer;
    private float cooldown;
    private float tempoMultiplier = 1f;

    public bool CanBoost => timer > 0.0f && timer < movementConfig.BoostThresholdTime;

    public void MoveLeg(KeyCode code)
    {
        if (cooldown <= 0f)
        {
            if(Input.GetKeyDown(code))
            {
                MovementStarted?.Invoke(this);
            }

            if (Input.GetKey(code) && timer < movementConfig.MaxHoldTime)
            {
                timer += Time.deltaTime;
                rigidBody.AddForce(transform.rotation * Vector3.forward * movementConfig.ForceMultiplier * tempoMultiplier * Time.deltaTime, ForceMode.Force);

                float torqueMultiplier = torqueClockwise ? 1 : -1;
                rigidBody.AddTorque(Vector3.up * torqueMultiplier * movementConfig.TorqueMultiplier * Time.deltaTime, ForceMode.Force);
                FlashColor(Color.green);
            }
            else if (Input.GetKeyUp(code))
            {
                timer = 0f;
                cooldown = movementConfig.LegCooldown;
                MovementStopped?.Invoke(this);
            }
            else if (timer > movementConfig.MaxHoldTime)
            {
                FlashColor(Color.red);
                MovementStopped?.Invoke(this);
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

    public void Boost()
    {
        timer = 0f;
        cooldown = movementConfig.LegCooldownBoost;
        rigidBody.AddForce(transform.rotation * Vector3.forward * movementConfig.BoostForceMultiplier, ForceMode.Impulse);
    }

    public void StartTempo()
    {
        tempoMultiplier = movementConfig.TempoForceMultiplier;
    }

    public void EndTempo()
    {
        tempoMultiplier = 1f;
    }
}
