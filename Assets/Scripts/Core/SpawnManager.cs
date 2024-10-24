using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : Singleton<SpawnManager>
{
    public float spawnInterval = 0.5f;
    public float mergeCoolTime = 0.5f;

    Unit0 unit0;
    Unit1 unit1;
    Unit2 unit2;
    Unit3 unit3;
    Unit4 unit4;
    Unit5 unit5;
    Unit6 unit6;
    Unit7 unit7;
    EffectPool effect;

    void Update()
    {
        mergeCoolTime -= Time.deltaTime;
    }

    //풀 초기화
    protected override void OnInitialize()
    {
        unit0 = GetComponentInChildren<Unit0>();
        if (unit0 != null)
        {
            unit0.Initialize();
        }
        unit1 = GetComponentInChildren<Unit1>();
        if (unit1 != null)
        {
            unit1.Initialize();
        }
        unit2 = GetComponentInChildren<Unit2>();
        if (unit2 != null)
        {
            unit2.Initialize();
        }
        unit3 = GetComponentInChildren<Unit3>();
        if (unit3 != null)
        {
            unit3.Initialize();
        }
        unit4 = GetComponentInChildren<Unit4>();
        if (unit4 != null)
        {
            unit4.Initialize();
        }
        unit5 = GetComponentInChildren<Unit5>();
        if (unit5 != null)
        {
            unit5.Initialize();
        }
        unit6 = GetComponentInChildren<Unit6>();
        if (unit6 != null)
        {
            unit6.Initialize();
        }
        unit7 = GetComponentInChildren<Unit7>();
        if (unit7 != null)
        {
            unit7.Initialize();
        }
        effect = GetComponentInChildren<EffectPool>();
        if (effect != null)
        {
            effect.Initialize();
        }
    }


    /// <summary>
    /// 합쳐질 때 다음레벨 유닛 생성 함수
    /// </summary>
    /// <param name="level">생성할 유닛의 레벨</param>
    /// <param name="position">생성 위치</param>
    /// <param name="angle">z축 회전각</param>
    /// <returns></returns>
    public Unit GetMerge(int level, Vector2 position)
    {
        //GameObject temp = Instantiate(units[level], position, Quaternion.Euler(0, 0, angle));
        //Unit unit = temp.GetComponent<Unit>();
        //unit.isDrop = true;


        Unit unit = null;

        switch (level)
        {
            case 0:
                unit = unit0.GetObject(position); break;
            case 1:
                unit = unit1.GetObject(position); break;
            case 2:
                unit = unit2.GetObject(position); break;
            case 3:
                unit = unit3.GetObject(position); break;
            case 4:
                unit = unit4.GetObject(position); break;
            case 5:
                unit = unit5.GetObject(position); break;
            case 6:
                unit = unit6.GetObject(position); break;
            case 7:
                unit = unit7.GetObject(position); break;
        }
        Rigidbody2D rigid = unit.GetComponent<Rigidbody2D>();
        Animator animator = unit.GetComponent<Animator>();
        rigid.simulated = true;
        rigid.AddForce(Vector2.up * 0.1f * unit.level, ForceMode2D.Impulse);
        unit.isDrop = true;
        animator.SetInteger("Lv", level);


        return unit;
    }

    public Unit GetUnit(Vector2 position, float angle)
    {
        float ran = Random.value;
        if (ran < 0.4f)         //40% 확률로 0레벨 유닛
            return unit0.GetObject(position, new Vector3(0, 0, angle));
        else if (ran < 0.7f)    //30% 확률로 1레벨 유닛
            return unit1.GetObject(position, new Vector3(0, 0, angle));
        else if (ran < 0.9f)    //20% 확률로 2레벨 유닛
            return unit2.GetObject(position, new Vector3(0, 0, angle));
        else                    //10% 확률로 3레벨 유닛
            return unit3.GetObject(position, new Vector3(0, 0, angle));         
    }

    public MergeEffect GetEffect(Vector2? position, float scale)
    {
        MergeEffect temp = effect.GetObject(position);
        temp.transform.localScale *= scale;
        return temp;
    }

    IEnumerator AutoGenerate(Vector2 position, float angle)
    {
        yield return new WaitForSeconds(spawnInterval);
        GetUnit(position, angle);
    }
    public void Generate(Vector2 position, float angle)
    {
        StartCoroutine(AutoGenerate(position, angle));
    }

    IEnumerator MergeDelayCoroutine(int level, Vector2 position)
    {
        while ( mergeCoolTime > 0)
        {
            yield return null;
        }

        if (mergeCoolTime < 0)
        {
            mergeCoolTime = 0.5f;
            yield return GetMerge(level, position);
        }
    }
    public void MergeDelay(int level, Vector2 position)
    {
        StartCoroutine (MergeDelayCoroutine(level, position));
    }
}
