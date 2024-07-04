using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGameManager : MonoBehaviour
{
    public static RPGGameManager sharedInstance = null;

    [SerializeField] private SpawnPoint playerSpawnPoint;

    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(sharedInstance);
        }
        else
        {
            sharedInstance = this;
        }
    }

    private void Start()
    {
        SetupScene();
    }

    private void SetupScene()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (playerSpawnPoint != null)
        {
            GameObject player = playerSpawnPoint.SpawnObject();
        }
    }
}
