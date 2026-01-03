using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class DialogueNodeBranch : DialogueNode
    {
        [HideInInspector] public List<string> choices = new List<string>();
    }
}
