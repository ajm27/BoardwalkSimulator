using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner instance;

    private void Awake()
    {
        instance = this;
    }

    public static CoroutineRunner Instance
    {
        get
        {
            return instance;
        }
    }
}
