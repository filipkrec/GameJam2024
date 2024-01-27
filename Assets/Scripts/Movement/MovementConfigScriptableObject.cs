using UnityEngine;

[CreateAssetMenu(fileName = "MovementConfig", menuName = "ScriptableObjects/MovementConfigScriptableObject")]
public class MovementConfigScriptableObject : ScriptableObject
{
    public float TorqueMultiplier;
    public float ForceMultiplier;
    public float BoostForceMultiplier;
    public float TempoForceMultiplier;
    public float MaxHoldTime;
    public float LegCooldown;
    public float LegCooldownBoost;
    public float BoostThresholdTime;
    public float TempoThresholdTime;
    public float TempoBreakTime;
}
