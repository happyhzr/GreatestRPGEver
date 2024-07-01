using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;
            if (hitObject != null)
            {
                Debug.Log("Hit: " + hitObject.name);
                switch (hitObject.itemType)
                {
                    case Item.ItemType.COIN:
                        break;
                    case Item.ItemType.HEALTH:
                        AdjustHitPoints(hitObject.quantity);
                        break;
                    default:
                        break;
                }
                collision.gameObject.SetActive(false);
            }
        }
    }

    private void AdjustHitPoints(int amount)
    {
        hitPoints += amount;
        Debug.Log("Adjusted hitpoints by: " + amount + ". New value: " + hitPoints);
    }
}
