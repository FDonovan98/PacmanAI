using UnityEngine;

public class FindClosestPair : BtNode
{
    GameObject[] m_sourceObjects = null;
    GameObject[] m_targetObjects = null;

    public FindClosestPair(string sourceTag, string targetTag)
    {
        GameObject[] tags = GameObject.FindGameObjectsWithTag(sourceTag);
        if (tags.Length == 0)
        {
            return;
        }
        m_sourceObjects = tags;

        tags = GameObject.FindGameObjectsWithTag(targetTag);
        if (tags.Length == 0)
        {
            return;
        }
        m_targetObjects = tags;
    }

    public override NodeState evaluate(Blackboard blackboard)
    {
        if (m_sourceObjects == null || m_targetObjects == null)
        {
            return NodeState.FAILURE;
        }

        float currDist = float.MaxValue;
        GameObject currTarget = null;
        foreach (GameObject source in m_sourceObjects)
        {
            foreach (GameObject target in m_targetObjects)
            {
                float newDist = Vector3.Distance(source.transform.position, target.transform.position);
                if (newDist < currDist)
                {
                    currDist = newDist;
                    currTarget = target;
                }
            }
        }

        blackboard.target = currTarget;
        return NodeState.SUCCESS;
    }

    public override string getName()
    {
        return "FindClosestPair";
    }
}