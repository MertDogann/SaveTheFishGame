using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public static PlayerController Current;

    [SerializeField] float limitXLeft;
    [SerializeField] float limitXRight;
    public float xSpeed;
    public float runningSpeed;
    private float currentSpeed;
    private Touch theTouch;
    private Vector2 touchStartPosition, touchEndPosition;
    
    public int aquareumcapacity1, aquareumcapacity2, aquareumcapacity3, aquareumcapacity4, aquareumcapacity5;
    [SerializeField] GameObject[] aquareumPrefabs;
    [System.NonSerialized] public List<GameObject> fishList = new List<GameObject>();
    private bool aquareum1 = true;
    private bool aquareum2, aquareum3, aquareum4, aquareum5;
    private bool bounceFish = false;


    [System.NonSerialized] public bool finished;
    [SerializeField] ParticleSystem evolutionParticle;

    public bool GetBounceFish
    {
        get
        {
            return bounceFish;
        }
        set
        {
            bounceFish = value;
        }
    }
    public bool GetAquareum1
    {
        get
        {
            return aquareum1;
        }set
        {
            aquareum1 = value;
        }
    }
    public bool GetAquareum2
    {
        get
        {
            return aquareum2;

        }
        set
        {
            aquareum2 = value;
        }
    }
    public bool GetAquareum3
    {
        get
        {
            return aquareum3;
        }set
        {
            aquareum3 = value;
        }
       
    }
    public bool GetAquareum4
    {
        get
        {
            return aquareum4;
        }set
        {
            aquareum4 = value;
        }
    }
    public bool GetAquareum5
    {
        get
        {
            return aquareum5;
        }
        set
        {
            aquareum5 = value;
        }
    }

    void Start()
    {
        Current = this;
        currentSpeed = runningSpeed;
    }
    void Update()
    {
        if (LevelController.Current == null || !LevelController.Current.gameActive)
        {
            return;
        }
        if (finished && fishList.Count<=0) 
        {
            LevelController.Current.FinishGame();
        }
        FinishSlowCamera();

        Move();

        AquareumGoodEvolution();

        AquareumBadEvolution();

    }



    private void OnTriggerStay(Collider other)
    {
        if (LevelController.Current == null || !LevelController.Current.gameActive)
        {
            return;
        }

        
        if (other.gameObject.CompareTag("FinishEnd"))
        {
            LevelController.Current.FinishGame();

        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Finish"))
        {
            finished = true;
        }

        if (other.gameObject.CompareTag("Collect") )
        {
            CollectFishStack(other.gameObject);

        }

    }

    private void CollectFishStack(GameObject other)
    {
        if (aquareum1)
        {
            float randomFishPosition = Random.Range(0.1f, 1.2f);
            float randomFishPositionZ = Random.Range(-0.2f, 0f);
            Vector3 startFishDistance = new Vector3(0, randomFishPosition, randomFishPositionZ);
            other.gameObject.SetActive(true);
            other.gameObject.transform.position = transform.position + startFishDistance;
        }
        else if (aquareum2)
        {
            float randomFishPosition = Random.Range(0.2f, 1.2f);
            float randomFishPositionZ = Random.Range(0.2f, 0.8f);
            Vector3 startFishDistance = new Vector3(0, randomFishPosition, randomFishPositionZ);
            other.gameObject.transform.position = transform.position + startFishDistance;
        }
        else if (aquareum3)
        {
            float randomFishPositionY = Random.Range(0.5f, 1.6f);
            float randomFishPositionX = Random.Range(-1.8f, 1.0f);
            float randomFishPositionZ = Random.Range(0.1f, 1f);
            Vector3 startFishDistance = new Vector3(randomFishPositionX, randomFishPositionY, randomFishPositionZ);
            other.gameObject.transform.position = transform.position + startFishDistance;
        }
        else if (aquareum4)
        {
            float randomFishPositionY = Random.Range(0.2f, 2f);
            float randomFishPositionX = Random.Range(-1.8f, 2f);
            float randomFishPositionZ = Random.Range(0.1f, 1.2f);
            Vector3 startFishDistance = new Vector3(randomFishPositionX, randomFishPositionY, randomFishPositionZ);
            other.gameObject.transform.position = transform.position + startFishDistance;
        }
        else if (aquareum5)
        {
            float randomFishPositionY = Random.Range(0.2f, 3f);
            float randomFishPositionX = Random.Range(-1f, 0.5f);
            float randomFishPositionZ = Random.Range(0.1f, 1.4f);
            Vector3 startFishDistance = new Vector3(randomFishPositionX, randomFishPositionY, randomFishPositionZ);
            other.gameObject.transform.position = transform.position + startFishDistance;
        }

        other.gameObject.transform.parent = gameObject.transform;
        other.gameObject.transform.localScale -= new Vector3(1f, 1f, 1f);

        fishList.Add(other.gameObject);
    }

    private void FinishSlowCamera()
    {
        if (finished)      
        {
            transform.DOMoveX(0, 0.2f);
            currentSpeed = 13f;
        }
    }
    private void Move()
    {
        float touchXDelta = 0;
        if (Input.touchCount > 0 && !finished)           
        {
            theTouch = Input.GetTouch(0);
            if (theTouch.phase == TouchPhase.Began)
            {
                touchXDelta = Input.GetTouch(0).deltaPosition.x / Screen.width;
                touchStartPosition = theTouch.position;
            }

            else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
            {
                touchXDelta = Input.GetTouch(0).deltaPosition.x / Screen.width;
                touchEndPosition = theTouch.position;
                float x = touchEndPosition.x - touchStartPosition.x;
                float y = touchEndPosition.y - touchStartPosition.y;

            }
        }

        float newX = 0;
        newX = transform.position.x + xSpeed * touchXDelta * Time.deltaTime;


        newX = Mathf.Clamp(newX, -limitXLeft, limitXRight);
        Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z + currentSpeed * Time.deltaTime);
        transform.position = newPosition;

    }

    private void AquareumGoodEvolution()
    {
        if (fishList.Count > aquareumcapacity1 && aquareum1)        
        {
            Evolotion2();

        }
        else if (fishList.Count > aquareumcapacity2 && aquareum2)
        {
            Evolotion3();
        }
        else if (fishList.Count > aquareumcapacity3 && aquareum3)
        {
            Evolotion4();
        }
        else if (fishList.Count > aquareumcapacity4 && aquareum4)
        {
            Evolotion5();
        }
    }

    private void AquareumBadEvolution()
    {
        if (fishList.Count <= aquareumcapacity1 && aquareum2 && !finished)      
        {
            Evolotion2Bad();

        }
        else if (fishList.Count <= aquareumcapacity2 && aquareum3 && !finished)
        {
            Evolotion3Bad();

        }
        else if (fishList.Count <= aquareumcapacity3 && aquareum4 && !finished)
        {
            Evolotion4Bad();

        }
        else if (fishList.Count <= aquareumcapacity4 && aquareum5 && !finished)
        {
            Evolotion5Bad();
        }
    }

    public void Die()
    {
        Camera.main.transform.SetParent(null);

    }


    private void Evolotion2()
    {
        aquareumPrefabs[0].SetActive(false);
        aquareumPrefabs[1].SetActive(true);
        limitXLeft = 3.8f;
        limitXRight = 4.4f;
        aquareum2 = true;
        aquareum1 = false;
        evolutionParticle.Play();
        LevelController.Current.ChanceAquareumCapacity(aquareumcapacity2);
        
    }
    private void Evolotion3()
    {
        aquareumPrefabs[1].SetActive(false);
        aquareumPrefabs[2].SetActive(true);
        
        limitXRight = 4f;
        aquareum3 = true;
        aquareum2 = false;
        evolutionParticle.Play();
        LevelController.Current.ChanceAquareumCapacity(aquareumcapacity3);
        
    }
    private void Evolotion4()
    {
        aquareumPrefabs[2].SetActive(false);
        aquareumPrefabs[3].SetActive(true);
        limitXLeft = 3.8f;
        limitXRight = 3.65f;
        aquareum4 = true;
        aquareum3 = false;
        evolutionParticle.Play();
        LevelController.Current.ChanceAquareumCapacity(aquareumcapacity4);
    }
    private void Evolotion5()
    {
        aquareumPrefabs[3].SetActive(false);
        aquareumPrefabs[4].SetActive(true);
        aquareum5 = true;
        aquareum4 = false;
        evolutionParticle.Play();

        LevelController.Current.ChanceAquareumCapacity(aquareumcapacity5);
        
    }

    public void Evolotion2Bad()
    {
        aquareumPrefabs[1].SetActive(false);
        aquareumPrefabs[0].SetActive(true);
        aquareum1 = true;
        aquareum2 = false;
        LevelController.Current.ChanceAquareumCapacity(aquareumcapacity1);
    }
    public void Evolotion3Bad()
    {
        aquareumPrefabs[2].SetActive(false);
        aquareumPrefabs[1].SetActive(true);
        aquareum2 = true;
        aquareum3 = false;
        LevelController.Current.ChanceAquareumCapacity(aquareumcapacity2);
    }

    public void Evolotion4Bad()
    {
        aquareumPrefabs[3].SetActive(false);
        aquareumPrefabs[2].SetActive(true);
        aquareum3 = true;
        aquareum4 = false;
        LevelController.Current.ChanceAquareumCapacity(aquareumcapacity3);
    }

    public void Evolotion5Bad()
    {
        aquareumPrefabs[4].SetActive(false);
        aquareumPrefabs[3].SetActive(true);
        aquareum4 = true;
        aquareum5 = false;
        LevelController.Current.ChanceAquareumCapacity(aquareumcapacity4);
    }


    public void HarpoonBearBadEvo2()
    {
        aquareumPrefabs[1].SetActive(false);
        aquareumPrefabs[0].SetActive(true);
        BearHarpoonEvo();
        LevelController.Current.ChanceAquareumCapacity(aquareumcapacity1);

        aquareum1 = true;
        aquareum2 = false;

    }

    public void HarpoonBearBadEvo3()
    {
        aquareumPrefabs[2].SetActive(false);
        aquareumPrefabs[1].SetActive(true);
        BearHarpoonEvo();
        aquareum2 = true;
        aquareum3 = false;
        LevelController.Current.ChanceAquareumCapacity(aquareumcapacity2);
    }
    public void HarpoonBearBadEvo4()
    {
        aquareumPrefabs[4].SetActive(false);
        aquareumPrefabs[3].SetActive(true);
        BearHarpoonEvo();
        aquareum4 = true;
        aquareum5 = false;
        LevelController.Current.ChanceAquareumCapacity(aquareumcapacity3);
    }
    public void HarpoonBearBadEvo5()
    {
        aquareumPrefabs[4].SetActive(false);
        aquareumPrefabs[3].SetActive(true);
        BearHarpoonEvo();
        aquareum4 = true;
        aquareum5 = false;
        LevelController.Current.ChanceAquareumCapacity(aquareumcapacity4);
    }

    public void BearHarpoonEvo()           
    {
        int listNumber = fishList.Count;   
        int allAquareumCapacity = 0;
        if (aquareum2)
        {
            allAquareumCapacity = aquareumcapacity1;
        }
        else if (aquareum3)
        {
            allAquareumCapacity = aquareumcapacity2;
        }
        else if (aquareum4)
        {
            allAquareumCapacity = aquareumcapacity3;
        }
        else if (aquareum5)
        {
            allAquareumCapacity = aquareumcapacity4;
        }
        int extraFish = listNumber - allAquareumCapacity;
        for (int i = 0; i < extraFish; i++)
        {
            Destroy(fishList[listNumber - extraFish]);
            fishList.RemoveAt(listNumber - extraFish);

        }
    }
}





