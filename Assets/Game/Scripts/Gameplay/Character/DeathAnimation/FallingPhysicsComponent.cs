using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class FallingPhysicsComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _force = 100f;
        [SerializeField] private float _torque = 10f;

        public void Activate()
        {
            _rb.isKinematic = false;
        }

        public void Deactivate()
        {
            _rb.isKinematic = true;
        }
        
        public void Fall()
        {
            var randomVector = Vector3Utils.GetRandomVector3(0f, 1f);
            randomVector.y = 1f;
            _rb.AddRelativeForce(randomVector * _force);
            _rb.AddRelativeTorque(randomVector * _torque);
        }
    }
}