using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour, ILevel
{
    [SerializeField] private ItemStorage _itemStorage;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _itemsParent;
    
    private Bar _bar;
    public IBar Bar => _bar;
    
    private List<ItemData> _itemsData;

    public void Init(Action action)
    {
        _bar = new Bar();
        
        _itemsData = new List<ItemData>(_itemStorage.Items);
        MyTools.Shuffle(_itemsData);

        StartCoroutine(GenerateItems());
        
        action.Invoke();
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

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}