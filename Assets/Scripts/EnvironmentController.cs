using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    [SerializeField] private GameObject[] environmentElement;
    [SerializeField] private GameObject[] rareEnvironmentElement;
    [SerializeField] private Transform referencePoint;
    [SerializeField] private SpeedController speedController;
    [SerializeField] private Vector3 offset = new Vector3(10, 0);
    [SerializeField] private int rareFrequency = 6;
    private GameObject instantiatedElement;
    

    private void Start()
    {
        StartCoroutine(CreateEnvironmentElement());
    }

    private IEnumerator CreateEnvironmentElement(bool isRare = false)
    {
        instantiatedElement = 
            Instantiate(
                isRare ? rareEnvironmentElement[Random.Range(0, rareEnvironmentElement.Length)] : environmentElement[Random.Range(0, environmentElement.Length)], 
                referencePoint.position + offset, 
                Quaternion.identity, 
                gameObject.transform);
        instantiatedElement.GetComponent<Platform>().BaseSpeed += speedController.Speed;

        yield return new WaitForSeconds(Random.Range(3, 6));

        StartCoroutine(CreateEnvironmentElement(Random.Range(0, rareFrequency + 1) == rareFrequency ? true : false));
    }
}
