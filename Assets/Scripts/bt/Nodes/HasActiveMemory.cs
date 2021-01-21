using System.Collections.Generic;

internal class HasActiveMemory : BtNode
{
    private MemoryType m_memType;

    public HasActiveMemory(MemoryType memType)
    {
        m_memType = memType;
    }

    public override NodeState Evaluate(Blackboard blackboard)
    {
        foreach (AIRememberedItem item in blackboard.rememberedItems[(int)m_memType])
        {
            if (item.activeMemory) return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }

    public override string getName()
    {
        return "Has Active Memory";
    }
}