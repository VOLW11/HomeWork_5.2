using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffectDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();

        if (character != null)
            Destroy(gameObject);
    }
}
