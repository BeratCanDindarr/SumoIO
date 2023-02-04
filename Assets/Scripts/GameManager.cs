using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

enum Name
{
    Player,
    Enemy,
    Food
};
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public List<GameObject> activedEnemys;
    public List<GameObject> activedFoods;

    public PoolManager poolManager;
    public UIManager uIManager;
    public CameraController camera;

    public float timer;


    public List<GameObject> spawnEnemy;
    private void Awake()
    {
        
        Instance = this;
        timer *= 60;
        uIManager.SetActivedPlayer(activedEnemys.Count);
    }
    
    private void Start()
    {
        //Karakterlerin belli bir saniye sonra spawn olmasýný saðlayan kod
        StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    { 
        yield return new WaitForSeconds(1f);
        SpawnEnemy();
        RandomFoodSpawner(20);
    }
    void SpawnEnemy()
    {
        foreach (var obje in spawnEnemy)
        {
            obje.SetActive(true);
        }
    }

    //Enum adlarýna göre hangi objenin oluþturulduðunda hangi listeye girmesi gerektiðine karar veren kod
    internal void AddObject(Name _name,GameObject _objects,bool _actived)
    {

        switch (_name)
        {

            case Name.Player:
                if (_actived)
                {
                    player = _objects;
                    activedEnemys.Add(_objects);
                }
                else
                {
                    player = null;
                    if (activedEnemys.Contains(_objects))
                    {
                        activedEnemys.Remove(_objects);
                        uIManager.SetActivedPlayer(activedEnemys.Count);
                    }
                }
                
                break;
            case Name.Food:
                if (_actived)
                {
                    activedFoods.Add(_objects);

                }
                else {
                    if (activedFoods.Contains(_objects))
                    {
                        activedFoods.Remove(_objects);

                    }
                }
                break;
            case Name.Enemy:
                if (_actived)
                {
                    activedEnemys.Add(_objects);
                }
                else {
                    if (activedEnemys.Contains(_objects))
                    {
                        activedEnemys.Remove(_objects);
                        uIManager.SetActivedPlayer(activedEnemys.Count);
                    }

                }
                break;

        }
    }

    private void Update()
    {
        //timer'ýn sürekli olarak çalýþtýgý ve UI a gönderildiði kýsým
        if (timer> 0)
        {
            timer -= Time.deltaTime;
            uIManager.SetTimer(timer);
        }
        if(timer <= 0 || activedEnemys.Count == 1)
        {
            Time.timeScale = 0;
            uIManager.SceneLoader();
            
        }
        uIManager.SetActivedPlayer(activedEnemys.Count);
    }

   //foodlarýn random olarak alanda daðýlmasýný saðlayan kod
    public void RandomFoodSpawner(int _size)
    {
        for (int i = 0; i < _size; i++)
        {
            var _objects = poolManager.GetPoolObject();
            
            float x = Random.Range(-19, 19);
            float z = Random.Range(-19, 19);
            _objects.transform.position = new Vector3(x,1,z);

        }
    }
    

}
