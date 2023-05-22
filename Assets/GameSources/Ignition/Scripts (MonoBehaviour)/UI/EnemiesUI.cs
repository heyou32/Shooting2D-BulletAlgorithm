﻿using UnityEngine;
using UnityEngine.UI;
public class EnemiesUI : MonoBehaviour
{
    public int format;
    public string prefix;
    public string suffix;
    public Text text;

    void Awake()
    {
        if (text == null) text = GetComponent<Text>();
    }

    void Update()
    {
        var count = GameData.progress.ToString();

        if (format > 0) count = StringHelpers.Format(count, format);

        if (prefix != "") count = prefix + count;
        if (suffix != "") count += suffix;

        text.text = count;
    }
}