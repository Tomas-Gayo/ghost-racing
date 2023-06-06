using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class ReplayRaceManager : MonoBehaviour
{
    [Tooltip("Reference to the camera that follows the car.")]
    public CinemachineVirtualCamera carVCam;
    [Tooltip("Reference to the cameras that are displayed around the circuit.")]
    public CinemachineClearShot replayCam;
    [Tooltip("Reference to the car prefab is going to run on the reply race.")]
    public GameObject carPrefab;
    [Tooltip("Reference to the camera that follows the car.")]
    public GameObject carCameraObject;
    [Tooltip("Reference to the cameras that are displayed around the circuit.")]
    public GameObject replayCamObject;

    private GameObject instance;                         // Game object of the car instanciated

    private void Start()
    {
        instance = Instantiate(carPrefab);
        replayCam.LookAt = instance.transform;
        carVCam.LookAt = instance.transform; 
        carVCam.Follow = instance.transform;
    }

    private void Update()
    {
        // Click space bar to blend between both camera modes
        if (Input.GetKeyDown(KeyCode.Space))
        {
            carCameraObject.SetActive(!carCameraObject.activeSelf);
            replayCamObject.SetActive(!replayCamObject.activeSelf);
        }

        if (instance.activeSelf == false)
        {
            SceneManager.LoadScene(0);
        }
    }
}
