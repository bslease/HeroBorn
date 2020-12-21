using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float onScreenDelay = 3f;

    void Start()
    {
        Destroy(this.gameObject, onScreenDelay);
    }
}
