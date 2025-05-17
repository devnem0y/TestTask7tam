using System;
using UnityEngine;

public interface ILevel
{
    IBar Bar { get; }

    event Action<int, ItemData, Vector3> AddItem;
    event Action<int> RemoveItem;
    event Action<bool> Begin;

    void CheckingForMatch();
    void Refresh();
}