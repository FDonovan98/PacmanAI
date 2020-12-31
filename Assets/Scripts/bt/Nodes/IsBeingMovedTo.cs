using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsBeingMovedTo : BtNode
{
    public override NodeState evaluate(Blackboard blackboard)
    {
        if (blackboard.target == null)
        {
            return NodeState.FAILURE;
        }

        Vector3 direction = blackboard.target.transform.position - blackboard.owner.transform.position;
        Vector2 direction2D = new Vector2(direction.x, direction.z).normalized;
        Vector2 targetView2D = new Vector2(blackboard.target.GetComponent<Rigidbody>().velocity.x, blackboard.target.GetComponent<Rigidbody>().velocity.z).normalized;
        float angle = Vector3.Dot(direction2D, targetView2D);
        angle = Mathf.Acos(angle);
        angle *= Mathf.Rad2Deg;
        Debug.Log(angle);

        if (angle <= 270 && angle >= 90)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }

    public override string getName()
    {
        return "IsBeingMovedTo";
    }
}