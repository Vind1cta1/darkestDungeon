using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextChanger : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    public void ChangeText(string newText)
    {
        if (textMesh != null)
        {
            textMesh.text = newText;
        }
    }
}
