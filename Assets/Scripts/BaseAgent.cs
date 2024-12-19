using System.Collections; // Import namespace for using coroutines.
using Unity.MLAgents; // Import ML-Agents namespace for creating agents.
using UnityEngine; // Import Unity engine namespace for Unity-specific functionality.

public class BaseAgent : Agent // Defines a base class for ML-Agents, inheriting from Unity's Agent class.
{
    [SerializeField]
    protected MeshRenderer groundMeshRenderer; 
    // Reference to the MeshRenderer of the ground object, used to change its appearance (e.g., color).

    [SerializeField]
    protected Material successMaterial; 
    // Material to be applied to the ground to indicate a successful action.

    [SerializeField]
    protected Material failureMaterial; 
    // Material to be applied to the ground to indicate a failed action.

    [SerializeField]
    protected Material defaultMaterial; 
    // The default material of the ground, restored after success/failure.

    /// <summary>
    /// Coroutine to temporarily swap the ground material.
    /// </summary>
    /// <param name="mat">The material to apply to the ground (success/failure).</param>
    /// <param name="time">Duration in seconds before restoring the default material.</param>
    /// <returns>IEnumerator for coroutine handling.</returns>
    protected IEnumerator SwapGroundMaterial(Material mat, float time)
    {
        groundMeshRenderer.material = mat; // Set the ground material to the specified material.
        yield return new WaitForSeconds(time); // Wait for the specified duration.
        groundMeshRenderer.material = defaultMaterial; // Restore the ground material to the default.
    }
}
