using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private MovementConfigScriptableObject movementConfig;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Collider duckCollider;
    [SerializeField] private DuckLeg leftLeg;
    [SerializeField] private DuckLeg rightLeg;
    [SerializeField] private ParticleSystem boostTrail;
    [SerializeField] private AudioClip swooshClip;
    [SerializeField] private AudioSource sfxSource;

    [Header("Input")]
    [SerializeField] private KeyCode KeyCodeLeft;
    [SerializeField] private KeyCode KeyCodeRight;

    private bool isTempoEvaluationActive;
    private bool isTempo;
    private bool isTempoInteruptionEvaluationActive;
    private float tempoTimer;
    private float tempoInteruptionTimer;
    private DuckLeg previousTempoLeg;

    private void Start()
    {
        leftLeg.MovementStarted = OnMovementStarted;
        leftLeg.MovementStopped = OnMovementStopped;

        rightLeg.MovementStarted = OnMovementStarted;
        rightLeg.MovementStopped = OnMovementStopped;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0f, transform.localRotation.eulerAngles.y, 0f));
        leftLeg.MoveLeg(KeyCodeLeft);
        rightLeg.MoveLeg(KeyCodeRight);

        if (leftLeg.CanBoost && rightLeg.CanBoost)
        {
            rightLeg.Boost();
            leftLeg.Boost();
            boostTrail.Play();
        }

        if (isTempoEvaluationActive && !isTempo)
        {
            tempoTimer += Time.deltaTime;
            if (tempoTimer > movementConfig.TempoThresholdTime)
            {
                StartTempo();
            }
        }

        if (isTempoInteruptionEvaluationActive)
        {
            tempoInteruptionTimer += Time.deltaTime;
            if (tempoInteruptionTimer > movementConfig.TempoBreakTime)
            {
                EndTempo();
            }
        }
    }

    private void StartTempo()
    {
        isTempoEvaluationActive = false;
        isTempo = true;
        leftLeg.StartTempo();
        rightLeg.StartTempo();
    }

    private void EndTempo()
    {
        isTempo = false;
        leftLeg.EndTempo();
        rightLeg.EndTempo();

        tempoTimer = 0f;
        tempoInteruptionTimer = 0f;
        previousTempoLeg = null;
    }

    private void OnMovementStarted(DuckLeg _leg)
    {
        sfxSource.Stop();
        sfxSource.clip = swooshClip;
        sfxSource.Play();

        if (tempoTimer <= 0f)
        {
            isTempoEvaluationActive = true;
        }
        else
        {
            if (_leg != previousTempoLeg && isTempoInteruptionEvaluationActive)
            {
                isTempoInteruptionEvaluationActive = false;
                tempoInteruptionTimer = 0f;
            }
        }
    }

    private void OnMovementStopped(DuckLeg _leg)
    {
        if (tempoTimer > 0f)
        {
            isTempoInteruptionEvaluationActive = true;
            previousTempoLeg = _leg;
        }
    }

    public void DisableMovementAndCollision()
    {
        rigidBody.isKinematic = true; duckCollider.enabled = false; Destroy(this);
    }
}
