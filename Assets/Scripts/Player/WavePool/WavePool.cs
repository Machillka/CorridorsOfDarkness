using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePool : MonoBehaviour
{
    // 使用单例模式管理对象池

    public static WavePool instance;

    public int poolCount;                               // 对象池的大小
    public GameObject wavePrefab;                       // 需要建立的对象

    Queue< GameObject > wavePool = new Queue< GameObject >();

    void Awake()
    {
        if (instance == null)
            instance = this;

        InitPool();
    }
    
    void InitPool()
    {
        for (int i = 0; i < poolCount; i++)
            PushinPool(CreateSingle());
        
    }

    GameObject CreateSingle()
    {
        var wave = Instantiate(wavePrefab);
        wave.transform.SetParent(transform);            // 修改成对象池的子物体
        return wave; 
    }

    // 把新的物体重新入队
    public void PushinPool(GameObject wave)
    {
        wave.SetActive(false);
        wavePool.Enqueue(wave);
    }

    // 得到物体
    public GameObject GetObject()
    {
        // 没物体了, 重新添加物体
        if (wavePool.Count == 0)
            PushinPool(CreateSingle());

        var wave = wavePool.Dequeue();
        wave.SetActive(true);

        return wave;
    }

}
