using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle_enemy : Abstract_enemy
{
    
    public static Castle_enemy Instance;

    [Header ("Spawn of enemy")]
    [SerializeField] private byte count_waves;
    [SerializeField] private uint delay_spawn_wave;



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
    private Dictionary<EnemyInfo.EnemyType, Pool> enemy_pools;

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
        
        enemy_pools = new Dictionary<EnemyInfo.EnemyType, Pool>();
        projectile_pools = new Dictionary<ProjectileInfo.ProjectileType, Pool>();
        slime_pools = new Dictionary<SlimeInfo.SlimeType, Pool>();
        InitEnemyPool();
        InitProjectilePool();
        InitSlimePool();
    }


    //Методы для работы с пуллами объектов. Из-за желания разделить пулл на 3 части по типам объектов пришлось 
    //прибегнуть к костылям в виде dynamic типа данных, т.к. у каждого пулла свой enum
    private void InitEnemyPool(){
        GameObject empty = new GameObject();

        foreach (var obj in enemiesInfo){
            GameObject container = Instantiate(empty, transform, false);
            container.name = obj.Type.ToString();

            enemy_pools[obj.Type] = new Pool(container.transform);

            for (int i = 0; i < obj.StartCount; i++){
                GameObject in_obj = InstantiateEnemy(obj.Type, container.transform);
                enemy_pools[obj.Type].Objects.Enqueue(in_obj);
            }
        }

        Destroy(empty);

    }

    private GameObject InstantiateEnemy(EnemyInfo.EnemyType type, Transform parent){
        GameObject in_obj = Instantiate(enemiesInfo.Find(x => x.Type == type).Prefab, parent);
        in_obj.SetActive(false);
        return in_obj;
    }

    public GameObject GetEnemy(EnemyInfo.EnemyType type){
        GameObject obj = enemy_pools[type].Objects.Count > 0 ?
            enemy_pools[type].Objects.Dequeue() : InstantiateEnemy(type, enemy_pools[type].Container);

        obj.SetActive(true);
        return obj;
    }

    public void DestroyEnemy(GameObject obj){
        enemy_pools[obj.GetComponent<Abstract_minion>().type].Objects.Enqueue(obj);
        obj.SetActive(false);
    }













    private void InitProjectilePool(){
        GameObject empty = new GameObject();

        foreach (var obj in projectilesInfo){
            GameObject container = Instantiate(empty, transform, false);
            container.name = obj.Type.ToString();

            projectile_pools[obj.Type] = new Pool(container.transform);

            for (int i = 0; i < obj.StartCount; i++){
                GameObject in_obj = InstantiateProjectile(obj.Type, container.transform);
                projectile_pools[obj.Type].Objects.Enqueue(in_obj);
            }
        }

        Destroy(empty);

    }

    private GameObject InstantiateProjectile(ProjectileInfo.ProjectileType type, Transform parent){
        GameObject in_obj = Instantiate(projectilesInfo.Find(x => x.Type == type).Prefab, parent);
        in_obj.SetActive(false);
        return in_obj;
    }

    public GameObject GetProjectile(ProjectileInfo.ProjectileType type){
        GameObject obj = projectile_pools[type].Objects.Count > 0 ?
            projectile_pools[type].Objects.Dequeue() : InstantiateProjectile(type, projectile_pools[type].Container);

        obj.SetActive(true);
        return obj;
    }

     public void DestroyProjectile(GameObject obj){
        projectile_pools[obj.GetComponent<Abstract_projectile>().type].Objects.Enqueue(obj);
        obj.SetActive(false);
    }















    private void InitSlimePool(){
        GameObject empty = new GameObject();

        foreach (var obj in slimesInfo){
            GameObject container = Instantiate(empty, transform, false);
            container.name = obj.Type.ToString();

            slime_pools[obj.Type] = new Pool(container.transform);

            for (int i = 0; i < obj.StartCount; i++){
                GameObject in_obj = InstantiateSlime(obj.Type, container.transform);
                slime_pools[obj.Type].Objects.Enqueue(in_obj);
            }
        }

        Destroy(empty);

    }

    private GameObject InstantiateSlime(SlimeInfo.SlimeType type, Transform parent){
        GameObject in_obj = Instantiate(slimesInfo.Find(x => x.Type == type).Prefab, parent);
        in_obj.SetActive(false);
        return in_obj;
    }

    public GameObject GetSlime(SlimeInfo.SlimeType type){
        GameObject obj = slime_pools[type].Objects.Count > 0 ?
            slime_pools[type].Objects.Dequeue() : InstantiateSlime(type, slime_pools[type].Container);

        obj.SetActive(true);
        return obj;
    }


    public void DestroySlime(GameObject obj){
        slime_pools[obj.GetComponent<Abstract_Slime>().type].Objects.Enqueue(obj);
        obj.SetActive(false);
    }

   
}
