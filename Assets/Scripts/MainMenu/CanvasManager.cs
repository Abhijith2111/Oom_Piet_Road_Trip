using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [Header("Canvas References")]
    public GameObject mainMenuCanvas;
    public GameObject popupCanvas;

    // Show popup and hide menu
    public void ShowPopup()
    {
        mainMenuCanvas.SetActive(false);
        popupCanvas.SetActive(true);
    }

    // Go back to menu
    public void ShowMainMenu()
    {
        popupCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}
