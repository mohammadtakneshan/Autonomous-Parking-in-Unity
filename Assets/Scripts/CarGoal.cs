using UnityEngine;

public class CarGoal : MonoBehaviour
{
    private CarAgent agent = null; // Reference to the CarAgent that interacts with the goal.

    [SerializeField]
    private GoalType goalType = GoalType.Milestone; // Type of goal (Milestone or FinalDestination).

    [SerializeField]
    private float goalReward = 0.1f; // Reward value given to the agent upon reaching the goal.

    [SerializeField]
    private bool enforceGoalMinRotation = false; // Whether to enforce a minimum rotation requirement for the goal.

    [SerializeField]
    private float goalMinRotation = 10.0f; // Minimum allowed rotation for a successful goal.

    // Prevents the AI from reusing the same goal to gain rewards repeatedly.
    public bool HasCarUsedIt { get; set; } = false;

    // Enum to define the type of goal.
    public enum GoalType
    {
        Milestone, // Intermediate goal in the path.
        FinalDestination // Final goal, such as parking.
    }

    void OnTriggerEnter(Collider collider)
    {
        // Check if the colliding object is tagged as "player" and the goal hasn't been used yet.
        if (collider.transform.tag.ToLower() == "player" && !HasCarUsedIt)
        {
            // Retrieve the CarAgent component from the parent hierarchy.
            agent = transform.parent.GetComponentInChildren<CarAgent>();

            if(goalType == GoalType.Milestone) // Check if the goal is a milestone.
            {
                HasCarUsedIt = true; // Mark the goal as used.
                agent.GivePoints(goalReward); // Reward the agent for reaching the milestone.
            }
            else // If the goal is a FinalDestination.
            {
                // Ensure the car's rotation is within the acceptable range, or enforceGoalMinRotation is false.
                if(Mathf.Abs(agent.transform.rotation.y) <= goalMinRotation || !enforceGoalMinRotation)
                {
                    HasCarUsedIt = true; // Mark the goal as used.
                    agent.GivePoints(goalReward, true); // Reward the agent with a final goal reward.
                }
                else // If rotation condition is not met.
                {
                    agent.TakeAwayPoints(); // Penalize the agent.
                }
            }
        }
    }
}