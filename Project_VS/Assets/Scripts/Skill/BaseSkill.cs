using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public BasePawnController Owner { get; protected set; }
    public int Damage { get; protected set; } = 100;
    public float CoolTime { get; protected set; } = 1.0f;

    public void ActivateSkill()
    {
        StartCoroutine(CoStartSkill());
    }

    private IEnumerator CoStartSkill()
    {
        while (true)
        {
            DoSkillJob();
            yield return new WaitForSeconds(CoolTime);
        }
    }

    protected abstract void DoSkillJob();

    protected virtual void GenerateProjectile(BasePawnController owner, Vector3 startPos, Vector3 dir)
    {
        ProjectileController projectileController = Managers.Object.Spawn<ProjectileController>(startPos);
        projectileController.SetInfo(owner, dir);
    }
}
