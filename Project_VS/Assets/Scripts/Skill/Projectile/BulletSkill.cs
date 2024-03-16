using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSkill : BaseSkill
{
    protected override void DoSkillJob()
    {
        if (Managers.Object.Player == null)
        {
            Debug.Log("No");
            return;
        }

        Debug.Log("Fire");
        Vector3 spawnPos = Managers.Object.Player.transform.position;

        GenerateProjectile(Managers.Object.Player, spawnPos, Vector3.left);
    }
}
