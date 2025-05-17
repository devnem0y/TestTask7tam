using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour, ILevel
{
    [SerializeField] private ItemStorage _itemStorage;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _itemsParent;
    
    private Bar _bar;
    public IBar Bar => _bar;
    public event Action<int, ItemData, Vector3> AddItem;
    public event Action<int> RemoveItem;

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

            if (item != null)
            {
                item.Init(i.ToString(), firstItem);
                item.Selected += OnItemSelected;
            }
            
            _itemsData.RemoveAt(0);
            
            yield return new WaitForSeconds(0.27f);
        }
    }

    private void OnItemSelected(Item item, Vector3 selectPosition)
    {
        var cell = _bar.GetEmptyCellByType(item.ItemData.Type);

        if (cell == null) return;
        
        cell.SetItem(item.ItemData);
        AddItem?.Invoke(cell.Id, item.ItemData, selectPosition);
        Destroy(item.gameObject);
            
        Invoke(nameof(CheckingForMatch), 1.17f);
    }

    public void CheckingForMatch()
    {
        var tmpCells = new List<Cell>();
        
        for (var i = 0; i < _bar.Length; i++)
        {
            var cell = _bar.GetCellByIndex(i);
            if (!cell.IsEmpty) tmpCells.Add((Cell)cell);
        }

        if (tmpCells.Count < 3) return;
        
        var cells = new List<Cell>();

        foreach (var list in tmpCells.Select(tmpCell => _bar.GetCellsByKey(tmpCell.ItemData.Key)).
                     Where(list => list.Count >= 3)) cells = new List<Cell>(list);
            
        foreach (var tmpCell in cells)
        {
            _bar.GetCellById(tmpCell.Id).Clear();
            RemoveItem?.Invoke(tmpCell.Id);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}