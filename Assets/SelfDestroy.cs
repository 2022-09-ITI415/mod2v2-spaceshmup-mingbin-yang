using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 1f);
    }
}
