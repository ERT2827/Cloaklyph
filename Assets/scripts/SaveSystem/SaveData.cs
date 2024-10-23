using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{
    // This script is just generic functions I use to encode and decode data.
    
    public static void SavePlayer(saveManager Player){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.CLK";

        FileStream stream = new FileStream(path, FileMode.Create);

        Playerdata data = new Playerdata(Player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Playerdata LoadPlayer(){

        string path = Application.persistentDataPath + "/player.CLK";

        if(File.Exists(path)){

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Playerdata data = formatter.Deserialize(stream) as Playerdata;
            stream.Close();

            return data;

        }else{
            Debug.Log("No Files?");
            return null;
        }

    }
}
