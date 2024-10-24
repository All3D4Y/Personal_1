using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Unit : RecycleObject
{
    Animator animator;
    Transform droperPos;
    Rigidbody2D rigid;

    /// <summary>
    /// 클릭하면 마우스 위치로 유닛을 고정하기 위한 bool타입 변수
    /// </summary>
    public bool isClick = false;
    /// <summary>
    /// 떨어트린 유닛을 구분하기 위한 bool타입 변수
    /// </summary>
    public bool isDrop = false;

    /// <summary>
    /// Merge에 딜레이
    /// </summary>
    public bool canMerge = false;
    public float mergeCoolTime = 0.2f;

    public int level = 0;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        droperPos = GameManager.Instance.DroperBase.transform;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        isClick = false;
        isDrop = false;
        canMerge = false;
        StartCoroutine(SetMergeAvailable());    //활성화 되고 0.2초 뒤 merge 가능
    }

    protected override void OnReset()
    {
        if (droperPos == null)
        {
            droperPos = GameManager.Instance.DeadLineBase.transform;
        }
    }
    void Update()
    {
        OnMoveUpdate();
        mergeCoolTime -= Time.deltaTime;
    }

    /// <summary>
    /// 유닛 드래그앤 드랍
    /// </summary>
    void OnMoveUpdate()
    {
        //클릭했고, 떨어트린 유닛이 아니라면
        if (isDrop)
        {
            return;
        }
        else if (isClick && !isDrop)
        {
            transform.position = droperPos.position;    //마우스 위치에 유닛 고정
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Unit"))
        {
            Unit other = collision.gameObject.GetComponent<Unit>();     //충돌 대상의 Unit컴포넌트 가져오기
            Vector2 otherPos = other.gameObject.transform.position;     //충돌 대상의 위치벡터
            Vector2 mergePos = 0.5f * (otherPos + (Vector2)transform.position);

            //충돌 대상의 레벨과 자신의 레벨이 같고, 둘 다 합체 가능한 상태면 오른쪽 아래에 있는 유닛에서만 충돌코드 실행
            if (mergeCoolTime < 0 && level == other.level && canMerge && other.canMerge && (transform.position.x > otherPos.x || transform.position.y < otherPos.y))
            { 
                mergeCoolTime = 0.2f;
                
                Merge(other, mergePos);
            }
        }
    }

    /// <summary>
    /// 활성화 되고 0.2초 후 부터 합쳐질 수 있게 하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator SetMergeAvailable()
    {
        yield return new WaitForSeconds(0.2f);
        canMerge = true;
    }
    void ScoreUp(int score)
    {
        GameManager.Instance.AddScore(score);
    }

    void Merge(Unit other, Vector2 position)
    {
        int temp = level;
        canMerge = false;
        other.canMerge = false;
        other.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        SpawnManager.Instance.GetEffect(position, temp + 1);

        if (level < 7)                       //6레벨 까지는
        {
            SpawnManager.Instance.GetMerge(temp + 1, position);    //다음유닛 생성
            ScoreUp(10 * (temp + 1));
        }
        else                                 //7레벨은 점수만
        {
            //점수++
            ScoreUp(1000);
        }
    }
}
