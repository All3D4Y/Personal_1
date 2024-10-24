using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Droper : MonoBehaviour
{
    [Header("드로퍼 가로세로 제한")]
    [SerializeField] float maxX = 2.5f;
    [SerializeField] float minX = -2.5f;
    [SerializeField] float fixedY = 4.0f;

    Camera cam;


    public float clickCool = 1.0f;
    public float dropCool = 1.05f;
    public bool canClick = false;
    public bool canDrop = false;

    Vector2 spawnPos = new Vector2(0, 4.0f);

    Unit unit = null;
    Rigidbody2D rb = null;

    void Awake()
    {
        cam = Camera.main;
    }

    void Start()
    {
        unit = SpawnManager.Instance.GetUnit(spawnPos, 360.0f * Random.value);
        rb = unit.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 드로퍼의 이동 구현
    /// </summary>
    void Update()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //커서 위치를 월드좌표로 변환
        mousePos.y = fixedY; //y축 높이 고정

        if (mousePos.x < minX)
            mousePos.x = minX;  //화면 왼쪽 제한
        else if (mousePos.x > maxX)
            mousePos.x = maxX;  //화면 오른쪽 제한
        else
            transform.position = mousePos;  //오브젝트 위치 = 커서 위치

        clickCool -= Time.deltaTime;
        if (clickCool < 0)
            canClick = true;
        else canClick = false;
        dropCool -= Time.deltaTime;
        if (dropCool < 0)
            canDrop = true;
        else canDrop = false;
    }
    /// <summary>
    /// 마우스 좌클릭 누를 때 실행되는 함수
    /// </summary>
    public void OnClickDown()
    {
        if (canClick)
        {
            StopAllCoroutines();
            unit.isClick = true;
            clickCool = 1.0f;
            //Debug.Log("LMB Down");
        }
    }
    /// <summary>
    /// 마우스 좌클릭 뗄 때 실행되는 함수
    /// </summary>
    public void OnClickUp()
    {
        if (canDrop)
        {
            SetUnitBool(unit, rb);
            dropCool = 1.05f;
            //Debug.Log("LMB Up");
            //unit = null;
            StartCoroutine(GetUnitWithDelay(0.5f));
        }
    }
    void SetUnitBool(Unit unit, Rigidbody2D rb)
    {
        unit.isDrop = true;
        unit.isClick = false;
        rb.simulated = true;
    }

    IEnumerator GetUnitWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        unit = SpawnManager.Instance.GetUnit(spawnPos, 360.0f * Random.value);
        rb = unit.GetComponent<Rigidbody2D>();
        unit.isDrop = false;
        unit.isClick = false;
        unit.canMerge = false;
        rb.simulated = false;
    }
}
