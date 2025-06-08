using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomUpdateManager : MonoBehaviour
{

    private static List<IUpdateManager> _updateManagers = new List<IUpdateManager>();

    // Update is called once per frame
    void Update()
    {
        foreach (var manager in _updateManagers)
        {
            manager.CustomUpdate();
        }
    }

    public static void RegisterUpdate(IUpdateManager newManager)
    {
        _updateManagers.Add(newManager);
    }

    public static void UnregisterUpdate(IUpdateManager newManager)
    {
        _updateManagers.Remove(newManager);
    }
}
