using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject ammoPrefab;
    private static List<GameObject> ammoPool;
    [SerializeField] private int poolSize;

    private void Awake()
    {
        if (ammoPool == null)
        {
            ammoPool = new List<GameObject>();
        }
        for (int i = 0; i < poolSize; i++)
        {
            GameObject ammoObject = Instantiate(ammoPrefab);
            ammoObject.SetActive(false);
            ammoPool.Add(ammoObject);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireAmmo();
        }
    }

    private GameObject SpawnAmmo(Vector3 location)
    {
        foreach (GameObject ammo in ammoPool)
        {
            if (!ammo.activeSelf)
            {
                ammo.SetActive(true);
                ammo.transform.position = location;
                return ammo;
            }
        }
        return null;
    }

    private void FireAmmo()
    {

    }

    private void OnDestroy()
    {
        ammoPool = null;
    }
}
