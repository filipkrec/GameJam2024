using System;
using UnityEngine;
using UnityEngine.UI;

public class DuckLeg : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private MovementConfigScriptableObject movementConfig;
    [SerializeField] private ParticleSystem legTrail;

    [Header("Setup")]
    [SerializeField] private bool torqueClockwise;

#if UNITY_EDITOR
    [Header("Test")]
    [SerializeField] private Image imagePreview;
#endif

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
                StartMovement();
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
                EndMovement();
            }
            else if (timer > movementConfig.MaxHoldTime)
            {
                FlashColor(Color.red);
                EndMovement();
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
#if UNITY_EDITOR
        if (imagePreview != null)
        {
            imagePreview.color = _color;
        }
#endif
    }

    public void Boost()
    {
        timer = 0f;
        cooldown = movementConfig.LegCooldownBoost;
        rigidBody.AddForce(transform.rotation * Vector3.forward * movementConfig.BoostForceMultiplier, ForceMode.Impulse);
        legTrail.Stop();
    }

    public void StartTempo()
    {
        tempoMultiplier = movementConfig.TempoForceMultiplier;
    }

    public void EndTempo()
    {
        tempoMultiplier = 1f;
    }

    private void StartMovement()
    {
        legTrail.Play();
        MovementStarted?.Invoke(this);
    }

    private void EndMovement()
    {
        legTrail.Stop();
        MovementStopped?.Invoke(this);
    }
}
