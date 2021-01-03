// Author: JohnnyA
// Collaborators:
// References: https://forum.unity.com/threads/how-to-change-the-name-of-list-elements-in-the-inspector.448910/
// File Source: Assets\Scripts\Helpers\NamedArrayAttribute.cs
// Dependencies: 
// Description: A script (see references for source) not written by me that allows naming of array elements in inspector.

using UnityEngine;

public class NamedArrayAttribute : PropertyAttribute
{
    public readonly string[] names;
    public NamedArrayAttribute(string[] names) { this.names = names; }
}
