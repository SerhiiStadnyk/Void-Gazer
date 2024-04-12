using System;
using System.Collections;
using UnityEngine;

namespace Core.Runtime
{
    public class SimpleAnimationHandler : MonoBehaviour
    {
        [SerializeField]
        private bool _playOnEnable;

        [SerializeField]
        private bool _disableOnCompletion;

        [SerializeField]
        private bool _alwaysProceed;

        [SerializeField]
        private Animator _animator;

        private Coroutine _playRoutine;

        public event Action OnWait;

        public event Action OnCompleted;

        public enum AnimType
        {
            Start,
            Wait,
            Proceed,
            Completed
        }


        private void OnEnable()
        {
            if (_playOnEnable)
            {
                Play();
            }
        }


        private void OnDisable()
        {
            if (_playRoutine != null)
            {
                StopCoroutine(_playRoutine);
            }
        }


        public void Play(AnimType animType = AnimType.Start, bool autoProceed = true)
        {
            if (_playRoutine != null)
            {
                StopCoroutine(_playRoutine);
            }

            if (_alwaysProceed)
            {
                autoProceed = true;
            }

            _playRoutine = StartCoroutine(PlayRoutine(animType, autoProceed));
        }


        private IEnumerator PlayRoutine(AnimType animType, bool autoProceed)
        {
            _animator.Play(animType.ToString(), 0);

            if (animType is AnimType.Start or AnimType.Wait)
            {
                yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName(AnimType.Wait.ToString()));
                OnWait?.Invoke();
                if (autoProceed)
                {
                    _animator.SetTrigger(AnimType.Proceed.ToString());
                }
            }

            if(autoProceed)
            {
                yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName(AnimType.Completed.ToString()));
                OnCompleted?.Invoke();

                if (_disableOnCompletion)
                {
                    gameObject.SetActive(false);
                }
            }

            _playRoutine = null;
        }
    }
}