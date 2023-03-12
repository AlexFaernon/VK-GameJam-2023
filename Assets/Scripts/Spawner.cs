using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private string monster;
    [SerializeField] private int maxCount;
    public static int BlevunCount;
    public static int FlyerCount;
    public static int JumperCount;
    private bool canSpawn = true;
    void Update()
    {
        if (!canSpawn) return;
        CheckMonster();
    }

    private void CheckMonster()
    {
        if (monster == "flyer" && FlyerCount < maxCount)
        {
            SpawnMonster();
        }

        if (monster == "blevun" && BlevunCount < maxCount)
        {
            SpawnMonster();
        }

        if (monster == "jumper" && JumperCount < maxCount)
        {
            SpawnMonster();
        }
    }

    private GameObject SpawnMonster()
    {
        var rect = GetComponent<RectTransform>().rect;
        var x = UnityEngine.Random.Range(-rect.width / 2, rect.width / 2);
        var y = UnityEngine.Random.Range(-rect.height / 2, rect.height / 2);

        switch (monster)
        {
            case "flyer":
                FlyerCount++;
                break;
            case "jumper":
                JumperCount++;
                break;
            case "blevun":
                BlevunCount++;
                break;
        }

        canSpawn = false;
        StartCoroutine(Delay());
        return Instantiate(prefab, new Vector3(x, y, 0), new Quaternion());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(25);
        canSpawn = true;
    }
    
    
}
