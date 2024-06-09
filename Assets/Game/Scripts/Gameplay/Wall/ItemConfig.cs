using Modules.Extensions;
using UnityEngine;

namespace Game.Scripts
{
    [CreateAssetMenu(menuName = "Create ItemConfig", fileName = "ItemConfig", order = 0)]
    public class ItemConfig : ScriptableObject
    {
        [SerializeField] private Item[] _items;

        public Item GetRandom()
        {
            return _items.GetRandom();
        }
    }
}