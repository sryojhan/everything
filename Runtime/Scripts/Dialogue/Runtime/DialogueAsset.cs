using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(menuName = "Dialogue", fileName = "New Dialogue instance")]
    public class DialogueAsset : ScriptableObject
    {
        [SerializeReference]
        public List<DialogueNode> nodes = new ();

        [SerializeReference]
        public List<DialogueLink> links = new ();






    }
}
