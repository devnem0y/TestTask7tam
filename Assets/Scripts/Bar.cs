using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] private List<GameObject> _cellPrefabs;
    [SerializeField] private Transform _wrapper;

    private void Start()
    {
        for (var i = 0; i < 9; i++)
        {
            var rndCell = _cellPrefabs[Random.Range(0, _cellPrefabs.Count - 1)];
            
            Instantiate(rndCell, _wrapper);
            _cellPrefabs.Remove(rndCell);
        }
    }
}
