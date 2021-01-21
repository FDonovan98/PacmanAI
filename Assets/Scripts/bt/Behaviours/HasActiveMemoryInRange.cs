using UnityEngine;

public class HasActiveMemoryInRange : BtNode
{
    private MemoryType m_memType;
    private float m_maxRange;

    public HasActiveMemoryInRange(MemoryType memType, float maxRange)
    {
        m_memType = memType;
        m_maxRange = maxRange;
    }

    public override NodeState Evaluate(Blackboard blackboard)
    {
        foreach (AIRememberedItem item in blackboard.rememberedItems[(int)m_memType])
        {
            if(item.activeMemory)
            {
                float dist = Vector3.Distance(blackboard.owner.transform.position, item.position);

                if(dist < m_maxRange)
                {
                    return NodeState.SUCCESS;
                }
            }
        }

        return NodeState.FAILURE;
    }

    public override string getName()
    {
        return "HasActiveMemoryInRange";
    }
}