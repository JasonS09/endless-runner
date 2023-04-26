using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackdropScroll : MonoBehaviour
{
    private new SpriteRenderer renderer;
    [SerializeField] private SpeedController speedController;
    [SerializeField] private float baseSpeed = 1;
    [SerializeField] private float offset = 0;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float speed = baseSpeed + speedController.Speed * Time.deltaTime;
        offset += Time.deltaTime * speed;
        renderer.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
