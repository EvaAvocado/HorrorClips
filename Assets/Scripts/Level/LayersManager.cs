using UnityEngine;

public class LayersManager : MonoBehaviour
{
    [SerializeField] private ClipPlace _clipPlacePrefab;

    [Header("Bottom Layer")]
    [SerializeField] private int _countOfClipsBottom;

    [SerializeField] private float _spawnPointY;
    [Header("Top Layer")] 
    [SerializeField] private int _countOfClipsTop;

    private void Awake()
    {
        SpawnLayer(_spawnPointY, _countOfClipsBottom);

        var height = _clipPlacePrefab.Sprite.size.y;
        SpawnLayer(_spawnPointY + height + height / 2, _countOfClipsTop);
    }

    private void SpawnLayer(float spawnPosY, int countOfClips)
    {
        var width = _clipPlacePrefab.Sprite.size.x;

        var spawnPoint = -width * countOfClips / 2 + width / 2;

        for (int i = 0; i < countOfClips; i++)
        {
            Instantiate(_clipPlacePrefab, new Vector3(spawnPoint, spawnPosY, 0), Quaternion.identity);
            spawnPoint += width;
        }
    }
}