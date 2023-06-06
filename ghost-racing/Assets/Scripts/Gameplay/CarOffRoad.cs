using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class CarOffRoad : MonoBehaviour
{
    [Tooltip("Layer mask of the object the car can collision.")]
    public LayerMask layers;
    [Tooltip("Cap speed when car is off road.")]
    public float capSpeed = 45f;
    [Tooltip("Cap reverse speed when car is off road.")]
    public float capReverseSpeed = 100f;
    [Tooltip("Speed default value when in not off road.")]
    public float carMaxSpeed = 200f;
    [Tooltip("Reverse speed default value when in not off road.")]
    public float carReverseMaxSpeed = 500f;

    private CarController car;                      // Reference to the Car controller scripts so we can modify its parameters

    private void Awake()
    {
        car = GetComponent<CarController>();
    }

    private void Update()
    {
        // Two layers are defined: Road and OffRoad
        int roadLayer_mask = LayerMask.NameToLayer("Road");
        int offRoadLayer_mask = LayerMask.NameToLayer("OffRoad");

        // A ray detects if the car is doing collision with the road or with the terrain
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.down));
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, 10, layers))
        {
            // If the collision is detected (and only detects the terrain) the speed is decreased 
            if (!(hitData.collider.gameObject.layer == roadLayer_mask))
            {
                Debug.Log("Off Road");
                car.SetTopSpeed(capSpeed);
                car.SetReverseSpeed(capReverseSpeed);

            }
            else
            {
                // Otherwise keep with the same speed
                car.SetTopSpeed(carMaxSpeed);
                car.SetReverseSpeed(carReverseMaxSpeed);
            }
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
        }
    }
}
