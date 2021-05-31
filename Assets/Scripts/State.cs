using RangeStruct;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE
    {
        IDLE,
        WALKING
    }

    public enum EVENT
    {
        ENTER,
        UPDATE,
        EXIT
    }

    public MonoBehaviour mono;

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected State nextState;
    protected NavMeshAgent agent;

    public RangeF WaitRange = new RangeF(5.0f, 10.0f);
    
    public State(GameObject npc, NavMeshAgent agent)
    {
        this.npc = npc;
        this.agent = agent;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        else if (stage == EVENT.UPDATE) Update();
        else if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }

        return this;
    }
}

public class Idle : State
{
    private bool isWaiting = false;
    private int timesWaited = 0;

    public Idle(GameObject npc, NavMeshAgent agent) : base(npc, agent)
    {
        name = STATE.IDLE;
        isWaiting = true;

        IEnumerator waitingTimer = WaitFor(Random.Range(WaitRange.min, WaitRange.max));
        CoroutineRunner.Instance.StartCoroutine(waitingTimer);
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if (!isWaiting)
        {
            Debug.Log($"Times Waited: {timesWaited}");
            if (Random.Range(0, 100) < 35 || timesWaited >= 5)
            {
                Debug.Log("Character is done waiting");
                nextState = new Walking(npc, agent);
                stage = EVENT.EXIT;
            }
            else
            {
                Debug.Log("Character is waiting again");
                timesWaited++;
                isWaiting = true;
                IEnumerator waitingTimer = WaitFor(Random.Range(WaitRange.min, WaitRange.max));
                CoroutineRunner.Instance.StartCoroutine(waitingTimer);
            }
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting");
        base.Exit();
    }

    private IEnumerator WaitFor(float seconds)
    {
        //Debug.Log($"Character waiting coroutine has started for {seconds} seconds");
        yield return new WaitForSeconds(seconds);
        isWaiting = false;
    }
}

public class Walking : State
{
    Vector3 currentTarget;

    public Walking(GameObject npc, NavMeshAgent agent) : base(npc, agent)
    {
        name = STATE.WALKING;
    }

    public override void Enter()
    {
        currentTarget = Environment.Instance.GetRandomTarget();
        agent.SetDestination(currentTarget);
        base.Enter();
    }

    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            nextState = new Idle(npc, agent);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
