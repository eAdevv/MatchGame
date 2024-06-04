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
    }

    void OnRebuildButtonClick()
    {
        if (InputSizeField != null)
        {
            string inputText = InputSizeField.text; // Field'dan girilen deðeri alýr.

            if (int.TryParse(inputText, out int number)) // Girilen deðeri integer'a çevir ve Gridi Rebuild et.
            {
                EventManager.OnPoolClear();
                EventManager.OnGridCreate?.Invoke(number);
            }
            else
            {
                Debug.LogError("Geçerli bir sayý girin.");
            }
        }
    }
}
