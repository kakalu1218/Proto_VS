using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager
{
    public PlayerController Player { get; private set; }
    public HashSet<MonsterController> Monsters { get; private set; } = new HashSet<MonsterController>();
    public HashSet<ProjectileController> Projectiles { get; private set; } = new HashSet<ProjectileController>();
    public HashSet<GemController> Gems { get; } = new HashSet<GemController>();

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
        else if (type == typeof(ProjectileController))
        {
            GameObject gameObject = Managers.Resource.Instantiate("Bullet.prefab");
            gameObject.transform.position = position;

            ProjectileController projectileController = gameObject.GetComponent<ProjectileController>();
            Projectiles.Add(projectileController);
            return projectileController as T;
        }
        else if (type == typeof(GemController))
        {
            GameObject gameObject = Managers.Resource.Instantiate("Gem.prefab");
            gameObject.transform.position = position;

            GemController gemController = gameObject.GetComponent<GemController>();
            Gems.Add(gemController);

            // Grid에 추가
            Managers.Grid.Add(gameObject);

            return gemController as T;
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
        else if (type == typeof(ProjectileController))
        {
            Projectiles.Remove(obj as ProjectileController);
            Managers.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(GemController))
        {
            Gems.Remove(obj as GemController);
            Managers.Resource.Destroy(obj.gameObject);

            // Grid에 삭제
            Managers.Grid.Remove(obj.gameObject);
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
