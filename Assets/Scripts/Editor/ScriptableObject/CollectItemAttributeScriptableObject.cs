using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class CollectItemAttributeScriptableObject : ScriptableObject
    {
        [SerializeField] private List<string> attributeTypeNames;

        public List<string> GetAttributeTypeNames { get { return attributeTypeNames; } }
    }

}