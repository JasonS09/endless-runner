using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private float leftLimit = -50;
    [SerializeField] private float baseSpeed = 3;

    public float BaseSpeed
    {
        get
        {
            return baseSpeed;
        }

        set
        {
            baseSpeed = value;
        }
    }

    private void Update()
    {
        transform.Translate(-BaseSpeed * Time.deltaTime, 0, 0);
        DestroyPlatform();
    }

    private void DestroyPlatform()
    {
        if (transform.position.x < leftLimit)
        {
            Destroy(gameObject);
        }
    }
}
