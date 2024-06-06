using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class GridManager : MonoSingleton<GridManager>
{ 
    [SerializeField] private int _Size;
    [SerializeField] private GameObject Cell;
    private GameObject[,] Grid;
    private List<Vector2Int> _markPositions = new List<Vector2Int>();
    private CameraManager _cameraManager;
    public int Size
    {
        get => _Size;
        set => _Size = value;
    }
    public List<Vector2Int> MarkPositions
    {
        get => _markPositions;
        set => _markPositions = value;
    }

    private Vector2Int[] directions = new Vector2Int[]
    {
            Vector2Int.left,
            Vector2Int.right,
            Vector2Int.up,
            Vector2Int.down
    };

    private void OnEnable()
    {
        EventManager.OnGridCreate += GridCreator;
        EventManager.OnGridReset += ResetGrid;
        EventManager.OnMatchCheck += CheckMatch;
    }

    private void OnDisable()
    {
        EventManager.OnGridCreate -= GridCreator;
        EventManager.OnGridReset -= ResetGrid;
        EventManager.OnMatchCheck -= CheckMatch;
    }

    [Inject]
    public void OnInstaller(CameraManager cameraManager)
    {
        _cameraManager = cameraManager;
    }
    private void Start()
    {
        GridCreator(Size);
    }



    #region Grid Create & Reset
    private void GridCreator(int gridSize) 
    {
        // Size'a göre Objeleri Spawnla
        EventManager.OnPoolSpawn?.Invoke("Cell", Cell, gridSize * gridSize);

        // Bos Gridi Oluþtur
        Grid = new GameObject[gridSize, gridSize]; 

        // Grid Islemleri
        for (int row = 0; row < gridSize; row++)
        {
            for (int column = 0; column < gridSize; column++)
            {
                Vector3 SetGrid = new Vector3(row , column , 0); // Cell Pozisyonu
                var GridCell = EventManager.InvokeOnGetPoolObject("Cell", SetGrid, Quaternion.identity); // Pooldan objeleri cekerek Celle dönüþtür
                Grid[row, column] = GridCell; // Gridi doldur

            }
        }

        _cameraManager.CameraSetSize(Size);
    }

    
    private void ResetGrid(int newSize)
    {
        // Gridi Resetle
        for (int row = 0; row < Grid.GetLength(0); row++)
        {
            for (int column = 0; column < Grid.GetLength(1); column++)
            {
                Destroy(Grid[row, column]);
            }
        }

        // Gridi Resetledikten sonra Poolu bosalt
        EventManager.OnPoolClear?.Invoke(); 

        // Yeni size ile yeni grid olustur.Eski datalari resetle.
        Size = newSize;
        GridCreator(Size);
        MarkPositions.Clear();
    }

    #endregion

    #region Match System
    private void CheckMatch(Vector2Int startPos)
    {
        for (int i = 0; i < directions.Length; i++) // Her marklanan obje icin 4 yonde tarama yapar.
        {
            var DirectionPoint = startPos - directions[i];
            if (MarkPositions.Contains(DirectionPoint) && Grid[DirectionPoint.x, DirectionPoint.y] != null) // Eger komsu bulunursa hücre ve komsusunun komsu sayacýný arttir.
            {
                Grid[startPos.x, startPos.y].GetComponent<CellActivity>().NeighbourCount++;

                var NeighBourPosition = startPos - directions[i];
                Grid[NeighBourPosition.x, NeighBourPosition.y].GetComponent<CellActivity>().NeighbourCount++;

            }
        }
        FindCellsDelete(); // Bu fonksiyon ilk once 2 komsusu olan hucreleri bulup etrafindaki sadece o hücreye komsu olan hucreyi kapatir.
                           // Daha sonra 2 komsulu hucreleri kapatir.
    }

    private void FindCellsDelete()
    {
        for (int i = 0; i < MarkPositions.Count; i++)
        {
            var GridCellObject = Grid[MarkPositions[i].x, MarkPositions[i].y]; // Isaretlenen objenin griddeki yeri
            var CellObjectPosition = new Vector2Int((int)GridCellObject.transform.position.x, (int)GridCellObject.transform.position.y); // Isaretlenen objenin griddeki yeri
            CheckNeighbour(GridCellObject,CellObjectPosition);
        } 
    }

    private void CheckNeighbour(GameObject CellObject , Vector2Int ObjectPosition)
    {
        

        if (CellObject.GetComponent<CellActivity>().NeighbourCount >= 2) // Komsu sayisi 2 veya daha fazla olan hücreleri kontrole et
        {
            foreach (var myDirection in directions)
            {
                var DirectionPoint = ObjectPosition - myDirection;
                if (MarkPositions.Contains(DirectionPoint) && Grid[DirectionPoint.x, DirectionPoint.y].GetComponent<CellActivity>().NeighbourCount == 1)
                {
                    MarkPositions.Remove(DirectionPoint);
                    Grid[DirectionPoint.x, DirectionPoint.y].GetComponent<CellActivity>().UnMarkCell();
                }
            }
            // Kalan 2 komsulu veya daha fazla olan hücre veya hücreleri cikar.
            MarkPositions.Remove(ObjectPosition);
            CellObject.GetComponent<CellActivity>().UnMarkCell();
        }
    }

    #endregion

   


}
