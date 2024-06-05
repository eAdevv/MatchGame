using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CellActivity : MonoBehaviour, IClickable
{
    public bool isMarked { get; private set; }
    public int neighbourCount;
    public GameObject Mark;

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
