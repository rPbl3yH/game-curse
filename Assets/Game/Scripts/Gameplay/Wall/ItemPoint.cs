using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class ItemPoint : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private bool _isEmpty;
        
        private Item _item;
        private ItemConfig _itemConfig;
        
        [Inject]
        public void Construct(ItemConfig itemConfig)
        {
            _itemConfig = itemConfig;
        }

        public void TrySpawn()
        {
            if (_isEmpty)
            {
                return;
            }
            
            if (_item == null)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            _item = Instantiate(_itemConfig.GetRandom(), _spawnPoint);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void ShowAnimation()
        {
            if (_isEmpty)
            {
                return;
            }
            
            _item.ShowAnimation();
        }
    }
}