using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test06_Unit : MonoBehaviour
{
    public GameObject prefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(prefab, new Vector3(0, 4, 0), Quaternion.identity);
        }
    }
}
