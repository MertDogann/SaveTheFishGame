using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOfChance : MonoBehaviour
{
    [SerializeField] GameObject chanceFish;
    [SerializeField] GameObject chanceRod;
    [SerializeField] GameObject oppositeChanceFish;
    [SerializeField] GameObject oppositeChanceRod;

    void Update()
    {
        if (chanceRod.activeInHierarchy == true)
        {
            oppositeChanceFish.SetActive(true);
        }
        else if (chanceFish.activeInHierarchy)
        {
            oppositeChanceRod.SetActive(true);
        } 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().xSpeed = 0f;
            Destroy(gameObject, 5f);
            int randomActive = Random.Range(0, 2);
            if (randomActive == 0)
            {
                chanceFish.SetActive(true);
            }
            else if (randomActive == 1)
            {
                chanceRod.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().xSpeed = 1000f;

        }
    }


}
