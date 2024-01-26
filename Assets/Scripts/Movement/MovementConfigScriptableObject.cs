using UnityEngine;

[CreateAssetMenu(fileName = "MovementConfig", menuName = "ScriptableObjects/MovementConfigScriptableObject")]
public class MovementConfigScriptableObject : ScriptableObject
{
    public float TorqueMultiplier;
    public float ForceMultiplier;
    public float MaxHoldTime;
    public float BoostThresholdTime;
    public float LegCooldown;
    public float LegCooldownBoost;
}
