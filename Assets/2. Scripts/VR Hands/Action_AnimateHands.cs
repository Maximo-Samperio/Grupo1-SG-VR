using UnityEngine;
using UnityEngine.InputSystem;

public class Action_AnimateHands : MonoBehaviour
{
    #region Variables to use: 

    public InputActionProperty pinchAnimAction;
    public InputActionProperty gripAnimAction;

    [Header("Animation Parameters")]
    [SerializeField] private Animator handAnimator;

    #endregion

    // Start is called before the first frame update
    //private void Start()
    //{
        // Not used. Will be out until we need it. 
    //}

    // Update is called once per frame

    private void Update()
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
