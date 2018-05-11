using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public DatabaseManager Instance;

    private string m_DatabaseURL;
    private string m_Username;
    private string m_Password;
    private string m_DatabaseName;

    private bool m_IsConnected;
    private WWWForm m_PosingData;
    

	// Use this for initialization
	void Start ()
    {
        if (Instance == null)
        {
            Instance = this;
        }
	}

    public void ConnectToDatabase()
    {

    }

    public void DisconnectFromDatabase()
    {
        
    }

    public void GetInfoFromDatabase(string aDatabaseURL)
    {

    }

    public void PostInfoToDatabase(string aDatabaseURL, string aDataToSend)
    {

    }

    public void GetUserData()
    {
        //Will change to return a userdata stuff
    }

    //This encrypt cannot be done done... so use it for username/password verification
    public string EncryptInfo(string aDataToEncrypt)
    {
        MD5 md5Encrypt = MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(aDataToEncrypt);
        byte[] hashData = md5Encrypt.ComputeHash(inputBytes);

        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

        for (int i = 0; i < hashData.Length; i++)
        {
            stringBuilder.Append(hashData[i].ToString("X2"));
        }

        return stringBuilder.ToString();
    }

}
