// Animancer // Copyright 2020 Kybernetik //

#pragma warning disable CS0618 // Type or member is obsolete (for ControllerStates in Animancer Lite).

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Animancer
{
    /// <summary>[Pro-Only]
    /// A <see cref="NamedAnimancerComponent"/> which plays a main <see cref="RuntimeAnimatorController"/> with the
    /// ability to play other individual <see cref="AnimationClip"/>s separately.
    /// </summary>
    [AddComponentMenu(Strings.MenuPrefix + "Hybrid Animancer Component")]
    [HelpURL(Strings.APIDocumentationURL + "/HybridAnimancerComponent")]
    public class HybridAnimancerComponent : NamedAnimancerComponent
    {
        /************************************************************************************************************************/
        #region Fields and Properties
        /************************************************************************************************************************/

        [SerializeField, Tooltip("The main Animator Controller that this object will play")]
        private ControllerState.Transition _Controller;

        /// <summary>[<see cref="SerializeField"/>]
        /// The main <see cref="RuntimeAnimatorController"/> that this object plays.
        /// </summary>
        public ControllerState.Transition Controller
        {
            get { return _Controller; }
            set { _Controller = value; }
        }

        /************************************************************************************************************************/
        #endregion
        /************************************************************************************************************************/
        #region Initialisation
        /************************************************************************************************************************/

#if UNITY_EDITOR
        /// <summary>[Editor-Only]
        /// Called by the Unity Editor when this component is first added (in Edit Mode) and whenever the Reset command
        /// is executed from its context menu.
        /// <para></para>
        /// Sets <see cref="PlayAutomatically"/> = false by default so that <see cref="OnEnable"/> will play the
        /// <see cref="Controller"/> instead of the first animation in the
        /// <see cref="NamedAnimancerComponent.Animations"/> array.
        /// </summary>
        protected override void Reset()
        {
            // The base.Animator property is not assigned yet and we need it before base.Reset clears the controller.
            var animator = Editor.AnimancerEditorUtilities.GetComponentInHierarchy<Animator>(gameObject);
            if (animator != null && animator.runtimeAnimatorController != null)
                Controller = animator.runtimeAnimatorController;

            base.Reset();

            PlayAutomatically = false;
        }
#endif

        /************************************************************************************************************************/

        /// <summary>
        /// Called by Unity when this component becomes enabled and active.
        /// <para></para>
        /// Plays the <see cref="Controller"/> if <see cref="PlayAutomatically"/> is false (otherwise it plays the
        /// first animation in the <see cref="NamedAnimancerComponent.Animations"/> array).
        /// </summary>
        protected override void OnEnable()
        {
            PlayController();
            base.OnEnable();
        }

        /************************************************************************************************************************/

        /// <summary>[<see cref="IAnimationClipSource"/>]
        /// Gathers all the animations in the <see cref="Playable"/> and the <see cref="Animations"/> array.
        /// </summary>
        public override void GatherAnimationClips(ICollection<AnimationClip> clips)
        {
            base.GatherAnimationClips(clips);

            if (_Controller != null &&
                _Controller.Controller != null)
                clips.Gather(_Controller.Controller.animationClips);
        }

        /************************************************************************************************************************/
        #endregion
        /************************************************************************************************************************/
        #region Animator Controller Wrappers
        /************************************************************************************************************************/

        /// <summary>
        /// Transitions to the <see cref="Controller"/> over its specified
        /// <see cref="AnimancerState.Transition{TState}.FadeDuration"/>.
        /// <para></para>
        /// Returns the <see cref="AnimancerState.Transition{TState}.State"/>.
        /// </summary>
        public ControllerState PlayController()
        {
            if (_Controller == null || _Controller.Controller == null)
                return null;

            // Don't just return the result of Transition because it is an AnimancerState which we would need to cast.
            Play(_Controller);
            return _Controller.State;
        }

        /************************************************************************************************************************/
        #region Cross Fade
        /************************************************************************************************************************/

        /// <summary>
        /// Starts a transition from the current state to the specified state using normalized times.
        /// </summary>
        public void CrossFade(int stateNameHash,
            float transitionDuration = AnimancerPlayable.DefaultFadeDuration,
            int layer = -1,
            float normalizedTime = float.NegativeInfinity)
        {
            var controllerState = PlayController();
            controllerState.Playable.CrossFade(stateNameHash, transitionDuration, layer, normalizedTime);
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Starts a transition from the current state to the specified state using normalized times.
        /// </summary>
        public AnimancerState CrossFade(string stateName,
            float transitionDuration = AnimancerPlayable.DefaultFadeDuration,
            int layer = -1,
            float normalizedTime = float.NegativeInfinity)
        {
            AnimancerState state;
            if (States.TryGet(name, out state))
            {
                Play(state, transitionDuration);

                if (layer >= 0)
                    state.LayerIndex = layer;

                state.NormalizedTime = normalizedTime;

                return state;
            }
            else
            {
                var controllerState = PlayController();
                controllerState.Playable.CrossFade(stateName, transitionDuration, layer, normalizedTime);
                return controllerState;
            }
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Starts a transition from the current state to the specified state using times in seconds.
        /// </summary>
        public void CrossFadeInFixedTime(int stateNameHash,
            float transitionDuration = AnimancerPlayable.DefaultFadeDuration,
            int layer = -1,
            float fixedTime = 0)
        {
            var controllerState = PlayController();
            controllerState.Playable.CrossFadeInFixedTime(stateNameHash, transitionDuration, layer, fixedTime);
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Starts a transition from the current state to the specified state using times in seconds.
        /// </summary>
        public AnimancerState CrossFadeInFixedTime(string stateName,
            float transitionDuration = AnimancerPlayable.DefaultFadeDuration,
            int layer = -1,
            float fixedTime = 0)
        {
            AnimancerState state;
            if (States.TryGet(name, out state))
            {
                Play(state, transitionDuration);

                if (layer >= 0)
                    state.LayerIndex = layer;

                state.Time = fixedTime;

                return state;
            }
            else
            {
                var controllerState = PlayController();
                controllerState.Playable.CrossFadeInFixedTime(stateName, transitionDuration, layer, fixedTime);
                return controllerState;
            }
        }

        /************************************************************************************************************************/
        #endregion
        /************************************************************************************************************************/
        #region Play
        /************************************************************************************************************************/

        /// <summary>
        /// Plays the specified state immediately, starting from a particular normalized time.
        /// </summary>
        public void Play(int stateNameHash,
            int layer = -1,
            float normalizedTime = float.NegativeInfinity)
        {
            var controllerState = PlayController();
            controllerState.Playable.Play(stateNameHash, layer, normalizedTime);
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Plays the specified state immediately, starting from a particular normalized time.
        /// </summary>
        public AnimancerState Play(string stateName,
            int layer = -1,
            float normalizedTime = float.NegativeInfinity)
        {
            AnimancerState state;
            if (States.TryGet(name, out state))
            {
                Play(state);

                if (layer >= 0)
                    state.LayerIndex = layer;

                state.NormalizedTime = normalizedTime;

                return state;
            }
            else
            {
                var controllerState = PlayController();
                controllerState.Playable.Play(stateName, layer, normalizedTime);
                return controllerState;
            }
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Plays the specified state immediately, starting from a particular time (in seconds).
        /// </summary>
        public void PlayInFixedTime(int stateNameHash,
            int layer = -1,
            float fixedTime = 0)
        {
            var controllerState = PlayController();
            controllerState.Playable.PlayInFixedTime(stateNameHash, layer, fixedTime);
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Plays the specified state immediately, starting from a particular time (in seconds).
        /// </summary>
        public AnimancerState PlayInFixedTime(string stateName,
            int layer = -1,
            float fixedTime = 0)
        {
            AnimancerState state;
            if (States.TryGet(name, out state))
            {
                Play(state);

                if (layer >= 0)
                    state.LayerIndex = layer;

                state.Time = fixedTime;

                return state;
            }
            else
            {
                var controllerState = PlayController();
                controllerState.Playable.PlayInFixedTime(stateName, layer, fixedTime);
                return controllerState;
            }
        }

        /************************************************************************************************************************/
        #endregion
        /************************************************************************************************************************/
        #region Parameters
        /************************************************************************************************************************/

        /// <summary>Gets the value of the specified boolean parameter.</summary>
        public bool GetBool(int id) { return _Controller.State.Playable.GetBool(id); }
        /// <summary>Gets the value of the specified boolean parameter.</summary>
        public bool GetBool(string name) { return _Controller.State.Playable.GetBool(name); }
        /// <summary>Sets the value of the specified boolean parameter.</summary>
        public void SetBool(int id, bool value) { _Controller.State.Playable.SetBool(id, value); }
        /// <summary>Sets the value of the specified boolean parameter.</summary>
        public void SetBool(string name, bool value) { _Controller.State.Playable.SetBool(name, value); }

        /// <summary>Gets the value of the specified float parameter.</summary>
        public float GetFloat(int id) { return _Controller.State.Playable.GetFloat(id); }
        /// <summary>Gets the value of the specified float parameter.</summary>
        public float GetFloat(string name) { return _Controller.State.Playable.GetFloat(name); }
        /// <summary>Sets the value of the specified float parameter.</summary>
        public void SetFloat(int id, float value) { _Controller.State.Playable.SetFloat(id, value); }
        /// <summary>Sets the value of the specified float parameter.</summary>
        public void SetFloat(string name, float value) { _Controller.State.Playable.SetFloat(name, value); }

        /// <summary>Gets the value of the specified integer parameter.</summary>
        public int GetInteger(int id) { return _Controller.State.Playable.GetInteger(id); }
        /// <summary>Gets the value of the specified integer parameter.</summary>
        public int GetInteger(string name) { return _Controller.State.Playable.GetInteger(name); }
        /// <summary>Sets the value of the specified integer parameter.</summary>
        public void SetInteger(int id, int value) { _Controller.State.Playable.SetInteger(id, value); }
        /// <summary>Sets the value of the specified integer parameter.</summary>
        public void SetInteger(string name, int value) { _Controller.State.Playable.SetInteger(name, value); }

        /// <summary>Sets the specified trigger parameter to true.</summary>
        public void SetTrigger(int id) { _Controller.State.Playable.SetTrigger(id); }
        /// <summary>Sets the specified trigger parameter to true.</summary>
        public void SetTrigger(string name) { _Controller.State.Playable.SetTrigger(name); }
        /// <summary>Resets the specified trigger parameter to false.</summary>
        public void ResetTrigger(int id) { _Controller.State.Playable.ResetTrigger(id); }
        /// <summary>Resets the specified trigger parameter to false.</summary>
        public void ResetTrigger(string name) { _Controller.State.Playable.ResetTrigger(name); }

        /// <summary>Gets the details of one of the <see cref="Controller"/>'s parameters.</summary>
        public AnimatorControllerParameter GetParameter(int index) { return _Controller.State.Playable.GetParameter(index); }
        /// <summary>Gets the number of parameters in the <see cref="Controller"/>.</summary>
        public int GetParameterCount() { return _Controller.State.Playable.GetParameterCount(); }

        /// <summary>Indicates whether the specified parameter is controlled by an <see cref="AnimationClip"/>.</summary>
        public bool IsParameterControlledByCurve(int id) { return _Controller.State.Playable.IsParameterControlledByCurve(id); }
        /// <summary>Indicates whether the specified parameter is controlled by an <see cref="AnimationClip"/>.</summary>
        public bool IsParameterControlledByCurve(string name) { return _Controller.State.Playable.IsParameterControlledByCurve(name); }

        /************************************************************************************************************************/
        #endregion
        /************************************************************************************************************************/
        #region Misc
        /************************************************************************************************************************/
        // Layers.
        /************************************************************************************************************************/

        /// <summary>Gets the weight of the layer at the specified index.</summary>
        public float GetLayerWeight(int layerIndex) { return _Controller.State.Playable.GetLayerWeight(layerIndex); }
        /// <summary>Sets the weight of the layer at the specified index.</summary>
        public void SetLayerWeight(int layerIndex, float weight) { _Controller.State.Playable.SetLayerWeight(layerIndex, weight); }

        /// <summary>Gets the number of layers in the <see cref="Controller"/>.</summary>
        public int GetLayerCount() { return _Controller.State.Playable.GetLayerCount(); }

        /// <summary>Gets the index of the layer with the specified name.</summary>
        public int GetLayerIndex(string layerName) { return _Controller.State.Playable.GetLayerIndex(layerName); }
        /// <summary>Gets the name of the layer with the specified index.</summary>
        public string GetLayerName(int layerIndex) { return _Controller.State.Playable.GetLayerName(layerIndex); }

        /************************************************************************************************************************/
        // States.
        /************************************************************************************************************************/

        /// <summary>Returns information about the current state.</summary>
        public AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex = 0) { return _Controller.State.Playable.GetCurrentAnimatorStateInfo(layerIndex); }
        /// <summary>Returns information about the next state being transitioned towards.</summary>
        public AnimatorStateInfo GetNextAnimatorStateInfo(int layerIndex = 0) { return _Controller.State.Playable.GetNextAnimatorStateInfo(layerIndex); }

        /// <summary>Indicates whether the specified layer contains the specified state.</summary>
        public bool HasState(int layerIndex, int stateID) { return _Controller.State.Playable.HasState(layerIndex, stateID); }

        /************************************************************************************************************************/
        // Transitions.
        /************************************************************************************************************************/

        /// <summary>Indicates whether the specified layer is currently executing a transition.</summary>
        public bool IsInTransition(int layerIndex = 0) { return _Controller.State.Playable.IsInTransition(layerIndex); }

        /// <summary>Gets information about the current transition.</summary>
        public AnimatorTransitionInfo GetAnimatorTransitionInfo(int layerIndex = 0) { return _Controller.State.Playable.GetAnimatorTransitionInfo(layerIndex); }

        /************************************************************************************************************************/
        // Clips.
        /************************************************************************************************************************/

        /// <summary>Gets information about the <see cref="AnimationClip"/>s currently being played.</summary>
        public AnimatorClipInfo[] GetCurrentAnimatorClipInfo(int layerIndex = 0) { return _Controller.State.Playable.GetCurrentAnimatorClipInfo(layerIndex); }
        /// <summary>Gets information about the <see cref="AnimationClip"/>s currently being played.</summary>
        public void GetCurrentAnimatorClipInfo(int layerIndex, List<AnimatorClipInfo> clips) { _Controller.State.Playable.GetCurrentAnimatorClipInfo(layerIndex, clips); }
        /// <summary>Gets the number of <see cref="AnimationClip"/>s currently being played.</summary>
        public int GetCurrentAnimatorClipInfoCount(int layerIndex = 0) { return _Controller.State.Playable.GetCurrentAnimatorClipInfoCount(layerIndex); }

        /// <summary>Gets information about the <see cref="AnimationClip"/>s currently being transitioned towards.</summary>
        public AnimatorClipInfo[] GetNextAnimatorClipInfo(int layerIndex = 0) { return _Controller.State.Playable.GetNextAnimatorClipInfo(layerIndex); }
        /// <summary>Gets information about the <see cref="AnimationClip"/>s currently being transitioned towards.</summary>
        public void GetNextAnimatorClipInfo(int layerIndex, List<AnimatorClipInfo> clips) { _Controller.State.Playable.GetNextAnimatorClipInfo(layerIndex, clips); }
        /// <summary>Gets the number of <see cref="AnimationClip"/>s currently being transitioned towards.</summary>
        public int GetNextAnimatorClipInfoCount(int layerIndex = 0) { return _Controller.State.Playable.GetNextAnimatorClipInfoCount(layerIndex); }

        /************************************************************************************************************************/
        #endregion
        /************************************************************************************************************************/
        #endregion
        /************************************************************************************************************************/
    }
}
