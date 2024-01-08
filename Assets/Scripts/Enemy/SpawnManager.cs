using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static float recordedDeaths;
    float timer = 0;
    float spawnTimer = 0;
    float flatbonusTimer ;
    float flatbonusCoolDown = 15;
    public GameObject basicEnemy;
    float flatbonus = 2;
    private void Update()
    {
        flatbonusTimer += Time.deltaTime;
        if(flatbonusTimer >= flatbonusCoolDown)
        {
            flatbonus++;
            flatbonusTimer = 0;
        }
        timer += Time.deltaTime;
        spawnTimer += Time.deltaTime;
        if(timer>=1)
        {
            recordedDeaths++;
            timer = 0;
        }
        if (spawnTimer>=5)
        {
            for (int i = 0; i < Mathf.Floor(flatbonus + (recordedDeaths / 2)); i++)
            {
                GameObject Player = GameObject.FindGameObjectWithTag("Player");
                float angle = (float)i / recordedDeaths * 2 * Mathf.PI;
                float offsetX = 20 * Mathf.Cos(angle);
                float offsetY = 20 * Mathf.Sin(angle);

                // Calculate spawn position
                Vector3 spawnPosition = Player.transform.position + new Vector3(offsetX, offsetY, 0f);

                // Instantiate the zombie at the calculated position
                GameObject spawnedZombie = Instantiate(basicEnemy, spawnPosition, transform.rotation);
            }
            Debug.Log("Recorded Deaths " + recordedDeaths);
            recordedDeaths = 0;
            
            spawnTimer = 0;
        }

    }


}
