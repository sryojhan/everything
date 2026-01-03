using System;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public class DialogueLink
    {
        public string description;
        [HideInInspector]
        public string from;
        [HideInInspector]
        public string to;
    }
}
