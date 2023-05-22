﻿[System.Serializable]
public class PlayerData
{
    public static int defaultLives = 3;

    public int coins;
    public int lives = defaultLives;
    public string name;
    public int scoreboard;

    //public List<string> rewards;

    // Custom data storage.
    public Config custom = new Config();
    public static int CountLives()
    {
        if (_data == null) return 0;

        var lives = 0;

        for (int i = 0; i < _data.Length; i++)
        {
            lives += _data[i].lives;
        }

        return lives;
    }
    // Returns the PlayerData for the player by index.
    public static PlayerData Get(int index = 0)
    {
        if (index < 0) return null;
        if (_data == null) System.Array.Resize(ref _data, index + 1);
        if (index >= _data.Length) System.Array.Resize(ref _data, index + 1);
        if (_data[index] == null) _data[index] = new PlayerData();

        return _data[index];
    }
    // Returns the PlayerData for the player by name.
    public static PlayerData Get(string name)
    {
        if (_data != null)
        {
            for (int i = 0; i < _data.Length; i++)
            {
                if (_data[i].name == name) return Get(i);
            }

        }

        return null;
    }
    // Reset to defaults
    public static void Reset(int lives)
    {
        if (_data == null) return;

        for (int i = 0; i < _data.Length; i++)
        {
            _data[i].coins = 0;
            _data[i].lives = lives;
            _data[i].scoreboard = 0;

            //if (_data[i].rewards != null) _data[i].rewards.Clear();

            _data[i].custom.Clear();
        }
    }
    // Set number of lives
    public static void SetLives(int lives)
    {
        if (_data == null) return;

        for (int i = 0; i < _data.Length; i++)
        {
            _data[i].lives = lives;
        }
    }

    static PlayerData[] _data;
}