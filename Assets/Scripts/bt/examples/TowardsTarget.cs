using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TowardsTarget : BtNode
{
    private NavMeshAgent m_agent;

    public override NodeState evaluate(Blackboard blackboard)
    {
        if (m_agent == null)
        {
            m_agent = blackboard.owner.GetComponent<NavMeshAgent>();
        }

        // if target is null, we can't move towards it!
        if (blackboard.target == null)
        {
            return NodeState.FAILURE;
        }

        m_agent.SetDestination(blackboard.target.position);

        if (Vector3.Distance(blackboard.owner.transform.position, blackboard.target.position) > 0.5)
        {
            return NodeState.RUNNING;
        }

        return NodeState.SUCCESS;
    }

    public override string getName()
    {
        return "TowardsTarget";
    }

}