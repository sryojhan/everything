using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public class DialogueNodeData : DialogueNode
    {
        [TextArea]
        public string message;
    }
}
