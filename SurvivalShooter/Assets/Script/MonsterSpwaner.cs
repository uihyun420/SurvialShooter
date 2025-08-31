using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MonsterSpwaner : LivingEntity
{
    public GameObject[] monsterPrefabs;
    public Ui ui;

    public Vector2 spawnRangeX = new Vector2(-10f, 10f);
    public Vector2 spawnRangeZ = new Vector2(-10f, 10f);
    public float spawnY = 0f;

    public float spawnInterval = 5f;

    private void Awake()
    {
        spawnInterval = 5f;
        ui.ScoreText();
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                SpawnMonster();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void SpawnMonster()
    {
        if (monsterPrefabs == null || monsterPrefabs.Length == 0) return;

        int index = Random.Range(0, monsterPrefabs.Length);
        GameObject prefab = monsterPrefabs[index];

        float spawnX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        float spawnZ = Random.Range(spawnRangeZ.x, spawnRangeZ.y);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

       Instantiate(prefab, spawnPosition, Quaternion.identity);
    }



}
