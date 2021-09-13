using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonHandler : ButtonFunc
{
    GridLayoutGroup gridLayoutGroup;
    void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();

        var buttons = gridLayoutGroup.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            var button = buttons[i];
            int level = i + 1;
            button.onClick.AddListener(delegate { LoadLevel(level); });
            var txtMesh = button.GetComponentInChildren<TextMeshProUGUI>();

            if (txtMesh)
            {
                txtMesh.text = level.ToString();
            }
        }
    }
}
