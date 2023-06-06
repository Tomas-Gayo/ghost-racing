using UnityEngine;

public class ToggleUISceens : MonoBehaviour
{
    public GameObject ScreenToToggle;
    public GameObject TargetScreen;
    
    public void ToggleSceen()
    {
        ScreenToToggle.SetActive(false);
        TargetScreen.SetActive(true);
    }
}
