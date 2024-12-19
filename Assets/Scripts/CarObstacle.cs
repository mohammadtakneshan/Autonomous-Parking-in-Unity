using UnityEngine;

public class CarObstacle : MonoBehaviour
{
    // Enumeration to define different types of obstacles.
    public enum CarObstacleType
    { 
        Barrier, // Represents a barrier obstacle.
        Tree,    // Represents a tree obstacle.
        Car,     // Represents another car obstacle.
        Ground   // Represents the ground obstacle.
    }

    // Serialized field to set the obstacle type in the Unity editor.
    [SerializeField]
    private CarObstacleType carObstacleType = CarObstacleType.Barrier;

    // Property to expose the obstacle type.
    public CarObstacleType CarObstacleTypeValue { get { return this.carObstacleType; } }

    private CarAgent agent = null; // Reference to the car agent interacting with the obstacle.

    void Awake()
    {
        // Cache the CarAgent component from the hierarchy.
        agent = transform.parent.parent.GetComponentInChildren<CarAgent>();
    }

    // Trigger-based collision detection.
    void OnTriggerEnter(Collider collider)
    {
        // Check if the colliding object is tagged as "player".
        if (collider.transform.tag.ToLower() == "player")
        {
            agent.TakeAwayPoints(); // Penalize the agent for hitting the obstacle.
        }
    }

    // Collision-based detection for rigid body objects.
    void OnCollisionEnter(Collision other) 
    {
        // Check if the colliding object is tagged as "player".
        if (other.transform.tag.ToLower() == "player")
        {
            agent.TakeAwayPoints(); // Penalize the agent for colliding with the obstacle.
        }
    }
}