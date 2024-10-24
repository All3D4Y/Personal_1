using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeEffect : RecycleObject
{
    public float disableTime;

    ParticleSystem effect;

    void Awake()
    {
        effect = GetComponent<ParticleSystem>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        transform.localScale = 0.5f * Vector3.one;
        effect.Play();
        StartCoroutine(SelfDisable());
    }

    IEnumerator SelfDisable()
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.SetActive(false);
    }
}
