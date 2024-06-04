using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoSingleton<GridManager>
{
    [SerializeField] private int Size;
    [SerializeField] private GameObject Cell;

    private GameObject Grid;
    private const float CellSpacingOffset = 1.5f;

    private void Start()
    {
        GridCreatorEvent(Size);
    }
    private void OnEnable()
    {
        EventManager.OnGridCreate += GridCreatorEvent;
    }

    private void OnDisable()
    {
        EventManager.OnGridCreate -= GridCreatorEvent;
    }

    private void GridCreatorEvent(int gridSize)
    {
        EventManager.OnPoolSpawn?.Invoke("Cell", Cell, gridSize * gridSize);

        for (int row = 0; row < gridSize; row++)
        {
            for (int column = 0; column < gridSize; column++)
            {
                Vector3 SetGrid = new Vector3(column * CellSpacingOffset, row * CellSpacingOffset, 0);
                EventManager.InvokeOnGetPoolObject("Cell", SetGrid, Quaternion.identity);
            }
        }
    }

    
}
