using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
     public static PoolControl Instance;



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

    public enum ProjectileType{
            element_air,
            element_terra,
            element_fire,
            element_water,
            element_ice,
            arrow
        }

    [Serializable]
    public enum SlimeType{
            None,
            T1_air = 1,
            T1_terra = 2,
            T1_fire = 3,
            T1_water = 4,
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
    
    [Serializable]
    public class ObjectPool<T> where T : Enum
    {
        public T Type;
        public GameObject Prefab;
        public int StartCount;
    }
    

    [Header ("Enemies")]
    [SerializeField] private List<ObjectPool<EnemyType>> enemiesInfo;
    public Dictionary<Enum, Pool> enemy_pools;

    [Header ("Projectiles objects")]
    [SerializeField] private List<ObjectPool<ProjectileType>> projectilesInfo;
    public Dictionary<Enum, Pool> projectile_pools;

    [Header ("Available slimes")]
    [SerializeField] private List<ObjectPool<SlimeType>> slimesInfo;
    public Dictionary<Enum, Pool> slime_pools;


    private void Awake()
    {

        //инициализируем пуллы объектов. Для удобства работы разбиты на 3 пулла по типам объекта: Враги, Слаймы, Снаряды. 
        if (Instance == null)
            Instance = this;
        
        enemy_pools = new Dictionary<Enum, Pool>();
        projectile_pools = new Dictionary<Enum, Pool>();
        slime_pools = new Dictionary<Enum, Pool>();
        InitPool(enemiesInfo, enemy_pools, nameof(enemy_pools));
        InitPool(projectilesInfo, projectile_pools, nameof(projectile_pools));
        InitPool(slimesInfo, slime_pools, nameof(slime_pools));
    }



    private void InitPool<T>(List<ObjectPool<T>> list, Dictionary<Enum, Pool> pools, String nameOfContainer) where T : Enum{
        GameObject empty = new GameObject();

        //создание родительского контейнера для пуллов одного типа объектов
        GameObject parentContainer = Instantiate(empty, transform.parent, false);
        parentContainer.name = nameOfContainer;


        foreach (ObjectPool<T> obj in list){
            GameObject container = Instantiate(empty, parentContainer.transform, false);
            container.name = obj.Type.ToString();

            pools[obj.Type] = new Pool(container.transform);

            for (int i = 0; i < obj.StartCount; i++){
                GameObject in_obj = InstantiateObject(obj.Type, container.transform);
                pools[obj.Type].Objects.Enqueue(in_obj);
            }
        }

        Destroy(empty);

    }

    private GameObject InstantiateObject<T>(T type, Transform parent) where T : Enum{
        GameObject in_obj;
        switch(type.GetType().ToString()){         
            case "PoolControl+SlimeType":
                in_obj = Instantiate(slimesInfo.Find(x => x.Type.Equals(type)).Prefab, parent);
                in_obj.transform.position = new Vector3(0,0,50);        //костыль, чтобы при одновременном суммоне врагов и слаймов не домажились 
                break;

            case "PoolControl+ProjectileType":
                in_obj = Instantiate(projectilesInfo.Find(x => x.Type.Equals(type)).Prefab, parent);
                in_obj.transform.position = new Vector3(0,0,-50);   //костыль,
                break;

            case "PoolControl+EnemyType":
            default:
                in_obj = Instantiate(enemiesInfo.Find(x => x.Type.Equals(type)).Prefab, parent);
                in_obj.transform.position = new Vector3(0,50,50); 
                break;
        }
        in_obj.SetActive(false);
        return in_obj;
    }

    public GameObject GetObject<T>(T type, Dictionary<Enum, Pool> pools) where T : Enum{
        GameObject obj = pools[type].Objects.Count > 0 ?
            pools[type].Objects.Dequeue() : InstantiateObject(type, pools[type].Container);

        obj.SetActive(true);
        return obj;
    }

    public void DestroyObject(GameObject obj){
        if (obj.TryGetComponent<Abstract_minion>(out Abstract_minion component))
            enemy_pools[component.type].Objects.Enqueue(obj);

        if (obj.TryGetComponent<Abstract_Slime>(out Abstract_Slime componentS))
            slime_pools[componentS.type].Objects.Enqueue(obj);

        if (obj.TryGetComponent<Abstract_projectile>(out Abstract_projectile componentP))
            projectile_pools[componentP.type].Objects.Enqueue(obj);

        obj.SetActive(false);
    }
}