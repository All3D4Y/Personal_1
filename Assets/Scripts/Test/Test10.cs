using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Test10 : TestBase
{
    public Transform droper;
    protected override void OnTest1(InputAction.CallbackContext context)
    {
        SpawnManager.Instance.GetUnit(droper.position, 360.0f * Random.value);
    }
    protected override void OnTest2(InputAction.CallbackContext context)
    {
        GameManager.Instance.AddScore(10);
    }
}
