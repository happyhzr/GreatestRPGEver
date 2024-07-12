using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arc : MonoBehaviour
{
    public IEnumerator TravelArc(Vector3 destination, float duration)
    {
        Vector3 startPosition = transform.position;
        float percentComplete = 0f;
        while (percentComplete < 1f)
        {
            percentComplete += Time.deltaTime / duration;
            float currentHeight = Mathf.Sin(Mathf.PI * percentComplete);
            transform.position = Vector3.Lerp(startPosition, destination, percentComplete) + Vector3.up * currentHeight;
            percentComplete += Time.deltaTime / duration;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
