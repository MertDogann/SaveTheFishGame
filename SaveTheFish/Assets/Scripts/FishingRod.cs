using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FishingRod : MonoBehaviour
{
    private BoxCollider collBox;
    private PlayerController playerController;
    [SerializeField] GameObject fishRodFish;
    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            if(playerController.fishList.Count > 0)
            {
                fishRodFish.SetActive(true);
                Destroy(fishRodFish, 2f);
                Destroy(gameObject, 5f);
                int listNumber = playerController.fishList.Count;
                Destroy(playerController.fishList[listNumber - 1]);
                playerController.fishList.RemoveAt(listNumber - 1);
                collBox = GetComponent<BoxCollider>();
                collBox.enabled = false;
            }

        }
    }
}
