using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test09_CombineUnit : TestBase
{
    public GameObject[] units;

    public Transform droper;


    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Instantiate(units[0], transform.position, Quaternion.identity);
    }
    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Instantiate(units[1], transform.position, Quaternion.identity);
    }
    protected override void OnTest3(InputAction.CallbackContext context)
    {
        Instantiate(units[2], transform.position, Quaternion.identity);
    }
    protected override void OnTest4(InputAction.CallbackContext context)
    {
        Instantiate(units[3], transform.position, Quaternion.identity);
    }
}
