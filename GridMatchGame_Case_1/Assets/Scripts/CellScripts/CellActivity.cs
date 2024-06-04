using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CellActivity : MonoBehaviour, IClickable
{
    public bool isMarked { get; private set; }

    private GameObject Mark;

    private void Start()
    {
        Mark = transform.GetChild(0).gameObject;
    }

    public void Click()
    {
        MarkCell();
    }

    private void MarkCell()
    {
        isMarked = true;
        Mark.SetActive(true);
    }

    private void UnMarkCell()
    {
        isMarked = false;
        Mark.SetActive(false);
    }
}
