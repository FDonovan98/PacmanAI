using System;
using System.Collections.Generic;
using UnityEngine;

public class GuardPowerPill : BtController
{
    protected override BtNode createTree()
    {
        // BtNode isTargetSelected = new Sequence(new IsTargeting(MemoryType.Pill), new Inverter(new IsClose(1)));
        // BtNode stickyTarget = new Selector(isTargetSelected, new TargetRandom(MemoryType.Pill));

        // BtNode wonderToPill = new Sequence(stickyTarget, new TowardsTarget(m_minApproachRange));

        // BtNode guardPower = new Sequence(new FindClosestPair(MemoryType.Player, MemoryType.PowerPill), new TowardsTarget(m_minApproachRange));

        // return new Selector(MoveToItem(MemoryType.Player, playerAttackRange), guardPower, wonderToPill);
        Debug.Log("check " + m_minApproachRange);
        BtNode GuardPill = new Sequence(new Inverter(new HasActiveMemoryInRange(MemoryType.Player, playerAttackRange)), new FindClosestPair(MemoryType.Player, MemoryType.PowerPill, false), new TowardsTarget(m_minApproachRange));

        return new Selector(MoveToPlayer(playerAttackRange), GuardPill);
    }

    protected override void InitialiseKnownItems()
    {
        m_blackboard.UpdateRememberedItems(MemoryType.PowerPill, GameObject.FindGameObjectsWithTag("powerpill"));

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPos = player.transform.position;

        player.transform.position = new Vector3(playerPos.x, 0.0f, playerPos.z);

        m_blackboard.UpdateRememberedItems(MemoryType.Player, player);

        player.transform.position = playerPos;
    }
}