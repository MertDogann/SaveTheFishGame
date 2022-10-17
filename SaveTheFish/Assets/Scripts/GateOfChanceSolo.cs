using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOfChanceSolo : MonoBehaviour
{
    [SerializeField] GameObject chanceFish;
    [SerializeField] GameObject chanceRod;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
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
}
