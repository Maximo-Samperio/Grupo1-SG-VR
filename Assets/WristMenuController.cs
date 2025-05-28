using UnityEngine;
using UnityEngine.XR;

public class WristMenuController : MonoBehaviour
{
    [SerializeField] private GameObject wristMenu;

    private bool menuButtonPressed;

    // Update is called once per frame
    void Update()
    {
        if (InputData.Instance._rightController.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonPressed) && menuButtonPressed)
            wristMenu.SetActive(true);
        else
            wristMenu.SetActive(false);
    }
}
