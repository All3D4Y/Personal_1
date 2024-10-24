using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test07_SpawnManager : MonoBehaviour
{
    public GameObject prefab;
    public GameObject droper;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(prefab, droper.transform.position, Quaternion.identity);
        }
    }
}
