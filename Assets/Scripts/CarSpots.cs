using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CarObstacle;

// Represents a cached car's position and rotation.
public class CachedCar
{
    public Vector3 Position { get; set; } // Cached position of the car.
    public Quaternion Rotation { get; set; } // Cached rotation of the car.
}

public class CarSpots : MonoBehaviour
{
    // Prefab representing the car goal (final destination or milestone).
    [SerializeField]
    private GameObject carGoalPrefab = null;

    // The number of cars to hide randomly during setup.
    [SerializeField]
    private int howManyCarsTohide = 1;

    // Collection of parked cars in the parking lot.
    private IEnumerable<CarObstacle> parkedCars;

    // Dictionary to store the initial positions and rotations of all parked cars.
    private Dictionary<int, CachedCar> cachedParkedCars = new Dictionary<int, CachedCar>();

    // The current instantiated goal object.
    private GameObject carGoal = null;

    // Reference to the current `CarGoal` script.
    public CarGoal CarGoal { get; private set; }    

    // Runs when the object is initialized.
    public void Awake()
    {
        // Retrieve all parked cars in the scene with `CarObstacleType.Car`.
        parkedCars = GetComponentsInChildren<CarObstacle>(true)
            .Where(c => c.CarObstacleTypeValue == CarObstacleType.Car);

        // Cache each parked car's position and rotation for resetting during setup.
        foreach(CarObstacle obstacle in parkedCars)
        {
            cachedParkedCars.Add(obstacle.GetInstanceID(), 
            new CachedCar 
            {
                Position = obstacle.transform.position,
                Rotation = obstacle.transform.rotation
            });
        }
    }

    // Generates a list of random indices to determine which cars to hide.
    private List<int> GetRandomNumsToHideCars(int howMany)
    {
        List<int> carsToHide = new List<int>();

        for(int i = 0; i < howMany; i++)
        {
            while(carsToHide.Count < howMany)
            {
                int carToHide = Random.Range(0, parkedCars.Count());
                if(!carsToHide.Contains(carToHide))
                {
                    carsToHide.Add(carToHide); // Ensure no duplicates.
                }
            }
        }
        return carsToHide;
    }

    // Resets the parking lot area and sets up cars and goals.
    public void Setup()
    {
        // Get a list of cars to hide.
        List<int> carsToHide = GetRandomNumsToHideCars(howManyCarsTohide);
        int carCounter = 0;

        foreach (var car in parkedCars)
        {
            // Restore the car to its cached position and rotation.
            var cachedParkedCar = cachedParkedCars[car.GetInstanceID()];
            car.GetComponent<Rigidbody>().velocity = Vector3.zero;
            car.transform.SetPositionAndRotation(cachedParkedCar.Position, cachedParkedCar.Rotation);

            // Hide cars based on the random indices.
            if(carsToHide.Contains(carCounter))
            {
                car.gameObject.SetActive(false);

                // Destroy any existing goal and instantiate a new one at the car's position.
                if(carGoal != null)
                {
                    Destroy(carGoal);
                }

                carGoal = Instantiate(carGoalPrefab, Vector3.zero, Quaternion.identity);
                CarGoal = carGoal.GetComponent<CarGoal>();
                CarGoal.HasCarUsedIt = false;
                carGoal.transform.parent = transform.parent;
                carGoal.transform.position = car.gameObject.transform.position;
            }    
            else 
            {
                car.gameObject.SetActive(true); // Show other cars.
            }

            carCounter++;
        }
    }
}
