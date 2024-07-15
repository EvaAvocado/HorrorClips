using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySystem.Minion
{
    public class PlayDeadSoundMinion : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private List<Minion> _diedMinions = new List<Minion>();

        private void OnEnable()
        {
            MinionAnimation.OnDieMinionPlaySound += CheckCanPlaySound;
        }

        private void OnDisable()
        {
            MinionAnimation.OnDieMinionPlaySound -= CheckCanPlaySound;
        }

        private void CheckCanPlaySound(Minion minion)
        {
            if (!_diedMinions.Contains(minion) && minion != null)
            {
                _diedMinions.Add(minion);

                if (_diedMinions.Count == 1)
                {
                    PlaySoundDieMinion();
                    StartCoroutine(TimeToResetList());
                }
            }
        }

        private void PlaySoundDieMinion()
        {
            if (_audioSource != null)
            {
                //_audioSource.clip = (AudioClip)Resources.Load("Sounds/" + "wilhelm-1-86895");
                _audioSource.PlayOneShot(_audioSource.clip);
            }
        }

        private IEnumerator TimeToResetList()
        {
            yield return new WaitForSeconds(_audioSource.clip.length);
            _diedMinions.Clear();
            _diedMinions = new List<Minion>();
        }
    }
}