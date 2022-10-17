using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    private Animator harpoonAnim;
    private BoxCollider collBox;
    private PlayerController playerController;
    [SerializeField] GameObject fishHarpoon;
    [SerializeField] GameObject glassBreak;
    [SerializeField] private ParticleSystem particleEffect;
    void Start()
    {
        harpoonAnim = GetComponent<Animator>();

    }
    void Update()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!playerController.GetAquareum1)
            {
                GameObject glassBreakk = Instantiate(glassBreak, other.transform.position + new Vector3(0, 0.5f, 6.5f), other.transform.rotation, this.transform);
                glassBreakk.transform.SetParent(null);
                particleEffect.Play();

            }
            //DecreaseFish();
            PreviousAquareum();
            harpoonAnim.SetTrigger("Catch");
            //fishHarpoon.SetActive(true);
            Destroy(fishHarpoon, 2f);
            Destroy(gameObject, 5f);
            collBox = GetComponent<BoxCollider>();
            collBox.enabled = false;
        }
    }

    private void DecreaseFish()             //Akvrayumdan akvrayumlar arasý balýk kapasitesi kaçsa o kadar balýk eksiltir.
    {
        for (int i = 0; i < (playerController.aquareumcapacity3 - playerController.aquareumcapacity2); i++)
        {
            int listNumber = playerController.fishList.Count;
            //playerController.fishList[listNumber - 1].transform.parent = gameObject.transform;
            Destroy(playerController.fishList[listNumber - 1]);
            playerController.fishList.RemoveAt(listNumber - 1);
        }
    }

    private void PreviousAquareum()                 //Önceki akvaryuma direk olarak dönüþü saðlar. Önceki akvaryumun maksimim kapasitesi ile baþlarsýn.
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
