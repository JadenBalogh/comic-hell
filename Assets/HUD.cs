using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private static HUD instance;

    public static XPComponent XPComponent { get => instance.xpComponent; }
    [SerializeField] private XPComponent xpComponent;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
}
