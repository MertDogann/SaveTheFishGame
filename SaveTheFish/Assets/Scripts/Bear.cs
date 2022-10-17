using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    private Animator bearAnim;
    private BoxCollider collBox;
    private PlayerController playerController;
    [SerializeField] GameObject glassBreak;
    [SerializeField] List<GameObject> fishBearList = new List<GameObject>();
    [SerializeField] private ParticleSystem particleEffect;

    void Start()
    {
        bearAnim = GetComponent<Animator>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!playerController.GetAquareum1)
            {
                GameObject glassBreakk =  Instantiate(glassBreak, other.transform.position + new Vector3(0, 0.5f, 6.5f), other.transform.rotation , this.transform);
                glassBreakk.transform.SetParent(null);
                particleEffect.Play();
            }
            PreviousAquareum();
            //DecreaseFish();
            bearAnim.SetTrigger("Catching");
            for (int i = 0; i < fishBearList.Count; i++)
            {
                fishBearList[i].SetActive(true);
            }
            Destroy(gameObject, 5f);
            collBox = GetComponent<BoxCollider>();
            collBox.enabled = false;
        }
    }
    private void DecreaseFish()             
    {
        for (int i = 0; i < (playerController.aquareumcapacity3- playerController.aquareumcapacity2); i++)
        {
            int listNumber = playerController.fishList.Count;
            Destroy(playerController.fishList[listNumber - 1]);
            playerController.fishList.RemoveAt(listNumber - 1);
        }
    }
    private void PreviousAquareum()         
    {
        if (playerController.GetAquareum1)
        {
            LevelController.Current.GameOver();
        }
        else if (playerController.GetAquareum2)
        {
            playerController.HarpoonBearBadEvo2();
        }
        else if (playerController.GetAquareum3)
        {
            playerController.HarpoonBearBadEvo3();
        }
        else if (playerController.GetAquareum4)
        {
            playerController.HarpoonBearBadEvo4();
        }
        else if (playerController.GetAquareum5)
        {
            playerController.HarpoonBearBadEvo5();
        }
    }      
}
