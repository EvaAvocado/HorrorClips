using System;
using System.Collections.Generic;
using Data;
using Level.Clips;
using UnityEngine;

namespace Level
{
    public class LayersManager : MonoBehaviour
    {
        [SerializeField] private ClipPlace _clipPlacePrefab;
        [SerializeField] private LevelData _levelData;

        [Header("Bottom Layer")] [SerializeField]
        private int _countOfClipsBottom;

        [SerializeField] private float _spawnPointY;

        [Header("Top Layer")] [SerializeField] private int _countOfClipsTop;

        private float _height;
        private float _width;
        private float _middleOfLayers;

        private List<ClipPlace> _topClipPlaces;
        private List<ClipPlace> _bottomClipPlaces;

        private List<GameObject> _topClips = new List<GameObject>();
        private List<GameObject> _bottomClips = new List<GameObject>();

        public float SpawnPointY => _spawnPointY;
        public float Height => _height;
        public float Width => _width;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            var size = _clipPlacePrefab.Sprite.size;
            _height = size.y;
            _width = size.x;

            if (_levelData != null)
            {
                for (int i = 0; i < _levelData.clips.GridSize.x; i++)
                {
                    var clip = _levelData.clips.GetCells()[0, i];

                    _topClips.Add(clip);
                }

                _countOfClipsTop = _topClips.Count;

                for (int i = 0; i < _levelData.clips.GridSize.x; i++)
                {
                    var clip = _levelData.clips.GetCells()[1, i];
                    if (clip != null)
                    {
                        _bottomClips.Add(clip);
                    }
                }

                _countOfClipsBottom = _bottomClips.Count;

                _bottomClipPlaces = SpawnLayer(_spawnPointY, _countOfClipsBottom);
                _topClipPlaces = SpawnLayer(_spawnPointY + _height + _height / 2, _countOfClipsTop);

                SpawnClips(_bottomClipPlaces, false);
                SpawnClips(_topClipPlaces, true);
            }
        }

        private List<ClipPlace> SpawnLayer(float spawnPosY, int countOfClips)
        {
            var spawnPoint = -_width * countOfClips / 2 + _width / 2;
            var clipPlaces = new List<ClipPlace>();

            for (int i = 0; i < countOfClips; i++)
            {
                clipPlaces.Add(Instantiate(_clipPlacePrefab,
                    new Vector3(spawnPoint, spawnPosY, _clipPlacePrefab.transform.position.z), Quaternion.identity));
                spawnPoint += _width;
            }

            return clipPlaces;
        }

        private void SpawnClips(List<ClipPlace> clipPlaces, bool isTop)
        {
            for (int i = 0; i < clipPlaces.Count; i++)
            {
                var clip = isTop ? _levelData.clips.GetCells()[0, i] : _bottomClips[i];
                if (clip != null)
                {
                    clipPlaces[i].SetClip(Instantiate(clip,
                            new Vector3(clipPlaces[i].transform.position.x, clipPlaces[i].transform.position.y,
                                clip.transform.position.z), Quaternion.identity)
                        .GetComponent<Clip>());
                }
            }

            if (!isTop)
            {
                clipPlaces[0].CurrentClip.ClipState = Clip.ClipStateEnum.Enter;
                clipPlaces[^1].CurrentClip.ClipState = Clip.ClipStateEnum.Exit;
            }
        }
    }
}