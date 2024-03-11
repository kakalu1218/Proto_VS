using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    public enum Scene
    { 
        None,
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
    }

    public enum MonsterState
    {
        None,
        Idle,
        Moving,
        Dead,
    }
}
