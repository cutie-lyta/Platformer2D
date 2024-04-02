using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public static PlayerMain Instance;

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }
}
