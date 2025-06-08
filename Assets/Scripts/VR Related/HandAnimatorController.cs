using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimatorController : MonoBehaviour
{
    [SerializeField] private InputActionProperty _inputTrigger;
    [SerializeField] private InputActionProperty _inputPinch;

    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float triggerValue = _inputTrigger.action.ReadValue<float>();
        float pinchValue = _inputPinch.action.ReadValue<float>();

        _anim.SetFloat("Trigger", triggerValue);

        _anim.SetFloat("Grip", pinchValue);
    }
}
