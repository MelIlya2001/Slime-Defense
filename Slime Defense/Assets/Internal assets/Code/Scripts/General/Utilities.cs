using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour 
{
    public static Utilities Instance;
    [SerializeField] private GameObject limitLeftSpawnPos;
    [SerializeField] private GameObject limitRightSpawnPos;
    [SerializeField] private GameObject Start_level;
    [SerializeField] private GameObject End_level;


     public enum Elements{
            Fire,
            Water,
            Air,
            Terra
        }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public float GetRandomPosZ(){
        return UnityEngine.Random.Range(limitLeftSpawnPos.transform.position.z, limitRightSpawnPos.transform.position.z);
    }

    public float GetSlimeSpawnX() {
        return limitLeftSpawnPos.transform.position.x;
    }

    public float GetEnemySpawnX() {
        return limitRightSpawnPos.transform.position.x;
    }

    public float GetHalfZ(){
        return Math.Abs(limitLeftSpawnPos.transform.position.z - limitRightSpawnPos.transform.position.z);
    }

    public Vector3 GetEndLevel(){
        return End_level.transform.position;
    }
}
