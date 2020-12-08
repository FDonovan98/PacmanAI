using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : BtController
{
    protected override BtNode createTree()
    {
        return new Sequence(new IsClose(100, "Player"), new TargetPlayer("Player"), new TowardsTarget());
    }
}