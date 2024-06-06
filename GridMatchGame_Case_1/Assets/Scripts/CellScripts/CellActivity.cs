using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;
public class CellActivity : MonoBehaviour, IClickable
{
    private bool isMarked;
    private int neighbourCount;
    private GridManager _gridManager;

    public GameObject Mark;


    public int NeighbourCount
    {
        get => neighbourCount;
        set => neighbourCount = value;
    }

  
    public void Click()
    {
        if (!isMarked) MarkCell();
    }

    private void MarkCell()
    {
        isMarked = true;
        Mark.SetActive(true);

        // Isaretledigimiz hucrenin Griddeki pozisyonunu bul ve Listede tut.
        Vector2Int MarkedPosition = new Vector2Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y);
        GridManager.Instance.MarkPositions.Add(MarkedPosition);

        // Marklanan hücre icin tarama yap.
        EventManager.OnMatchCheck?.Invoke(MarkedPosition);
    }

    public void UnMarkCell()
    {
        isMarked = false;
        Mark.SetActive(false);
        neighbourCount = 0;

    }

}
