using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Obstacle : MonoBehaviour
{
    [SerializeField] GameObject fishAdd;
    [SerializeField] int fishToAdd;
    private PlayerController playerController;
    [SerializeField] TextMeshPro positiveText;
    [SerializeField] TextMeshPro negativeText;
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (fishToAdd >0)
        {
            positiveText.text = "+" + fishToAdd;
        }else
        {
            negativeText.text = "" + fishToAdd;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (fishToAdd >0)
            {
                for (int i = 0; i < fishToAdd; i++)
                {
                    Instantiate(fishAdd, other.transform.position, Quaternion.identity);
                }
            }
            else
            {
                for (int i = 0; i < -fishToAdd; i++)
                {
                    if (playerController.fishList.Count<=0)
                    {
                        LevelController.Current.GameOver();
                    }
                    playerController.fishList[0].SetActive(false);
                    playerController.fishList.RemoveAt(0);
                }
            }
           
        }
    }
}
