using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Level : MonoBehaviour, ILevel
{
    [SerializeField] private ItemStorage _itemStorage;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _itemsParent;
    
    private List<ItemData> _itemsData;

    private void Start()
    {
        _itemsData = new List<ItemData>(_itemStorage.Items);
        Shuffle();

        StartCoroutine(GenerateItems());
    }

    private IEnumerator  GenerateItems()
    {
        var spawnPosition = _spawnPoint.position;
        
        for (var i = 0; i < _itemStorage.Items.Count; i++)
        {
            var firstItem = _itemsData[0];
            
            var item = firstItem.Type switch
            {
                ItemType.SQUARE => Instantiate(_itemStorage.ItemSquarePrefab, spawnPosition,
                    Quaternion.identity, _itemsParent),
                ItemType.CIRCLE => Instantiate(_itemStorage.ItemCirclePrefab, spawnPosition,
                    Quaternion.identity, _itemsParent),
                ItemType.TRIANGLE => Instantiate(_itemStorage.ItemTrianglePrefab, spawnPosition,
                    Quaternion.identity, _itemsParent),
                _ => null
            };

            if (item != null) item.Init(i.ToString(), firstItem);
            
            _itemsData.RemoveAt(0);
            
            yield return new WaitForSeconds(0.27f);
        }
    }
    
    private void Shuffle()
    {
        var rnd = new Random();

        for (var i = _itemsData.Count - 1; i >= 1; i--)
        {
            var j = rnd.Next(i + 1);
            (_itemsData[j], _itemsData[i]) = (_itemsData[i], _itemsData[j]);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}