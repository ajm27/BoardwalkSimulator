using RangeStruct;
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
        Transform target =  targets[Random.Range(0, targets.Count - 1)].transform;
        Bounds bounds = target.GetComponent<Renderer>().bounds;
        RangeF xRange = new RangeF(target.position.x - (bounds.size.x / 2) , (target.position.x + bounds.size.x / 2));
        RangeF zRange = new RangeF(target.position.z - (bounds.size.z / 2) , (target.position.z + bounds.size.z / 2));

        return new Vector3(Random.Range(xRange.min, xRange.max), target.position.y, Random.Range(zRange.min, zRange.max));

    }

    public List<Vector3> GetTargets()
    {
        List<Vector3> targetsToReturn = new List<Vector3>();

        foreach (GameObject target in targets)
            targetsToReturn.Add(target.transform.position);

        return targetsToReturn;
    }
}
