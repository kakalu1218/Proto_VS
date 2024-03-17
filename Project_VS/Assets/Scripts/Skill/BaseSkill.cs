using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public int Damage { get; protected set; } = 1;
    private float _coolTime = 1.0f;

    public void ActivateSkill()
    {
        StartCoroutine(CoStartSkill());
    }

    private IEnumerator CoStartSkill()
    {
        while (true)
        {
            DoSkillJob();
            yield return new WaitForSeconds(_coolTime);
        }
    }

    protected abstract void DoSkillJob();

    protected virtual void GenerateProjectile(BasePawnController owner, Vector3 startPos, Vector3 moveDir)
    {
        ProjectileController projectileController = Managers.Object.Spawn<ProjectileController>(startPos);
        projectileController.SetInfo(owner, moveDir, Damage);
    }
}
