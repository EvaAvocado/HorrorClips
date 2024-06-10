using UnityEngine;

public class LayersManager : MonoBehaviour
{
    [SerializeField] private ClipPlace _clipPlacePrefab;

    [Header("Bottom Layer")]
    [SerializeField] private int _countOfClipsBottom;
    [SerializeField] private float _spawnPointY;
    
    [Header("Top Layer")] 
    [SerializeField] private int _countOfClipsTop;

    private float _height;
    private float _width;
    private float _middleOfLayers;

    public float SpawnPointY => _spawnPointY;
    public float Height => _height;
    public float Width => _width;

    private void Awake()
    {
        _height = _clipPlacePrefab.Sprite.size.y;
        _width = _clipPlacePrefab.Sprite.size.x;
        
        SpawnLayer(_spawnPointY, _countOfClipsBottom);
        SpawnLayer(_spawnPointY + _height + _height / 2, _countOfClipsTop);
    }

    private void SpawnLayer(float spawnPosY, int countOfClips)
    {
        var spawnPoint = -_width * countOfClips / 2 + _width / 2;

        for (int i = 0; i < countOfClips; i++)
        {
            Instantiate(_clipPlacePrefab, new Vector3(spawnPoint, spawnPosY, 0), Quaternion.identity);
            spawnPoint += _width;
        }
    }
}