using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private bool startVisible;
    public static CanvasGroupController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start() => ShowHideMenu(startVisible);

    private void ShowHideMenu(bool value)
    {
        canvasGroup.alpha = value ? 1 : 0;
        canvasGroup.interactable = value;
        canvasGroup.blocksRaycasts = value;
    }


}
