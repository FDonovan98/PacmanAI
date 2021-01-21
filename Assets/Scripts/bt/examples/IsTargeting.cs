using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTargeting : BtNode
{
    private MemoryType m_memoryType;

    public IsTargeting(MemoryType memoryType)
    {
        m_memoryType = memoryType;
    }

    public override NodeState Evaluate(Blackboard blackboard)
    {
        if (blackboard.target == null)
        {
            return NodeState.FAILURE;
        }

        if (blackboard.target.memoryType == m_memoryType)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }

    public override string getName()
    {
        return "isTargeting";
    }

}