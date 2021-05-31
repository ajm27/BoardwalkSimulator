using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private List<GameObject> target;
    [SerializeField]
    private State currentState;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        currentState = new Walking(this.gameObject, agent);
    }

    void Update()
    {
        currentState = currentState.Process();
    }
}
