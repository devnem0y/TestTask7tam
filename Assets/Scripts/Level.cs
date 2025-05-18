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
    public event Action<bool> Begin;

    private List<ItemData> _itemsData;
    private List<Item> _fieldItems;
    
    private int _refreshNumber;
    private bool _isBegin;

    public void Init(Action action)
    {
        _bar = new Bar();
        _fieldItems = new List<Item>();
        Begin?.Invoke(_isBegin);
        StartCoroutine(GenerateItems(_itemStorage.Items));
        action.Invoke();
    }

    private IEnumerator GenerateItems(ICollection<ItemData> items)
    {
        _itemsData = new List<ItemData>(items);
        
        MyTools.Shuffle(_itemsData);
        
        var spawnPosition = _spawnPoint.position;
        var count = items.Count;
        
        for (var i = 0; i < count; i++)
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
                item.Init(firstItem);
                item.Selected += OnItemSelected;
            }
            
            _fieldItems.Add(item);
            _itemsData.RemoveAt(0);
            
            yield return new WaitForSeconds(0.27f);
        }

        _isBegin = true;
        Begin?.Invoke(_isBegin);
    }

    private void OnItemSelected(Item item, Vector3 selectPosition)
    {
        if (!_isBegin) return;
        
        var cell = _bar.GetEmptyCellByType(item.ItemData.Type);

        if (cell == null) return;
        
        cell.SetItem(item.ItemData);
        AddItem?.Invoke(cell.Id, item.ItemData, selectPosition);
        _fieldItems.Remove(item);
        Destroy(item.gameObject);
            
        Invoke(nameof(CheckingForMatch), 1.17f); //TODO: Для тестового пойдет, но можно сделать лучше
    }

    private void CheckingForMatch()
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
        
        if (_fieldItems.Count <= 0) Game.Instance.ChangeState(GameState.VICTORY);
        if (_bar.IsFull()) Game.Instance.ChangeState(GameState.DEFEAT);
    }

    public void Refresh()
    {
        _isBegin = false;
        Begin?.Invoke(_isBegin);
        var itemsData = _fieldItems.Select(fieldItem => fieldItem.ItemData).ToList();
        _fieldItems.Clear();
        foreach (Transform child in _itemsParent) Destroy(child.gameObject);
        StartCoroutine(GenerateItems(itemsData));
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}