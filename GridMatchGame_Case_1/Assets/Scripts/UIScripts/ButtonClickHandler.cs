using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonClickHandler : MonoBehaviour
{
    private Button myButton;
    [SerializeField] private TMP_InputField InputSizeField;
    private void Start()
    {
        myButton = this.gameObject.GetComponent<Button>();

        if(myButton != null)
            myButton.onClick.AddListener(OnRebuildButtonClick); // Button click

        InputSizeField.text = GridManager.Instance.Size.ToString();
    }

    void OnRebuildButtonClick()
    {
        if (InputSizeField != null)
        {
            string inputText = InputSizeField.text; // Field'dan girilen degeri al.

            if (int.TryParse(inputText, out int number)) // Girilen degeri integer'a çevir ve Gridi Rebuild et.
            {
                EventManager.OnGridReset?.Invoke(number);
            }
            else
            {
                Debug.LogError("Geçerli bir sayý girin.");
            }
        }
    }
}
