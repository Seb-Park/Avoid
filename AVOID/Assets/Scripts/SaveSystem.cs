using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SaveBoolArray (bool[] boolArray, string filename){//array you want to save + the name of the file
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + filename;
        FileStream stream = new FileStream(path, FileMode.Create);
        bf.Serialize(stream, boolArray);
        stream.Close();
    }

    public static bool[] LoadBoolArray(string filename)
    {//array you want to save + the name of the file
        string path = Application.persistentDataPath + "/" + filename;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            bool[] returnableBool = formatter.Deserialize(stream) as bool[];

            stream.Close();

            return returnableBool;
        }
        else{
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
