using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger10 : MonoBehaviour
{
    PlayerController playerController;
    public GameObject fish1, fish2, fish3, fish4;
    public Animator childAnim1, childAnim2;
    [SerializeField] ParticleSystem dolarParticle;
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FinishLastXDecreaseToFishCount();
        }
    }

    private void FinishLastXDecreaseToFishCount()
    {
        
        childAnim1.SetBool("WinRed", true);
        LevelController.Current.ChangeScore(50);
        fish1.SetActive(true);
        if (playerController.fishList.Count <= 1)
        {
            playerController.fishList[0].SetActive(false);
            playerController.fishList.RemoveAt(0);
        }
        else if (playerController.fishList.Count <= 2)
        {
            playerController.fishList[0].SetActive(false);
            playerController.fishList[1].SetActive(false);
            playerController.fishList.RemoveAt(0);
            playerController.fishList.RemoveAt(0);
            fish2.SetActive(true);
            childAnim2.SetBool("WinRed", true);
            LevelController.Current.ChangeScore(50);
        }
        else if (playerController.fishList.Count <= 3)
        {
            fish2.SetActive(true);

            fish3.SetActive(true);
            childAnim2.SetBool("WinRed", true);
            Debug.Log("Balýk sil");
            for (int i = 0; i < 3; i++)
            {
                Destroy(playerController.fishList[0].gameObject);
                playerController.fishList.RemoveAt(0);
            }
            LevelController.Current.ChangeScore(100);
        }
        else if (playerController.fishList.Count >= 4)
        {
            fish2.SetActive(true);
            fish3.SetActive(true);
            fish4.SetActive(true);
            childAnim2.SetBool("WinRed", true);
            Debug.Log("Balýk sil");
            for (int i = 0; i < playerController.fishList.Count; i++)
            {
                Destroy(playerController.fishList[0].gameObject);
                playerController.fishList.RemoveAt(0);
            }
            LevelController.Current.ChangeScore(150);
            dolarParticle.Play();
        }
    }
}
