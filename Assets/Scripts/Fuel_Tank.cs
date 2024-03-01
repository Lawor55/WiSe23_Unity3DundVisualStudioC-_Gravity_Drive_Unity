using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel_Tank : MonoBehaviour
{
    [SerializeField] private float percentageOfMaxFuelToAdd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.fuel += percentageOfMaxFuelToAdd;
            gameObject.SetActive(false);
        }
    }
}
