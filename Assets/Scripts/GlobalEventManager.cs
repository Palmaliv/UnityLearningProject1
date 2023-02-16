using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    private static GlobalEventManager _instance = null;
    public static UnityEvent OnLevelComplete = new UnityEvent();
    public static UnityEvent OnObstacleCollision = new UnityEvent();

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public static void LevelComplete()
    {
        OnLevelComplete.Invoke();
    }

    public static void ObstacleCollision()
    {
        OnObstacleCollision.Invoke();
    }
}
