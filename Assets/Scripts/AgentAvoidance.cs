using TMPro; // Import TextMeshPro namespace for UI text management.
using Unity.MLAgents.Sensors; // Import ML-Agents namespace for sensor data.
using UnityEngine; // Import Unity engine base namespace.

public class AgentAvoidance : BaseAgent // Inherits from a custom BaseAgent class.
{
    [SerializeField]
    private float speed = 50.0f; // Speed at which the agent moves.

    [SerializeField]
    private Vector3 idlePosition = Vector3.zero; // Default "idle" position for the agent.

    [SerializeField]
    private Vector3 leftPosition = Vector3.zero; // Position the agent moves to when going left.

    [SerializeField]
    private Vector3 rightPosition = Vector3.zero; // Position the agent moves to when going right.

    [SerializeField]
    private TextMeshProUGUI rewardValue = null; // UI element displaying the cumulative reward.

    [SerializeField]
    private TextMeshProUGUI episodesValue = null; // UI element displaying the number of completed episodes.

    [SerializeField]
    private TextMeshProUGUI stepValue = null; // UI element displaying the total number of steps taken.

    private TargetMoving targetMoving = null; // Reference to the target object the agent interacts with.

    private float overallReward = 0; // Tracks the overall reward across episodes.

    private float overallSteps = 0; // Tracks the total steps across episodes.

    private Vector3 moveTo = Vector3.zero; // Current target position the agent is moving towards.

    private Vector3 prevPosition = Vector3.zero; // Tracks the agent's previous position.

    private int punishCounter; // Counter to track repeated actions (used for punishment).

    void Awake()
    {
        // Initialize targetMoving by finding the TargetMoving component within the parent object.
        targetMoving = transform.parent.GetComponentInChildren<TargetMoving>();
    }

    public override void OnEpisodeBegin()
    {
        // Reset agent's position to idle at the start of an episode.
        transform.localPosition = idlePosition;

        // Initialize movement and previous position to idle.
        moveTo = prevPosition = idlePosition;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Add the agent's current position as an observation (x, y, z).
        sensor.AddObservation(transform.localPosition);

        // Add the target's current position as an observation (x, y, z).
        sensor.AddObservation(targetMoving.transform.localPosition);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        prevPosition = moveTo; // Save the current moveTo position as the previous position.

        int direction = Mathf.FloorToInt(vectorAction[0]); // Convert the action received to an integer direction.
        moveTo = idlePosition; // Default movement is idle.

        switch (direction) // Determine the new position based on the action received.
        {
            case 0:
                moveTo = idlePosition; // Idle action.
                break;
            case 1:
                moveTo = leftPosition; // Move left.
                break;
            case 2:
                moveTo = rightPosition; // Move right.
                break;
        }

        // Move the agent towards the target position at a speed scaled by Time.fixedDeltaTime.
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, moveTo, Time.fixedDeltaTime * speed);

        // If the agent doesn't change position, increment the punishment counter.
        if (prevPosition == moveTo)
        {
            punishCounter++;
        }

        // If the agent repeats an action too many times, apply a small penalty.
        if (punishCounter > 3.0f)
        {
            AddReward(-0.01f); // Penalize the agent slightly.
            punishCounter = 0; // Reset the punishment counter.
        }
    }

    public void TakeAwayPoints()
    {
        AddReward(-0.01f); // Penalize the agent.
        targetMoving.ResetTarget(); // Reset the target's position.

        UpdateStats(); // Update UI statistics.

        EndEpisode(); // End the current episode.

        // Temporarily change ground material to indicate failure.
        StartCoroutine(SwapGroundMaterial(failureMaterial, 0.5f));
    }

    private void UpdateStats()
    {
        // Update cumulative reward and step count.
        overallReward += this.GetCumulativeReward();
        overallSteps += this.StepCount;

        // Update UI text for reward, episodes, and steps.
        rewardValue.text = $"{overallReward.ToString("F2")}";
        episodesValue.text = $"{this.CompletedEpisodes}";
        stepValue.text = $"{overallSteps}";
    }

    public void GivePoints()
    {
        AddReward(1.0f); // Reward the agent for success.
        targetMoving.ResetTarget(); // Reset the target's position.

        UpdateStats(); // Update UI statistics.

        EndEpisode(); // End the current episode.

        // Temporarily change ground material to indicate success.
        StartCoroutine(SwapGroundMaterial(successMaterial, 0.5f));
    }

    public override void Heuristic(float[] actionsOut)
    {
        // Manually specify actions during testing.
        // Idle action when the down arrow is pressed.
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            actionsOut[0] = 0;
        }

        // Move left when the left arrow is pressed.
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            actionsOut[0] = 1;
        }

        // Move right when the right arrow is pressed.
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            actionsOut[0] = 2;
        }
    }
}