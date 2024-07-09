using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    float hitPoints;
    [SerializeField] private int damageStrength;
    Coroutine damageCoroutine;

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            hitPoints -= damage;
            if (hitPoints <= float.Epsilon)
            {
                KillCharacter();
                break;
            }
            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }

    protected override void ResetCharacter()
    {
        hitPoints = startingHitPoints;
    }

    private void OnEnable()
    {
        ResetCharacter();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision entered");
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 1f));
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("collision exited");
        if (collision.gameObject.CompareTag("Player"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }
}
