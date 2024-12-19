using System.Collections; // Provides collections like ArrayLists.
using System.Collections.Generic; // Provides generic collections like List<T>.
using DilmerGames.Core; // Likely a custom library used for additional core functionality.
using UnityEngine; // Unity framework for game objects and components.

public class CarController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f; // Controls the forward/backward movement speed.

    [SerializeField]
    private float torque = 1.0f; // Controls the rotational torque for turning.

    [SerializeField]
    private float minSpeedBeforeTorque = 0.3f; // Minimum speed required to apply torque for turning.

    [SerializeField]
    private float minSpeedBeforeIdle = 0.2f; // Speed threshold below which the car switches to idle.

    [SerializeField]
    private Animator carAnimator; // Handles car animations for different states.

    public Direction CurrentDirection { get; set; } = Direction.Idle; // Tracks the car's current movement direction.

    public bool IsAutonomous { get; set; } = false; // Indicates if the car is controlled by AI (true) or manually (false).

    private Rigidbody carRigidBody; // Rigidbody component for applying physics-based movement.

    public enum Direction
    {
        Idle, // No movement.
        MoveForward, // Moving forward.
        MoveBackward, // Moving backward.
        TurnLeft, // Turning left.
        TurnRight // Turning right.
    }

    void Awake() 
    {
        carRigidBody = GetComponent<Rigidbody>(); // Initialize the Rigidbody reference.
    }

    void Update() 
    {
        // If the car's velocity falls below the idle threshold, set its state to idle.
        if(carRigidBody.velocity.magnitude <= minSpeedBeforeIdle)
        {
            CurrentDirection = Direction.Idle;
            ApplyAnimatorState(Direction.Idle); // Update animations to reflect idle state.
        }    
    }
    
    void FixedUpdate() => ApplyMovement(); // Continuously apply movement in the physics update cycle.

    public void ApplyMovement()
    {
        // Move forward: Responds to UpArrow input or autonomous forward action.
        if(Input.GetKey(KeyCode.UpArrow) || (CurrentDirection == Direction.MoveForward && IsAutonomous))
        {
            ApplyAnimatorState(Direction.MoveForward); // Update animations.
            carRigidBody.AddForce(transform.forward * speed, ForceMode.VelocityChange); // Apply forward force.
        }

        // Move backward: Responds to DownArrow input or autonomous backward action.
        if(Input.GetKey(KeyCode.DownArrow) || (CurrentDirection == Direction.MoveBackward && IsAutonomous))
        {
            ApplyAnimatorState(Direction.MoveBackward); // Update animations.
            carRigidBody.AddForce(-transform.forward * speed, ForceMode.VelocityChange); // Apply backward force.
        }

        // Turn left: Responds to LeftArrow input with torque conditions or autonomous left turn.
        if((Input.GetKey(KeyCode.LeftArrow) && canApplyTorque()) || (CurrentDirection == Direction.TurnLeft && IsAutonomous))
        {
            ApplyAnimatorState(Direction.TurnLeft); // Update animations.
            carRigidBody.AddTorque(transform.up * -torque); // Apply torque to rotate left.
        }

        // Turn right: Responds to RightArrow input with torque conditions or autonomous right turn.
        if(Input.GetKey(KeyCode.RightArrow) && canApplyTorque() || (CurrentDirection == Direction.TurnRight && IsAutonomous))
        {
            ApplyAnimatorState(Direction.TurnRight); // Update animations.
            carRigidBody.AddTorque(transform.up * torque); // Apply torque to rotate right.
        }
    }

    void ApplyAnimatorState(Direction direction)
    {   
        carAnimator.SetBool(direction.ToString(), true); // Set the current direction animation state to true.

        switch(direction)
        {
            case Direction.Idle:
                carAnimator.SetBool(Direction.MoveBackward.ToString(), false);
                carAnimator.SetBool(Direction.MoveForward.ToString(), false);
                carAnimator.SetBool(Direction.TurnLeft.ToString(), false);
                carAnimator.SetBool(Direction.TurnRight.ToString(), false);
                break;
            case Direction.MoveForward:
                carAnimator.SetBool(Direction.Idle.ToString(), false);
                carAnimator.SetBool(Direction.MoveBackward.ToString(), false);
                carAnimator.SetBool(Direction.TurnLeft.ToString(), false);
                carAnimator.SetBool(Direction.TurnRight.ToString(), false);
                break;
            case Direction.MoveBackward:
                carAnimator.SetBool(Direction.Idle.ToString(), false);
                carAnimator.SetBool(Direction.MoveForward.ToString(), false);
                carAnimator.SetBool(Direction.TurnLeft.ToString(), false);
                carAnimator.SetBool(Direction.TurnRight.ToString(), false);
                break;
            case Direction.TurnLeft:
                carAnimator.SetBool(Direction.TurnRight.ToString(), false); // Ensure opposite direction animations are off.
                break;
            case Direction.TurnRight:
                carAnimator.SetBool(Direction.TurnLeft.ToString(), false); // Ensure opposite direction animations are off.
                break;
        }
    }

    public bool canApplyTorque()
    {
        // Ensure the car's speed is sufficient to allow turning.
        Vector3 velocity = carRigidBody.velocity;
        return Mathf.Abs(velocity.x) >= minSpeedBeforeTorque || Mathf.Abs(velocity.z) >= minSpeedBeforeTorque;
    }
}
