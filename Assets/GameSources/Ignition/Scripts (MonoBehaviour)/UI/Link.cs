﻿using UnityEngine;
using UnityEngine.EventSystems;

public class Link : MonoBehaviour, IPointerClickHandler
{
    public string link;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Application.OpenURL(link);
    }
}