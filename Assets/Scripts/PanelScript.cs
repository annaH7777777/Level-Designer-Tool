using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{
    public Text codeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
    public void CopyOnClipboard()
    {
        TextEditor te = new TextEditor();
        te.text = codeText.text;
        te.SelectAll();
        te.Copy();
    }
}
