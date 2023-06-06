using UnityEngine;

public class GhostLapPlay : MonoBehaviour
{
    [Tooltip("Time will pass between samples.")]
    public float timeBetweenSamples = 0.25f;
    [Tooltip("Referece to the scriptable object where the data of the lap will be saved.")]
    public GhostLapData bestLapSO;
    [Tooltip("Reference to the ghost car prefab is going to be displayed.")]
    private Transform carToPlay;                           

    private float currentTimeBetweenPlaySamples;            // Current time so it is possible to read samples in a certain time.
    private int currentSampleToPlay;                        // Index of the sample
    private Vector3 lastSamplePosition;
    private Quaternion lastSampleRotation;
    private Vector3 nextPosition;
    private Quaternion nextRotation;

    private void Start()
    {
        carToPlay = GetComponent<Transform>();
        currentSampleToPlay = 0;
    }

    private void Update()
    {
        // A cada frame incrementamos el tiempo transcurrido 
        currentTimeBetweenPlaySamples += Time.deltaTime;

        // Si el tiempo transcurrido es mayor que el tiempo de muestreo
        if (currentTimeBetweenPlaySamples >= timeBetweenSamples)
        {
            // De cara a interpolar de una manera fluida la posición del coche entre una muestra y otra,
            // guardamos la posición y la rotación de la anterior muestra
            lastSamplePosition = nextPosition;
            lastSampleRotation = nextRotation;

            // Cogemos los datos del scriptable object
            bestLapSO.GetDataAt(currentSampleToPlay, out nextPosition, out nextRotation);

            // Dejamos el tiempo extra entre una muestra y otra
            currentTimeBetweenPlaySamples -= timeBetweenSamples;

            // Incrementamos el contador de muestras
            currentSampleToPlay++;
        }

        // To avoid taking samples outside the list we disable the object once there is no more samples for this lap
        if (currentSampleToPlay == bestLapSO.GetIndexSample())
        {
            gameObject.SetActive(false);
        }
        else
        {
            float percentageBetweenFrames = currentTimeBetweenPlaySamples / timeBetweenSamples;

            carToPlay.transform.position = Vector3.Slerp(lastSamplePosition, nextPosition, percentageBetweenFrames);
            carToPlay.transform.rotation = Quaternion.Slerp(lastSampleRotation, nextRotation, percentageBetweenFrames);
        }

       
    }
}
