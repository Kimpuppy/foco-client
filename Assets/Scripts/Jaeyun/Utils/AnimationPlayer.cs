using System;
using Cysharp.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Jaeyun
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class AnimationPlayer : MonoBehaviour
    {
        private PlayableGraph _playableGraph;
        private SpriteRenderer _spriteRenderer;

        public AnimationClip firstClip;

        public bool isPlayAtStart = true;
        private Animator _animator;
        private AnimationClipPlayable _clipPlayable;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            if (isPlayAtStart && firstClip != null) PlayAnim(firstClip);
        }
        

        public void SetFlip(bool isFlip)
        {
            _spriteRenderer.flipX = isFlip;
        }

        public bool GetFlip()
        {
            return _spriteRenderer.flipX;
        }

        public AnimationClip GetPlayingAnimationClip()
        {
            return _clipPlayable.IsValid() ? _clipPlayable.GetAnimationClip() : null;
        }
        

        public void SetAnimationTime(double time)
        {
            if (_clipPlayable.IsValid())
            {
                _clipPlayable.SetTime(time);
                _playableGraph.Evaluate();
            }
        }

        public float GetCurrentAnimationLength()
        {
            return _clipPlayable.GetAnimationClip().length;
        }

        public double GetAnimationCurrentTime()
        {
            return _clipPlayable.GetTime();
        }
        
        public async UniTaskVoid PlayAnimWithCallback(AnimationClip clip, Action callback)
        {
            var clipPlayable = PlayAnim(clip);
            
            await UniTask.Delay(TimeSpan.FromSeconds(clipPlayable.GetAnimationClip().length));
            if (_clipPlayable.IsValid() && _clipPlayable.GetAnimationClip() == clip)
            {
                callback?.Invoke();
            }
        }

        public async UniTask PlayAndWaitAnimDone(AnimationClip clip)
        {
            var clipPlayable = PlayAnim(clip);
            await UniTask.Delay(TimeSpan.FromSeconds(clipPlayable.GetAnimationClip().length));
        }
        
        
        public AnimationClipPlayable PlayAnim(AnimationClip clip, DirectorUpdateMode timeUpdateMode = DirectorUpdateMode.GameTime)
        {
            ClearAnimPlayable();

            
            _playableGraph = PlayableGraph.Create();
            _playableGraph.SetTimeUpdateMode(timeUpdateMode);
            
            var playableOutput = AnimationPlayableOutput.Create(_playableGraph, "Animation", _animator);

            // Wrap the clip in a playable

            _clipPlayable = AnimationClipPlayable.Create(_playableGraph, clip);

            // Connect the Playable to an output

            playableOutput.SetSourcePlayable(_clipPlayable);
            
            // Plays the Graph.

            _playableGraph.Play();

            return _clipPlayable;
        }

        public void ClearAnimPlayable()
        {
            if (_playableGraph.IsValid())
            { 
                _playableGraph.Destroy();
                
            }
        }


        private void OnDisable()
        {
            ClearAnimPlayable();
        }
    }
}