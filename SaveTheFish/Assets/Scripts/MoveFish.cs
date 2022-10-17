using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFish : MonoBehaviour
{
    private float spawnRate = 0.8f;
    [SerializeField] GameObject downFish;

    void Update()
    {
        transform.Translate(Vector3.down * 20f * Time.deltaTime);
        if (downFish.transform.position.y < -6f)
        {
            downFish.transform.position = new Vector3(transform.position.x, 6.5f, transform.position.z);
            transform.Translate(Vector3.down * 13f * Time.deltaTime);
        }
    }


}
