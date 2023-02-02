using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{

    public static UnityEvent OnLevelStart = new UnityEvent();

    public static UnityEvent OnLevelFinish = new UnityEvent();
    public static UnityEvent OpenWinPanel = new UnityEvent();
    public static UnityEvent OpenFailPanel= new UnityEvent();

}