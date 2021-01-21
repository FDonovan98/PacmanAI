using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TowardsTarget : BtNode
{
    private NavMeshAgent m_agent;
    float m_minApproachRange;

    public TowardsTarget(float approachRange)
    {
        m_minApproachRange = approachRange;
    }

    public override NodeState Evaluate(Blackboard blackboard)
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

        if (Vector3.Distance(blackboard.owner.transform.position, blackboard.target.position) > m_minApproachRange)
        {
            Debug.Log(Vector3.Distance(blackboard.owner.transform.position, blackboard.target.position));
            Debug.Log(m_minApproachRange);
            return NodeState.RUNNING;
        }
        Debug.Log("WORDS: " + Vector3.Distance(blackboard.owner.transform.position, blackboard.target.position));
        Debug.Log("ToTa");
        for (int i = 0; i < blackboard.rememberedItems[(int)blackboard.target.memoryType].Length ; i++)
        {
            if (blackboard.target == blackboard.rememberedItems[(int)blackboard.target.memoryType][i])
            {
                blackboard.rememberedItems[(int)blackboard.target.memoryType][i].activeMemory = false;
                break;
            }
        }

        return NodeState.SUCCESS;
    }

    public override string getName()
    {
        return "TowardsTarget";
    }

}