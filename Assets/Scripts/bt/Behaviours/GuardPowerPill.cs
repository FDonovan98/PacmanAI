using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPowerPill : BtController
{
    protected override BtNode createTree()
    {
        BtNode isTargetSelected = new Sequence(new IsTargeting("pill"), new Inverter(new IsClose(1)));
        BtNode stickyTarget = new Selector(isTargetSelected, new TargetRandom("pill"));

        BtNode wonderToPill = new Sequence(stickyTarget, new TowardsTarget());

        BtNode chasePlayer = new Sequence(new IsClose(3, "Player"), new TargetPlayer("Player"), new TowardsTarget());

        BtNode guardPower = new Sequence(new FindClosestPair("Player", "powerpill"), new TowardsTarget());
        return new Selector(chasePlayer, guardPower, wonderToPill);
    }
}