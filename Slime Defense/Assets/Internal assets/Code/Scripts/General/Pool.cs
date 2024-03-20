using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    //переменная для контейнера, чтобы не спавнить все объекты в корне иерархии, а спавнить дочерними к объектам-контейнерам
    public Transform Container { get; private set; }
    public Queue<GameObject> Objects;

    public Pool(Transform container)
    {
        Container = container;
        Objects = new Queue<GameObject>();
    }

}
