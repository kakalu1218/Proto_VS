using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectManager
{
    public PlayerController Player { get; private set; }
    public HashSet<MonsterController> Monsters { get; private set; } = new HashSet<MonsterController>();

    public T Spawn<T>(Vector3 position) where T : BaseController
    {
        Type type = typeof(T);
        if (type == typeof(PlayerController))
        {
            GameObject gameObject = Managers.Resource.Instantiate("Player.prefab");
            gameObject.transform.position = position;

            PlayerController playerController = gameObject.GetComponent<PlayerController>();
            Player = playerController;
            return playerController as T;
        }
        else if (type == typeof(MonsterController))
        {
            GameObject gameObject = Managers.Resource.Instantiate("Monster.prefab");
            gameObject.transform.position = position;

            MonsterController monsterController = gameObject.GetComponent<MonsterController>();
            Monsters.Add(monsterController);
            return monsterController as T;
        }

        return null;
    }

    public void Despawn<T>(T obj) where T : BaseController
    {
        Type type = typeof(T);
        if (type == typeof(PlayerController))
        {
        }
        else if (type == typeof(MonsterController))
        {
            Monsters.Remove(obj as MonsterController);
            Managers.Resource.Destroy(obj.gameObject);
        }
    }

    public void DespawnAllMonsters()
    {
        foreach (var monster in Monsters)
        {
            Despawn<MonsterController>(monster);
        }
    }

    public void Clear()
    {
    }
}
