using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SpeedController : MonoBehaviour
{
    [SerializeField] private float increaseSpeedLimit = 5;
    [SerializeField] private float secondsToIncreaseSpeed = 5;
    [SerializeField] private float valueToIncreaseBy = 0.0014f;
    [SerializeField] private float mediumDifficultySpeed = 2;
    public UnityEvent OnMediumDifficulty;
    public float Speed { get; private set; } 

    private void Start()
    {
        StartCoroutine(IncreaseSpeed());
    }

    private void Update()
    {
        CheckIfMediumDificulty();
    }

    private IEnumerator IncreaseSpeed()
    {
        
        Speed += valueToIncreaseBy;

        yield return new WaitForSeconds(secondsToIncreaseSpeed);

        if (Speed < increaseSpeedLimit)
        {
            StartCoroutine(IncreaseSpeed());
        }      
    }

    private void CheckIfMediumDificulty()
    {
        if (Speed >= mediumDifficultySpeed)
        {
            OnMediumDifficulty.Invoke();
            OnMediumDifficulty = new UnityEvent();
        }  
    }
}
