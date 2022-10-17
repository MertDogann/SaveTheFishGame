using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAnimations : MonoBehaviour
{
    public Animator fishAnim;
    private PlayerController playerController;
    private Vector3 swimmingUpRight = new Vector3(0,0,0);
    [SerializeField] GameObject lotus;
    void Start()
    {
        fishAnim = GetComponent<Animator>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerController.GetBounceFish)
        {
            fishAnim.SetBool("Bounce", true);
        }
        if (transform.position.y < -10f) 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.rotation = Quaternion.Euler(swimmingUpRight);
            fishAnim.SetBool("Swimming", true);
            Destroy(lotus);
        }
    }
}
