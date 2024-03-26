using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class Castle_enemy : Abstract_enemy 
{
    

    [Serializable]
    public struct Wave{
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


/*
    
    [Serializable]
    public struct EnemyInfo
    {
        public enum EnemyType{
            Enemy_warrior,
            Enemy_archer,
            Enemy_standard_bearer,
            Enemy_mouse,
            Enemy_bomber,
            Enemy_spearman,
            Enemy_axeman,
            Enemy_battering_ram,
            Enemy_jumper,
            Enemy_shield,
            Enemy_boss,
            Enemy_necromancer,
            Enemy_blocker,
            Enemy_elementalist
        }


        public EnemyType Type;
        public GameObject Prefab;
        public int StartCount;
    }

    [Serializable]
    public struct ProjectileInfo
    {
        public enum ProjectileType{
            element_air,
            element_terra,
            element_fire,
            element_water,
            element_ice,
            arrow
        }


        public ProjectileType Type;
        public GameObject Prefab;
        public int StartCount;
    }
    
    [Serializable]
    public struct SlimeInfo
    {
        public enum SlimeType{
            T1_air,
            T1_terra,
            T1_fire,
            T1_water,
            T2_fire_fire,
            T2_fire_terra,
            T2_fire_water,
            T2_fire_air,
            T2_air_air,
            T2_air_terra,
            T2_air_water,
            T2_terra_water,
            T2_terra_terra,
            T2_water_water            
        }
        

        public SlimeType Type;
        public GameObject Prefab;
        public int StartCount;
    }
    
    
    
    [Header ("Enemies")]
    [SerializeField] private List<EnemyInfo> enemiesInfo;
    private Dictionary<EnemyInfo.EnemyType, ObjectPool<GameObject>> enemy_pools;

    [Header ("Projectiles objects")]
    [SerializeField] private List<ProjectileInfo> projectilesInfo;
    private Dictionary<ProjectileInfo.ProjectileType, Pool> projectile_pools;

    [Header ("Available slimes")]
    [SerializeField] private List<SlimeInfo> slimesInfo;
    private Dictionary<SlimeInfo.SlimeType, Pool> slime_pools;


    


  private void Awake()
    {

        //инициализируем пуллы объектов. Для удобства работы разбиты на 3 пулла по типам объекта: Враги, Слаймы, Снаряды. 
        if (Instance == null)
            Instance = this;
        
        enemy_pools = new Dictionary<EnemyInfo.EnemyType, ObjectPool<GameObject>>();
        projectile_pools = new Dictionary<ProjectileInfo.ProjectileType, Pool>();
        slime_pools = new Dictionary<SlimeInfo.SlimeType, Pool>();
        InitPool();

    }


    //Методы для работы с пуллами объектов. Из-за желания разделить пулл на 3 части по типам объектов пришлось 
    //прибегнуть к костылям в виде dynamic типа данных, т.к. у каждого пулла свой enum
    private void InitPool(){
        GameObject empty = new GameObject();
        
        foreach (var obj in enemiesInfo){
            GameObject container = Instantiate(empty, transform.parent, false);
            container.name = obj.Type.ToString();
            

            enemy_pools[obj.Type] = new ObjectPool<GameObject>(() => {
                Debug.Log("1");
                return Instantiate(obj.Prefab, container.transform);     
            },  prefab => {
                Debug.Log("2");
                prefab.SetActive(true);                                                         //OnGet function
            }, prefab => {
                Debug.Log("3");
                prefab.SetActive(false);                                                        //OnRelease()
            },  prefab => {
                Debug.Log("4");
                Destroy(prefab);                                                                //OnDelete()        
            },  false, obj.StartCount, obj.StartCount + 10);

            
        }   
        Destroy(empty);
    }
*/
  
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
                timer_wave = waves[current_wave].delay_seconds_to_start;
            }

            if (timer_summon < 0)
            {
                PoolControl.Instance.GetObject(waves[current_wave].enemyInfos[0].enemyType, PoolControl.Instance.enemy_pools);
                waves[current_wave].enemyInfos.RemoveAt(0);
                timer_summon = waves[current_wave].enemyInfos[0].delay_seconds_to_summon;
            } else{ 
                timer_summon -= Time.fixedDeltaTime;
            }
        }
        
    }
    
}


