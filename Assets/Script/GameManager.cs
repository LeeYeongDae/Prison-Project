using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameObject("GameManager").AddComponent<GameManager>();
            return instance;
        }
    }

    private void Awake()
    {
        GameObject go = GameObject.Find("GameManager");
        if (go == null)
        {
            go = new GameObject { name = "GameManager" };
            go.AddComponent<GameManager>();
        }
        DontDestroyOnLoad(go);
        instance = go.GetComponent<GameManager>();

        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        
    }
}
