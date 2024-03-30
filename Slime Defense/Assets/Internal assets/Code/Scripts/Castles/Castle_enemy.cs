using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class Castle_enemy : Abstract_enemy 
{
    [Header ("Starts_spawn_positions")]
    [SerializeField] private List<PoolControl.EnemyType> fly_enemies; 
    [SerializeField] private float walking_enemy_posY;
    [SerializeField] private float flying_enemy_posY;
    
    [Serializable]
    public struct Wave{
        [Serializable]
        public struct WaveInfo{
            public uint delay_seconds_to_summon;
            public PoolControl.EnemyType enemyType;
        }
        
        public float delay_seconds_to_start;      
        public List<WaveInfo> enemyInfos;
    } 


    [Header ("Spawn of enemy")]
    [SerializeField] private List<Wave> waves;
    private float timer_wave;
    private float timer_summon;
    private int current_wave;


    void Start(){
        timer_wave = waves[current_wave].delay_seconds_to_start;
        
    }

    void FixedUpdate(){
        if (current_wave >= waves.Count) return;
        
        if (timer_wave >= 0){
            timer_wave -= Time.fixedDeltaTime;
        } else {
            if (waves[current_wave].enemyInfos.Count <= 0) {
                current_wave++;
                timer_wave = current_wave < waves.Count ? waves[current_wave].delay_seconds_to_start : 0;
            } 
            else if (timer_summon < 0){
                GameObject enemy = PoolControl.Instance.GetObject(waves[current_wave].enemyInfos[0].enemyType, PoolControl.Instance.enemy_pools);

                //start_position
                var enemy_posY = fly_enemies.Contains(waves[current_wave].enemyInfos[0].enemyType) ? flying_enemy_posY : walking_enemy_posY;
                enemy.transform.position = new Vector3(transform.position.x, enemy_posY, transform.position.z);
                Debug.Log(enemy.transform.position);

                waves[current_wave].enemyInfos.RemoveAt(0);
                timer_summon = waves[current_wave].enemyInfos.Count > 0 ? waves[current_wave].enemyInfos[0].delay_seconds_to_summon : 0;

            } 
            else{ 
                timer_summon -= Time.fixedDeltaTime;
            }
        }
        
    }
    

    public override void TakeDamage(float damage, string[] elements){
        hp -= damage;
        if (hp <= 0){
            Menu_scripts.Instance.GameOver(true);
        }
    }
}


