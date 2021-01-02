public class GuardPowerPill : BtController
{
    protected override BtNode createTree()
    {
        BtNode isTargetSelected = new Sequence(new IsTargeting(MemoryType.Pill), new Inverter(new IsClose(1)));
        BtNode stickyTarget = new Selector(isTargetSelected, new TargetRandom(MemoryType.Pill));

        BtNode wonderToPill = new Sequence(stickyTarget, new TowardsTarget());

        BtNode guardPower = new Sequence(new FindClosestPair(MemoryType.Player, MemoryType.PowerPill), new TowardsTarget());

        return new Selector(MoveToPlayer(3.0f), guardPower, wonderToPill);
    }
}