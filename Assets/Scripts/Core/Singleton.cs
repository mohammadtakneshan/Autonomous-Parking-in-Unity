using UnityEngine;

// Namespace for organizing the utility classes.
namespace DilmerGames.Core
{
    // Generic singleton pattern implementation for Unity components.
    public class Singleton<T> : MonoBehaviour
        where T : Component // Constraint: T must be a Unity Component.
    {
        // Static instance of the singleton, shared across all instances of this class.
        private static T _instance;

        // Public property to access the singleton instance.
        public static T Instance
        {
            get
            {
                // If the instance has not been initialized yet.
                if (_instance == null)
                {
                    // Find all objects of type T in the scene.
                    var objs = FindObjectsOfType(typeof(T)) as T[];

                    // If at least one instance exists, assign the first one to _instance.
                    if (objs.Length > 0)
                        _instance = objs[0];

                    // Log an error if more than one instance of the singleton is found in the scene.
                    if (objs.Length > 1)
                    {
                        Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");
                    }

                    // If no instance exists, create a new one.
                    if (_instance == null)
                    {
                        // Create a new GameObject to hold the singleton.
                        GameObject obj = new GameObject();

                        // Name the GameObject based on the type of T.
                        obj.name = string.Format("_{0}", typeof(T).Name);

                        // Add the T component to the GameObject and assign it to _instance.
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance; // Return the singleton instance.
            }
        }
    }
}
