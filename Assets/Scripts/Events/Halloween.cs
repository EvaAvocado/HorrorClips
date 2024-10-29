using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public class Halloween : MonoBehaviour
    {
        [SerializeField] private bool _isCanSpawn = true;
        [SerializeField] private List<SpriteRenderer> _bigRopes;
        [SerializeField] private List<SpriteRenderer> _leftRopes;
        [SerializeField] private List<SpriteRenderer> _rightRopes;
        [SerializeField] private List<SpriteRenderer> _frontPumpkins;
        [SerializeField] private List<SpriteRenderer> _behindPumpkins;


        public List<SpriteRenderer> GetSpriteRenderers()
        {
            List<SpriteRenderer> list = new List<SpriteRenderer>();
            
            list.AddRange(_bigRopes);
            list.AddRange(_leftRopes);
            list.AddRange(_rightRopes);
            list.AddRange(_frontPumpkins);
            list.AddRange(_behindPumpkins);

            return list;
        }
        
        public void Spawn()
        {
            if (_isCanSpawn)
            {
                SpawnRope();
                SpawnPumpkin();
            }
        }

        private void SpawnPumpkin()
        {
            var isPumpkin = Random.Range(0, 4);
            if (isPumpkin == 0) //Front
            {
                _frontPumpkins[Random.Range(0, _frontPumpkins.Count)].gameObject.SetActive(true);
            }
            else if (isPumpkin == 1) //Behind
            {
                _behindPumpkins[Random.Range(0, _behindPumpkins.Count)].gameObject.SetActive(true);
            }
        }

        private void SpawnRope()
        {
            var isRope = Random.Range(0, 3);
            if (isRope == 0) //Big
            {
                var bigNum = Random.Range(0, 2);
                if (bigNum == 0)
                {
                    _bigRopes[0].gameObject.SetActive(true);
                }
                else if (bigNum == 1)
                {
                    _bigRopes[1].gameObject.SetActive(true);
                }
            }
            else if (isRope == 1) //Two small
            {
                var leftNum = Random.Range(0, 2);
                if (leftNum == 0)
                {
                    _leftRopes[0].gameObject.SetActive(true);
                }
                else if (leftNum == 1)
                {
                    _leftRopes[1].gameObject.SetActive(true);
                }
                    
                var rightNum = Random.Range(0, 2);
                if (rightNum == 0)
                {
                    _rightRopes[0].gameObject.SetActive(true);
                }
                else if (rightNum == 1)
                {
                    _rightRopes[1].gameObject.SetActive(true);
                }
            }
        }
    }
}
