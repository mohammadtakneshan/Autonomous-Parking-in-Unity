using Unity.MLAgents.Policies; // Import for configuring agent behavior parameters.
using Unity.MLAgents.Sensors;  // Import for defining observations collected by the agent.
using UnityEngine;            // Import Unity-specific functionality.
using static CarController;   // Allow direct access to `CarController` enums or static members.

public class CarAgent : BaseAgent // Inherits from BaseAgent for shared functionality like material swapping.
{
    private Vector3 originalPosition; // Stores the agent's starting position.
    private BehaviorParameters behaviorParameters; // Handles ML-Agent behavior settings (e.g., training vs. inference).
    private CarController carController; // Reference to the car's control system.
    private Rigidbody carControllerRigidBody; // Rigidbody component for physical interactions.
    private CarSpots carSpots; // Reference to parking spots management.

    public override void Initialize()
    {
        originalPosition = transform.localPosition; // Cache the initial position for resetting.
        behaviorParameters = GetComponent<BehaviorParameters>(); // Access agent behavior settings.
        carController = GetComponent<CarController>(); // Get the car controller component.
        carControllerRigidBody = carController.GetComponent<Rigidbody>(); // Get the car's rigidbody for physics.
        carSpots = transform.parent.GetComponentInChildren<CarSpots>(); // Access the car spots system.

        ResetParkingLotArea(); // Initialize the parking lot and car states.
    }

    public override void OnEpisodeBegin()
    {
        ResetParkingLotArea(); // Reset the environment and agent for a new episode.
    }

    private void ResetParkingLotArea()
    {
        carController.IsAutonomous = behaviorParameters.BehaviorType == BehaviorType.Default; 
        // Set control mode based on behavior type (manual or autonomous).
        
        transform.localPosition = originalPosition; // Reset agent position.
        transform.localRotation = Quaternion.identity; // Reset agent rotation.
        carControllerRigidBody.velocity = Vector3.zero; // Stop all linear motion.
        carControllerRigidBody.angularVelocity = Vector3.zero; // Stop all rotational motion.

        carSpots.Setup(); // Reset the parking spots to their initial state.
    }

    void Update()
    {
        if (transform.localPosition.y <= 0) // Check if the agent has fallen out of bounds.
        {
            TakeAwayPoints(); // Penalize and end the episode.
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition); // Observe the agent's position.
        sensor.AddObservation(transform.rotation); // Observe the agent's rotation.

        sensor.AddObservation(carSpots.CarGoal.transform.position); // Observe the goal's position.
        sensor.AddObservation(carSpots.CarGoal.transform.rotation); // Observe the goal's rotation.

        sensor.AddObservation(carControllerRigidBody.velocity); // Observe the car's current velocity.
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        var direction = Mathf.FloorToInt(vectorAction[0]); // Decode action as an integer.

        switch (direction) // Execute action based on the decoded direction.
        {
            case 0: // Idle
                carController.CurrentDirection = Direction.Idle;
                break;
            case 1: // Move forward
                carController.CurrentDirection = Direction.MoveForward;
                break;
            case 2: // Move backward
                carController.CurrentDirection = Direction.MoveBackward;
                break;
            case 3: // Turn left
                carController.CurrentDirection = Direction.TurnLeft;
                break;
            case 4: // Turn right
                carController.CurrentDirection = Direction.TurnRight;
                break;
        }

        AddReward(-1f / MaxStep); // Apply a small time-based penalty to encourage efficiency.
    }

    public void GivePoints(float amount = 1.0f, bool isFinal = false)
    {
        AddReward(amount); // Add a positive reward to the agent.

        if (isFinal) // If this is the final reward, end the episode.
        {
            StartCoroutine(SwapGroundMaterial(successMaterial, 0.5f)); // Visual feedback for success.
            EndEpisode(); // End the episode.
        }
    }

    public void TakeAwayPoints()
    {
        StartCoroutine(SwapGroundMaterial(failureMaterial, 0.5f)); // Visual feedback for failure.

        AddReward(-0.01f); // Add a small penalty.
        EndEpisode(); // End the episode.
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0; // Default to idle.

        if (Input.GetKey(KeyCode.UpArrow)) // If the up arrow is pressed, move forward.
        {
            actionsOut[0] = 1;
        }

        if (Input.GetKey(KeyCode.DownArrow)) // If the down arrow is pressed, move backward.
        {
            actionsOut[0] = 2;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && carController.canApplyTorque()) // Turn left if allowed.
        {
            actionsOut[0] = 3;
        }

        if (Input.GetKey(KeyCode.RightArrow) && carController.canApplyTorque()) // Turn right if allowed.
        {
            actionsOut[0] = 4;
        }
    }
}
