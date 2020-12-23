using UnityEngine;

[RequireComponent(typeof(Transform))]
public class AIRememberedItem : MonoBehaviour
{
    float timeUpdated;
    MemoryType memoryType;
    Blackboard blackboard;
    Vector3 pos;

    public void Initialise(Blackboard blackboard, MemoryType memoryType, Vector3 pos)
    {
        this.memoryType = memoryType;
        this.blackboard = blackboard;
        UpdateLocation(pos);
    }

    public void UpdateLocation(Vector3 pos)
    {
        this.pos = pos;
        // Uses Vector3.Distance as bool checks are inaccurate w/ v. large values.
        if (Vector3.Distance(pos, Vector3.positiveInfinity) < 1)
        {
            this.transform.position = pos;
            timeUpdated = Time.timeSinceLevelLoad;
        }
    }
}

public enum MemoryType
{
    Pill,
    Player
}