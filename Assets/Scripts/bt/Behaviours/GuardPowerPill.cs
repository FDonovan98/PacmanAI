public class GuardPowerPill : BtController
{
    protected override BtNode createTree()
    {
        BtNode isTargetSelected = new Sequence(new IsTargeting("pill"), new Inverter(new IsClose(1)));
        BtNode stickyTarget = new Selector(isTargetSelected, new TargetRandom("pill"));

        BtNode wonderToPill = new Sequence(stickyTarget, new TowardsTarget());

        BtNode guardPower = new Sequence(new FindClosestPair("Player", "powerpill"), new TowardsTarget());

        return new Selector(MoveToPlayer(3.0f), guardPower, wonderToPill);
    }
}