using UnityEngine;

public class FollowPlayer : BtController
{
    protected override BtNode createTree()
    {
        return MoveToItem(MemoryType.Player, playerAttackRange);
    }
}