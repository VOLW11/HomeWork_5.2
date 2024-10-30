using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffectDestroy : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        Controller controller = other.GetComponent<Controller>();

        if (controller != null)
            Destroy(gameObject);
    }
}
