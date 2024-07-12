using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Level.Clips
{
    public class ClipPlace : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private LayerMask _clipLayer;
        [SerializeField] private Clip _currentClip;

        private Clip _clipIn;

        #region Properties

        public SpriteRenderer Sprite => _sprite;

        public bool IsEmpty => _currentClip == null;

        public Clip CurrentClip
        {
            get => _currentClip;
            set => _currentClip = value;
        }

        #endregion

        private void OnEnable()
        {
            Clip.OnMouseUpAction += PickUpClip;
        }

        private void OnDisable()
        {
            Clip.OnMouseUpAction -= PickUpClip;
        }

        public void SetClip(Clip clipToSet)
        {
            clipToSet.CurrentClipPlace = this;
            _currentClip = clipToSet;
        }

        private void PickUpClip(Clip comparisonClip)
        {
            if (comparisonClip == _clipIn && IsEmpty)
            {
                if (_clipIn.CurrentClipPlace != null)
                {
                    if (_clipIn.CurrentClipPlace != this)
                    {
                        _clipIn.CurrentClipPlace.ThrowClip(_clipIn);
                    }
                }

                _clipIn.IsCanDrag = false;
                _clipIn.CurrentClipPlace = this;

                _clipIn.ChangePosition(new Vector3(transform.position.x, transform.position.y,
                    _clipIn.transform.position.z));

                _currentClip = _clipIn;
            }
            else if (comparisonClip == _clipIn && !IsEmpty && _currentClip.ClipState == Clip.ClipStateEnum.Default)
            {
                _currentClip.IsCanDrag = false;
                _currentClip.CurrentClipPlace = _clipIn.CurrentClipPlace;
                _currentClip.SetSortingLayer("RoomTop");
                _currentClip.ChangePosition(new Vector3(_currentClip.CurrentClipPlace.transform.position.x,
                    _currentClip.CurrentClipPlace.transform.position.y,
                    _currentClip.transform.position.z));
                _clipIn.CurrentClipPlace.CurrentClip = _currentClip;

                _clipIn.IsCanDrag = false;
                _clipIn.CurrentClipPlace = this;
                _clipIn.SetSortingLayer("RoomTopTop");
                _clipIn.ChangePosition(new Vector3(transform.position.x, transform.position.y,
                    _clipIn.transform.position.z));
                _currentClip = _clipIn;
            }
            else if (_currentClip != null)
            {
                _currentClip.ChangePosition(new Vector3(transform.position.x, transform.position.y,
                    _currentClip.transform.position.z));
            }
        }

        private void ThrowClip(Clip comparisonClip)
        {
            if (_currentClip == comparisonClip)
            {
                _currentClip = null;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_clipLayer.Contains(other.gameObject.layer))
            {
                _clipIn = other.GetComponent<CheckerClipPlace>().Clip;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_clipLayer.Contains(other.gameObject.layer))
            {
                _clipIn = null;
            }
        }
    }
}