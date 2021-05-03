using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public static class StringProvider
{
    
    public static string Encode(string stringToEncode)
    {
        byte[] bytesToEncode = Encoding.ASCII.GetBytes(stringToEncode); 
        string encodedText  = Convert.ToBase64String(bytesToEncode);
        return encodedText;
    }

    public static string Decode(string stringToDecode)
    {
        byte[] decodedBytes = System.Convert.FromBase64String(stringToDecode);
        string decodedText = System.Text.ASCIIEncoding.ASCII.GetString(decodedBytes);
        return (decodedText);
    }
}
