using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ButtonHandler : ButtonFunc
{
    GridLayoutGroup gridLayoutGroup;
    public GameObject lvlButton;
    void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();

        var scenes = GlobalControl.Instance.scenesInBuild;

        foreach (var scene in scenes)
        {
            if (scene.Contains("level") && scene.Any(char.IsDigit))
            {
                Instantiate(lvlButton, gridLayoutGroup.transform);
            }
        }

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
