using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test05_DropUnit : MonoBehaviour
{
    public GameObject test;
    public Transform droper;

    void Update()
    {
        Unit unit = test.GetComponent<Unit>();
        if (Input.GetKeyDown(KeyCode.Space))
            Instantiate(test, transform.position, Quaternion.identity);
        if (Input.GetKeyUp(KeyCode.Space)) {}
    }
}
