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
        byte[] bytesToEncode = Encoding.ASCII.GetBytes(stringToEncode); //Encoding.UTF8.GetBytes
        string encodedText  = Convert.ToBase64String(bytesToEncode);
        Debug.Log("encoded text " + encodedText);
        return encodedText;
        //byte[] decodedBytes1 = System.Convert.FromBase64String(encodedText);
        //string decodedText1 = System.Text.ASCIIEncoding.ASCII.GetString(decodedBytes1);
        //Debug.Log("decoded text " + decodedText1);
    }

    public static string Decode(string stringToDecode)
    {
        byte[] decodedBytes1 = System.Convert.FromBase64String(stringToDecode);
        //byte[] decodedBytes2 = System.Convert.FromBase64String("MzozOjA6MToxLTM6NDoxOjE6NC0zOjU6MDoxOjMtMzo2OjA6MToy");
        //byte[] decodedBytes3 = System.Convert.FromBase64String("MzozOjA6MToxLTM6NDowOjE6NC0zOjU6MDoxOjMtMzo2OjA6MTozLTM6NzowOjE6Mg ==");
        string decodedText1 = System.Text.ASCIIEncoding.ASCII.GetString(decodedBytes1);
        //string decodedText2 = System.Text.ASCIIEncoding.ASCII.GetString(decodedBytes2);
        //string decodedText3 = System.Text.ASCIIEncoding.ASCII.GetString(decodedBytes3);
        //Debug.Log(decodedText1);
        //Debug.Log(decodedText2);
        //Debug.Log(decodedText3);
        return (decodedText1);
    }
}
