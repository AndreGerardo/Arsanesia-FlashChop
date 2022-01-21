using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Physics")]
    private Rigidbody2D rb;
    [SerializeField] private float throwForce = 1000f;

    [Header("Reference")]
    public List<GameObject> objects;
    public Transform tableSpawnpoint;
    public Transform parabolicSpawnpoint;
    

    public void SpawnObject()
    {
        int randomVal = Random.Range(0,7);

        GameObject obj =  objects[randomVal];
        obj.SetActive(true);

        if(randomVal <= 3)
        {
            obj.transform.position = tableSpawnpoint.position;
        }else
        {
            obj.transform.position = parabolicSpawnpoint.position;
            obj.GetComponent<Rigidbody2D>().AddForce((Vector2.left + Vector2.up) * throwForce * Time.deltaTime);

        }
    }


}