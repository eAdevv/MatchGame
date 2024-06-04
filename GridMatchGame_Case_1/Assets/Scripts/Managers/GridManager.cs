using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoSingleton<GridManager>
{
    private const float cellSpacingOffset = 1.5f;

    [SerializeField] private int Size;
    [SerializeField] private GameObject Cell;

    private GameObject Grid;
    private Camera myCamera;
    private void Start()
    {
        myCamera = Camera.main;
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

    private void GridCreatorEvent(int gridSize) // Grid Oluþturucu
    {
        EventManager.OnPoolSpawn?.Invoke("Cell", Cell, gridSize * gridSize);

        for (int row = 0; row < gridSize; row++)
        {
            for (int column = 0; column < gridSize; column++)
            {
                Vector3 SetGrid = new Vector3(column * cellSpacingOffset, row * cellSpacingOffset, 0);
                Grid = EventManager.InvokeOnGetPoolObject("Cell", SetGrid, Quaternion.identity);

            }
        }

        CenterCamera(gridSize);
    }

    private void CenterCamera(int _size) // Kamera Ayarý 
    {
        myCamera.orthographicSize = _size * 1.5f;
      
        float gridWidth = _size * cellSpacingOffset;
        float gridHeight = _size * cellSpacingOffset;
        Vector3 centerPosition = new Vector3(gridWidth / 2 - cellSpacingOffset / 2, gridHeight / 2 - cellSpacingOffset / 2, -10);

        myCamera.transform.position = centerPosition;
    }


}
