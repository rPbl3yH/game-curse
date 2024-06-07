using System;
using Animancer;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts
{
    public class CharacterAnimatorController : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private AnimancerComponent _animancerComponent;

        private void Update()
        {
            var magnitude = _character.GetVelocity().magnitude;
            if (magnitude < Mathf.Epsilon)
            {
                _animancerComponent.Play("HappyIdle", 0.2f);
            }
            else
            {
                _animancerComponent.Play("Walking");
            }
        }
    }
}