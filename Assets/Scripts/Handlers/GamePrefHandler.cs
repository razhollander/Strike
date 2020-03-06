using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePrefHandler
{
    private const string PASSWORD = "razboost";
    ES3Settings settings;
    public GamePrefHandler()
    {
        settings = new ES3Settings(ES3.EncryptionType.AES, PASSWORD);
        settings.location = ES3.Location.PlayerPrefs;
    }
    public T LoadPref<T>(string loadName)
    {
        return ES3.Load<T>(loadName, settings);
    }
    public void SavePref<T>(T savedObject, string saveName)
    {
        ES3.Save<T>(saveName, savedObject, settings);
    }
    public bool IsKeyExist(string keyName)
    {
        return ES3.KeyExists(keyName,settings);
    }
    public void DeleteKey(string keyName)
    {
        ES3.DeleteKey(keyName,settings);
    }
}
