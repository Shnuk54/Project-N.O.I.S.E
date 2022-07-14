﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System;

    public class Events : MonoBehaviour
    {
    public static event UnityAction<PlayerState> onPlayerChangeState;
    public static event UnityAction<CameraStates> onCameraChangeState;
    public static event UnityAction onPlayerBlink;
    public static event UnityAction onPlayerStartBlink;
    public static Events instance { get; private set; }

    private void Start()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void OnPlayerBlink()
    {
        if (onPlayerBlink != null)
        {
            onPlayerBlink.Invoke();
        }
    }
    public void OnPlayerStartBlink()
    {
        if (onPlayerStartBlink != null)
        {
            onPlayerStartBlink.Invoke();
        }
    }
    public void OnPlayerChangeState(PlayerState state)
    {
        if(onPlayerChangeState != null)
        {
            onPlayerChangeState.Invoke(state);
        }
    }
    public void OnCameraChangeState(CameraStates state)
    {
        if (onCameraChangeState != null)
        {
            onCameraChangeState.Invoke(state);
        }
    }

}
