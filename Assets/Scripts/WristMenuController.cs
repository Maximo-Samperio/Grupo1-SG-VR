using UnityEngine;
using UnityEngine.XR;

public class WristMenuController : NeedCustomUpdateObject
{
    [SerializeField] private GameObject wristMenu;

    private bool menuButtonPressed;

    // Update is called once per frame
    public override void CustomUpdate()
    {
        if (InputData.Instance._rightController.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonPressed) && menuButtonPressed)
            wristMenu.SetActive(true);
        else
            wristMenu.SetActive(false);
    }
}
