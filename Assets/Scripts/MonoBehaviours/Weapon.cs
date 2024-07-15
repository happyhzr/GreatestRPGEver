using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject ammoPrefab;
    private static List<GameObject> ammoPool;
    [SerializeField] private int poolSize;
    [SerializeField] private float weaponVelocity;
    private bool isFiring;
    [HideInInspector] public Animator animator;
    private Camera localCamera;
    private float positiveSlope;
    private float negativeSlope;

    private enum Quadrant
    {
        East,
        South,
        West,
        North,
    }

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
            isFiring = true;
            FireAmmo();
        }
        UpdateState();
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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject ammo = SpawnAmmo(transform.position);
        if (ammo != null)
        {
            Arc arcScript = ammo.GetComponent<Arc>();
            float travelDuration = 1f / weaponVelocity;
            StartCoroutine(arcScript.TravelArc(mousePosition, travelDuration));
        }
    }

    private void OnDestroy()
    {
        ammoPool = null;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        isFiring = false;
        localCamera = Camera.main;
        Vector2 lowerLeft = localCamera.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 upperRight = localCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 upperLeft = localCamera.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 lowerRight = localCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        positiveSlope = GetSlope(lowerLeft, upperRight);
        negativeSlope = GetSlope(upperLeft, lowerRight);
    }

    private float GetSlope(Vector2 pointOne, Vector2 pointTwo)
    {
        return (pointTwo.y - pointOne.y) / (pointTwo.x - pointOne.x);
    }

    private bool HigherThanPositiveSlopeLine(Vector2 inputPosition)
    {
        Vector2 playerPosition = gameObject.transform.position;
        Vector2 mousePosition = localCamera.ScreenToWorldPoint(inputPosition);
        float yIntercept = playerPosition.y - (positiveSlope * playerPosition.x);
        float inputIntercept = mousePosition.y - (positiveSlope * mousePosition.x);
        return inputIntercept > yIntercept;
    }

    private bool HigherThanNegativeSlopeLine(Vector2 inputPosition)
    {
        Vector2 playerPosition = gameObject.transform.position;
        Vector2 mousePosition = localCamera.ScreenToWorldPoint(inputPosition);
        float yIntercept = playerPosition.y - (negativeSlope * playerPosition.x);
        float inputIntercept = mousePosition.y - (negativeSlope * mousePosition.x);
        return inputIntercept > yIntercept;
    }

    private Quadrant GetQuadrant()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 playerPosition = transform.position;
        bool higherThanPositiveSlopeLine = HigherThanPositiveSlopeLine(mousePosition);
        bool higherThanNegativeSlopeLine = HigherThanNegativeSlopeLine(mousePosition);
        if (!higherThanPositiveSlopeLine && higherThanNegativeSlopeLine)
        {
            return Quadrant.East;
        }
        else if (!higherThanPositiveSlopeLine && !higherThanNegativeSlopeLine)
        {
            return Quadrant.South;
        }
        else if (higherThanPositiveSlopeLine && !higherThanNegativeSlopeLine)
        {
            return Quadrant.West;
        }
        else
        {
            return Quadrant.North;
        }
    }

    private void UpdateState()
    {
        if (isFiring)
        {
            Vector2 quadrantVector;
            Quadrant quadEnum = GetQuadrant();
            switch (quadEnum)
            {
                case Quadrant.East:
                    quadrantVector = new Vector2(1f, 0f);
                    break;
                case Quadrant.South:
                    quadrantVector = new Vector2(0f, -1f);
                    break;
                case Quadrant.West:
                    quadrantVector = new Vector2(-1f, 0f);
                    break;
                case Quadrant.North:
                    quadrantVector = new Vector2(0f, 1f);
                    break;
                default:
                    quadrantVector = new Vector2(0f, 0f);
                    break;
            }
            animator.SetBool("isFiring", true);
            animator.SetFloat("fireXDir", quadrantVector.x);
            animator.SetFloat("fireYDir", quadrantVector.y);
            isFiring = false;
        }
        else
        {
            animator.SetBool("isFiring", false);
        }
    }
}
