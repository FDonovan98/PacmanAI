using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsClose : BtNode
{
    private float m_distanceLimit = 10;

    // Vars for if test target is passed in.
    private GameObject[] m_possibleTargets = null;
    private GameObject m_testTarget = null;

    public IsClose(float distanceLimit)
    {
        m_distanceLimit = distanceLimit;
    }

    // If distanceFromSelf is false distance is taken from the blackboard target to the test target.
    public IsClose(float distanceLimit, string targetTag)
    {
        m_distanceLimit = distanceLimit;
        GameObject[] tags = GameObject.FindGameObjectsWithTag(targetTag);

        if (tags.Length == 0)
        {
            return;
        }

        m_possibleTargets = tags;
    }

    public override NodeState evaluate(Blackboard blackboard)
    {
        float distance;

        if (m_testTarget == null)
        {
            if (m_possibleTargets != null)
            {
                float currDist = float.MaxValue;

                foreach (GameObject element in m_possibleTargets)
                {
                    float newDist = Vector3.Distance(blackboard.owner.transform.position, element.transform.position);
                    if (newDist < currDist)
                    {
                        currDist = newDist;
                        m_testTarget = element;
                    }
                }

                distance = currDist;
            }
            else
            {
                if (blackboard.target == null)
                {
                    return NodeState.FAILURE;
                }

                distance = Vector3.Distance(blackboard.owner.transform.position, blackboard.target.transform.position);
            }
        }
        else
        {
            distance = Vector3.Distance(blackboard.owner.transform.position, m_testTarget.transform.position);
        }

        if (distance < m_distanceLimit)
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
        return "isClose";
    }

}