using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    [Tooltip("몬스터 리스폰 주기")]
    [SerializeField] private float _spawnInterval = 1.0f;
    [Tooltip("몬스터 최대 개수")]
    [SerializeField] private int _maxMonsterCount = 100;

    protected override void Init()
    {
        base.Init();

        Managers.Resource.Instantiate("Map.prefab");
        Managers.Object.Spawn<PlayerController>(Vector3.zero);

        StartCoroutine(CoUpdateSpawningPool());
    }

    private IEnumerator CoUpdateSpawningPool()
    {
        while (true)
        {
            TrySpawn();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void TrySpawn()
    {
        int monsterCount = Managers.Object.Monsters.Count;
        if (monsterCount >= _maxMonsterCount)
        { 
            return;
        }

        Vector3 randPos = Util.GenerateMonsterSpawnPosition(Managers.Object.Player.transform.position, 10, 15);
        Managers.Object.Spawn<MonsterController>(randPos);
    }

    public override void Clear()
    {
    }
}
