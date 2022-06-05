using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private MapRoom[] _corridors;
    [SerializeField] private MapRoom[] _endRooms;
    [SerializeField] private Vector2Int _roomSize;

    private MapRoom[,] _generatedMap;

    private WaitForSeconds _roomGenerationWait;

    private void Awake()
    {
        _roomGenerationWait = new WaitForSeconds(Time.fixedDeltaTime);
    }

    private IEnumerator GenerateMap(int seed)
    {
        Random.InitState(seed);
        yield return null;
    }

    private IEnumerator InstantiateRoom(MapRoom roomPrefab)
    {

        yield return _roomGenerationWait;
    }
}
