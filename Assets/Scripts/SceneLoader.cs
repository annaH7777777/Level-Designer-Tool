using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Image sprite;
    string spriteString;
    
    // Start is called before the first frame update
    void Start()
    {
        byte[] spriteByte = sprite.sprite.texture.EncodeToJPG();
        spriteString = Convert.ToBase64String(spriteByte);
        Debug.Log(spriteString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
