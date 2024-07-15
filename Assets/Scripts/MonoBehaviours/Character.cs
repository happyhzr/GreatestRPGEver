using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] public float maxHitPoints;
    [SerializeField] protected float startingHitPoints;

    protected virtual void KillCharacter()
    {
        Destroy(gameObject);
    }

    protected abstract void ResetCharacter();
    public abstract IEnumerator DamageCharacter(int damage, float interval);

    protected virtual IEnumerator FlickerCharacter()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
