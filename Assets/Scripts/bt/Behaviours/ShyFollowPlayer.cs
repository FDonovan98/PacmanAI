using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShyFollowPlayer : BtController
{
    protected override BtNode createTree()
    {
        // BtNode awayFromPlayer = new Sequence(new IsClose(3, "Player"), new TargetPlayer("Player"), new AwayFromTarget());

        BtNode awayFromPlayer = new Sequence(new TargetPlayer("Player"), new IsBeingMovedTo(), new AwayFromTarget(7.0f));

        BtNode followPlayer = new Sequence(new IsClose(100, "Player"), new TargetPlayer("Player"), new TowardsTarget());

        return new Selector(awayFromPlayer, followPlayer);
    }
}