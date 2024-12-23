using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

public static class SaveSys
{
    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/PlayersData.tantan";
        FileStream stream = new FileStream(path , FileMode.Create);

        Data data = new Data();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Data Load()
    {
        string path = Application.persistentDataPath + "/PlayersData.tantan";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream (path , FileMode.Open);

            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

    public static void DeleteData()
    {
        string path = Application.persistentDataPath + "/PlayersData.tantan";
        if(File.Exists(path))
        {
            File.Delete(path);
            UnityEngine.Debug.Log("Save file deleted.");
        }
    }
}
