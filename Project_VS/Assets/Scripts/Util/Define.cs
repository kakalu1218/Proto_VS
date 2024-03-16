using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    public enum Scene
    { 
        None,
        Game,
    }

    public enum UIEvent
    {
        None,
        Click,
    }

    public enum ObjectType
    { 
        None,
        Player,
        Monster,
        Projectile,
    }

    public enum MonsterState
    {
        None,
        Idle,
        Moving,
        Dead,
    }

    public enum SkillTpye
    { 
        None,
    }
}
