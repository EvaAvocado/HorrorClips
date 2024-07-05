using EnemySystem.Minion;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public class TVItem: MonoBehaviour, IItem
    {
        [SerializeField] private int _necessaryMinionsCount;
        [SerializeField] private int _currentMinionsCount;
        [SerializeField] private Animator[] _animators;
        [SerializeField] private UnityEvent _eventIfInteract;
        [SerializeField] private AudioSource _audioSource;
        
        private bool _isCanInteract;
        
        private static readonly int IsOn = Animator.StringToHash("is-on");
        
        private void OnEnable()
        {
            Minion.OnDieMinion += AddDeadMinion;
        }
        
        private void OnDisable()
        {
            Minion.OnDieMinion -= AddDeadMinion;
        }
        
        private void AddDeadMinion(Minion minion)
        {
            _currentMinionsCount++;
            CheckCountMinions();
        }

        private void CheckCountMinions()
        {
            if (_currentMinionsCount >= _necessaryMinionsCount)
            {
                OnTv();
            }
        }

        private void OnTv()
        {
            foreach (var animator in _animators)
            {
                _isCanInteract = true;
                animator.SetBool(IsOn, true);
            }
        }

        public bool IsDropItem()
        {
            throw new System.NotImplementedException();
        }

        public ItemEnum GetItemEnum()
        {
            return ItemEnum.TV;
        }

        public Transform GetTransform()
        {
            throw new System.NotImplementedException();
        }

        public void AlternativeUse(IItem item = null)
        {
            _eventIfInteract?.Invoke();
        }

        public void Flip(float direction)
        {
            throw new System.NotImplementedException();
        }

        public void Drop()
        {
            throw new System.NotImplementedException();
        }

        public bool CheckUse(bool haveAxe)
        {
            return _isCanInteract;
        }

        public void ChangeSprite()
        {
            throw new System.NotImplementedException();
        }

        public void PlayNewAnimation(string animName)
        {
            throw new System.NotImplementedException();
        }
        
        public void PlaySound(string soundName)
        {
            _audioSource.PlayOneShot((AudioClip)Resources.Load("Sounds/" +soundName));
        }
    }
}