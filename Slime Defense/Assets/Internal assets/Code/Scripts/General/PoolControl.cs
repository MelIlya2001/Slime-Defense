using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
     public static PoolControl Instance;

/*testpool
    
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
    
    /*
    #region [ Pool Info ]
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
        public uint delay_seconds_to_summon;
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
    #endregion [ Pool Info ]




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
    #region [ Enemy Methods ]
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
    #endregion [ Enemy Methods ]

    #region [ Projectile Methods ]
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
    #endregion [ Projectile Methods ]

    #region [ Slime Methods ]
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
    #endregion [ Slime Methods ]

    */

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
    
   
   
    [Serializable]
    public class ObjectPool<T> where T : Enum
    {
        public T Type;
        public GameObject Prefab;
        public int StartCount;
        public uint delay_seconds_to_summon;
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

        GameObject parentContainer = Instantiate(empty, transform, false);
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
                break;

            case "PoolControl+ProjectileType":
                in_obj = Instantiate(projectilesInfo.Find(x => x.Type.Equals(type)).Prefab, parent);
                break;

            case "PoolControl+EnemyType":
            default:
                in_obj = Instantiate(enemiesInfo.Find(x => x.Type.Equals(type)).Prefab, parent);
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