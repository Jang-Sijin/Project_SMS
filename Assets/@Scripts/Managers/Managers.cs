using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : Singleton<Managers>
{
    public MapLevelManager MapLevel { get { return _mapLevel; } }
    private MapLevelManager _mapLevel = new MapLevelManager();

    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                // 씬에서 해당 타입의 객체를 찾는다.
                _instance = FindObjectOfType<T>();

                // 씬에 해당 타입의 객체가 없으면 새로 생성
                if( _instance == null )
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    protected void Awake()
    {
        if( _instance != null && _instance != this ) 
        { 
            Destroy(_instance);
        }
        else
        {
            _instance = this as T;
        }
    }

    protected void OnDestroy()
    {
        if(_instance == this)
        {
            _instance = null;
        }
    }
}