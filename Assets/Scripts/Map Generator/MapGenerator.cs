using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private MapRoom[] _corridors;
    [SerializeField] private MapRoom[] _mainRooms;
    [SerializeField] private MapRoom[] _endRooms;


   private IEnumerator GenerateMap(int seed)
   {
        Random.InitState(seed);
        yield return null;
   }
}
