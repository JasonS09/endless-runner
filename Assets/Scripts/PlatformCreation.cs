using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformCreation : MonoBehaviour
{
    [SerializeField] private List<GameObject> platformsPrefab;
    [SerializeField] private List<GameObject> mediumDifficultyPlatformsPrefab;
    [SerializeField] private Transform referencePoint;
    [SerializeField] private GameObject lastCreatedPlatform;
    [SerializeField] private SpeedController speedController;
    [SerializeField] private float spaceBetweenPlatforms = 2;
    [SerializeField] private float maxSpaceBetweenPlatforms = 4.5f;
    private float lastPlatformWidth;

    private void Update()
    {
        CreatePlatform();
    }

    private void CreatePlatform()
    {
        if (lastCreatedPlatform.transform.position.x < referencePoint.position.x)
        {
            float randomSpaceBetweenPlatforms = Random.Range(spaceBetweenPlatforms, maxSpaceBetweenPlatforms);
            Vector3 targetCreationPoint = new Vector3(referencePoint.position.x + lastPlatformWidth + randomSpaceBetweenPlatforms, 0, 0);
            int randomPlatform = Random.Range(0, platformsPrefab.Count);
            lastCreatedPlatform = Instantiate(platformsPrefab[randomPlatform], targetCreationPoint, Quaternion.identity, gameObject.transform);
            lastCreatedPlatform.GetComponent<Platform>().BaseSpeed += speedController.Speed;
            BoxCollider2D collider = lastCreatedPlatform.GetComponent<BoxCollider2D>();
            lastPlatformWidth = collider.bounds.size.x;
        }
    }

    public void addMediumDifficultPlatforms()
    {
        platformsPrefab.AddRange(mediumDifficultyPlatformsPrefab);   
    }
}
