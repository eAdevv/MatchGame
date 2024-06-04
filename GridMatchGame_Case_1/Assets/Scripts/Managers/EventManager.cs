using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class EventManager 
{
    public static Action<int> OnGridCreate;

    #region Object Pool Events
    public static Action<string, GameObject, int> OnPoolSpawn;
    public static Action OnPoolClear;
    public delegate GameObject GameObjectEventHandler(string text, Vector3 transform, Quaternion Quaternion);
    public static event GameObjectEventHandler OnGetPoolObject;

    public static GameObject InvokeOnGetPoolObject(string text, Vector3 transform, Quaternion Quaternion)
    {
        return OnGetPoolObject?.Invoke(text, transform, Quaternion);
    }
    #endregion


}
