using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NeedCustomUpdateObject : MonoBehaviour, IUpdateManager
{
    private void OnEnable()
    {
        CustomUpdateManager.RegisterUpdate(this);
    }

    private void OnDisable()
    {
        CustomUpdateManager.UnregisterUpdate(this);
    }

    public abstract void CustomUpdate();
}
