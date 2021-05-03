using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{
    public Text codeText;

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void CopyOnClipboard()
    {
        TextEditor te = new TextEditor
        {
            text = codeText.text
        };
        te.SelectAll();
        te.Copy();
    }
}
