using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Environment
{
    private static Environment instance;

    [SerializeField]
    private List<GameObject> targets = new List<GameObject>();

    public static Environment Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Environment();
                instance.targets.AddRange(GameObject.FindGameObjectsWithTag("Target"));
            }

            return instance;
        }
    }

    public Vector3 GetRandomTarget()
    {
        return targets[Random.Range(0, targets.Count - 1)].transform.position;
    }

    public List<Vector3> GetTargets()
    {
        List<Vector3> targetsToReturn = new List<Vector3>();

        foreach (GameObject target in targets)
            targetsToReturn.Add(target.transform.position);

        return targetsToReturn;
    }
}
