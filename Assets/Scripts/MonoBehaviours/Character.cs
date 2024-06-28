using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected int hitPoints;
    [SerializeField] protected int maxHitPoints;
}
