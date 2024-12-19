using Unity.MLAgents.Sensors;
using UnityEngine;

// A Unity ML-Agents Player Agent class that interacts with an environment and learns via reinforcement learning.
public class PlayerAgent : BaseAgent
{
    #region Exposed Instance Variables

    [SerializeField]
    private float speed = 10.0f; // The movement speed of the player.

    [SerializeField]
    private GameObject target = null; // The target the agent needs to reach.

    [SerializeField]
    private float distanceRequired = 1.5f; // Minimum distance required to consider reaching the target.

    #endregion

    #region Private Instance Variables

    private Rigidbody playerRigidbody; // Reference to the player's Rigidbody component.

    private Vector3 originalPosition; // Stores the original position of the player.

    private Vector3 originalTargetPosition; // Stores the original position of the target.

    #endregion

    // Initializes the agent by setting up required references and initial states.
    public override void Initialize()
    {
        playerRigidbody = GetComponent<Rigidbody>(); // Cache the Rigidbody component.
        originalPosition = transform.localPosition; // Store the player's initial position.
        originalTargetPosition = target.transform.localPosition; // Store the target's initial position.
    }

    // Called at the start of each episode to reset the agent and target to their initial states.
    public override void OnEpisodeBegin()
    {
        transform.LookAt(target.transform); // Orient the player to face the target.
        target.transform.localPosition = originalTargetPosition; // Reset the target's position.
        transform.localPosition = originalPosition; // Reset the player's position.

        // Randomize the player's horizontal position within a range for diversity in training.
        transform.localPosition = new Vector3(Random.Range(-4, 4), originalPosition.y, originalPosition.z);
    }

    // Collects observations that the agent uses to make decisions.
    public override void CollectObservations(VectorSensor sensor)
    {
        // Add the player's current position as a 3D vector observation (x, y, z).
        sensor.AddObservation(transform.localPosition);

        // Add the target's current position as a 3D vector observation (x, y, z).
        sensor.AddObservation(target.transform.localPosition);

        // Add the player's x-velocity as a single observation.
        sensor.AddObservation(playerRigidbody.velocity.x);

        // Add the player's z-velocity as a single observation.
        sensor.AddObservation(playerRigidbody.velocity.z);
    }

    // Processes actions received from the agent's decision-making model.
    public override void OnActionReceived(float[] vectorAction)
    {
        // Convert the action array into a movement vector.
        var vectorForce = new Vector3
        {
            x = vectorAction[0], // Action for horizontal movement.
            z = vectorAction[1]  // Action for forward/backward movement.
        };

        // Apply the movement force to the player's Rigidbody, scaled by the speed variable.
        playerRigidbody.AddForce(vectorForce * speed);

        // Calculate the distance between the player and the target.
        var distanceFromTarget = Vector3.Distance(transform.localPosition, target.transform.localPosition);

        // Reward the agent if it gets close enough to the target.
        if (distanceFromTarget < distanceRequired)
        {
            AddReward(1); // Positive reward for success.
            EndEpisode(); // End the current episode.

            // Change the ground material to indicate success.
            StartCoroutine(SwapGroundMaterial(successMaterial, 0.5f));
        }

        // Punish the agent if it falls off the platform (y position below 0).
        if (transform.localPosition.y < 0)
        {
            EndEpisode(); // End the current episode.

            // Change the ground material to indicate failure.
            StartCoroutine(SwapGroundMaterial(failureMaterial, 0.5f));
        }
    }

    // Provides manual control input for testing/debugging during heuristic mode.
    public override void Heuristic(float[] actionsOut)
    {
        // Map the horizontal input (arrow keys or A/D) to the x-axis action.
        actionsOut[0] = Input.GetAxis("Horizontal");

        // Map the vertical input (arrow keys or W/S) to the z-axis action.
        actionsOut[1] = Input.GetAxis("Vertical");
    }
}