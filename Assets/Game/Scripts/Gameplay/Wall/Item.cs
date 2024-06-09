using UnityEngine;

namespace Game.Scripts
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        public void ShowAnimation()
        {
            _animator.SetTrigger("Shake");
        }
    }
}