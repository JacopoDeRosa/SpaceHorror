using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapRoom[] _connectors;
        [SerializeField] private MapRoom[] _endRooms;
        [SerializeField] private MapRoom[] _normalRooms;
        [SerializeField] private Vector2Int _roomsSize;

        private MapRoom[,] _generatedMap;

        private WaitForSeconds _roomGenerationInterval;



        private void Awake()
        {
            _roomGenerationInterval = new WaitForSeconds(Time.fixedDeltaTime);
        }

        private IEnumerator GenerateMap(int seed)
        {
            Random.InitState(seed);
            yield return null;
        }

        private IEnumerator InstantiateRoom(MapRoom roomPrefab, Vector2Int position)
        {

            yield return _roomGenerationInterval;
        }
    }
}
