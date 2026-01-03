using UnityEngine;

namespace Dialogue
{
    public abstract class DialogueNode : ScriptableObject
    {
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;
        [HideInInspector] public bool isEntryNode;
    }
}
