using UnityEngine;
using UnityEngine.XR;

public class WristMenuController : MonoBehaviour //NeedCustomUpdateObject, IUpdateManager
{
    [SerializeField] private GameObject wristMenu;

    private bool menuButtonPressed;

    private void Update()
    {
        if (InputData.Instance._rightController.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonPressed) && menuButtonPressed)
            wristMenu.SetActive(true);
        else
            wristMenu.SetActive(false);
    }

    // Update is called once per frame
    /*public override void CustomUpdate()
    {
        if (InputData.Instance._rightController.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonPressed) && menuButtonPressed)
            wristMenu.SetActive(true);
        else
            wristMenu.SetActive(false);
    }*/
}
