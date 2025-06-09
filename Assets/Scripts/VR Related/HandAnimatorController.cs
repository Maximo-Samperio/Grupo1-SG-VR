using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimatorController : NeedCustomUpdateObject
{
    [SerializeField] private InputActionProperty _inputTrigger;
    [SerializeField] private InputActionProperty _inputPinch;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public override void CustomUpdate()
    {
        float triggerValue = _inputTrigger.action.ReadValue<float>();
        float pinchValue = _inputPinch.action.ReadValue<float>();

        _anim.SetFloat("Trigger", triggerValue);

        _anim.SetFloat("Grip", pinchValue);
    }
}
