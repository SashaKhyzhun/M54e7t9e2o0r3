using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    public static string fileName;
    public static string fileExtention;

    private static Game savedGame;

    public static void Save()
    {
        Game.current.timeStamp = DateTime.Now;
        savedGame = Game.current;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + fileName + "." + fileExtention);
        bf.Serialize(file, savedGame);
        file.Close();
        //GPGController.OpenSavedGame(Game.current.name, OpenMode.Save);
    }

    public static bool Load()
    {
        bool localLoadSuccess = LoadLocal();
        //if (Game.current != null)
        //   GPGController.OpenSavedGame(Game.current.name, OpenMode.Load);
        Game.current = Game.local;
        return localLoadSuccess;
    }

    public static bool LoadLocal()
    {
        if (File.Exists(Application.persistentDataPath + "/" + fileName + "." + fileExtention))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + fileName + "." + fileExtention, FileMode.Open);
            savedGame = (Game)bf.Deserialize(file);
            Game.local = savedGame;
            file.Close();
            if (GPGController.NoGPGMode)
            {
                Game.current = savedGame;
            }
            return true;
        }
        else { return false; }
    }

    public static void LoadCloud(byte[] data, bool success)
    {
        if (success)
        {
            BinaryFormatter bf = new BinaryFormatter();
            File.WriteAllBytes(Application.persistentDataPath + "/" + fileName + "_cloud_backup." + fileExtention, data);
            FileStream file = File.Open(Application.persistentDataPath + "/" + fileName + "_cloud_backup." + fileExtention, FileMode.Open);
            savedGame = (Game)bf.Deserialize(file);
            Game.cloud = savedGame;
            file.Close();
            Debug.Log("Loading was successful");
        }
        else
        {
            Debug.Log("Loading failed");
        }
        ChooseSavedGame();
        GameObject.FindGameObjectWithTag("GameController").SendMessage("LoadStats");
    }

    static void ChooseSavedGame()
    {
        int i = DateTime.Compare(Game.local.timeStamp, Game.cloud.timeStamp);
        if (i > 0)
        {
            Game.current = Game.local;
        }
        else
        {
            Game.current = Game.cloud;
        }
    }
}
