using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Tooltip("Order number of the checkpoint in the circuit.")]
    public int index;

    public GameManager gameManager;                         // Reference to the car manager script.

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {           
            if (gameManager.checkpointIndex == index - 1)
            {
                gameManager.checkpointIndex = index;

                Debug.Log("Checkpoint number " + index + " Car index is " + gameManager.checkpointIndex);
            }
        }
    }
}