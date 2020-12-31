public class ShyFollowPlayer : BtController
{
    protected override BtNode createTree()
    {
        return new Selector(AwayFromPlayer(7.0f, false), MoveToPlayer(100.0f));
    }
}