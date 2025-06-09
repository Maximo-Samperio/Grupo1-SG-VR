using UnityEngine;
using UnityEngine.InputSystem;

public class Action_AnimateHands : NeedCustomUpdateObject
{
    #region Variables to use: 

    public InputActionProperty pinchAnimAction;
    public InputActionProperty gripAnimAction;

    [Header("Animation Parameters")]
    [SerializeField] private Animator handAnimator;

    #endregion

    public override void CustomUpdate()
    {
        ReadInputValues();
    }

    #region Methods in use: 

    private void ReadInputValues()
    {
        float triggerValue = pinchAnimAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        float gripValue = gripAnimAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }

    #endregion
}
