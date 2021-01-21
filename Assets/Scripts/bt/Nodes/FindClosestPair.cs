using UnityEngine;

public class FindClosestPair : BtNode
{
    MemoryType m_sourceMemoryType;
    MemoryType m_targetMemoryType;
    bool m_requireActiveMemory;

    public FindClosestPair(MemoryType sourceMemoryType, MemoryType targetMemoryType, bool requireActiveMemory = true)
    {
        m_sourceMemoryType = sourceMemoryType;
        m_targetMemoryType = targetMemoryType;
        m_requireActiveMemory = requireActiveMemory;
    }

    public override NodeState Evaluate(Blackboard blackboard)
    {
        AIRememberedItem newTarget = null;
        float dist = float.MaxValue;
        float newDist;

        // Loop through each remembered memory type.
        // Skip over any memories that aren't from active objects.
        foreach (AIRememberedItem source in blackboard.rememberedItems[(int)m_sourceMemoryType])
        {
            if (!(m_requireActiveMemory && !source.activeMemory))
            {
                foreach (AIRememberedItem target in blackboard.rememberedItems[(int)m_targetMemoryType])
                {
                    if (target.activeMemory)
                    {
                        if (source != target)
                        {
                            newDist = Vector3.Distance(source.position, target.position);
                            if (newDist < dist)
                            {
                                dist = newDist;
                                newTarget = target;
                            }
                        }
                    }
                }

            }
        }

        if (newTarget == null)
        {
            return NodeState.FAILURE;
        }

        blackboard.target = newTarget;
        return NodeState.SUCCESS;
    }

    public override string getName()
    {
        return "FindClosestPair";
    }
}