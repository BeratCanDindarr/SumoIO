using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject foodPrefabs;

    [SerializeField]public Queue<GameObject> foods;


    
    private void Start()
    {
       //Baþlangýçta 10 tane food objesini oluþturucak kod
            foods = new Queue<GameObject>();
            for (int k = 0; k < 10; k++)
            {
                GameObject obj =Instantiate(foodPrefabs,gameObject.transform);

                obj.SetActive(false);
                foods.Enqueue(obj);
            }
        
    }

    //Yemek lazým oldupunda baþvurulacak ve aktif olmayan yemeklerden birini verecek kod
    public GameObject GetPoolObject()
    {
        if (foods.Count == 0)
        {
            AddSizePool(5f);
        }
        GameObject obj = foods.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    //Geri aktifliðini kapatarak pool a eklememizi saðlayan kod blogu
    public void SetPoolObject(GameObject poolObject)
    {
        foods.Enqueue(poolObject);
        poolObject.SetActive(false);
    }

    //Pool da yemek kalmazsa kendine yemek ekleyen kod
    public void AddSizePool(float amount)
    {

        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(foodPrefabs,gameObject.transform);
            obj.SetActive(false);
            foods.Enqueue(obj);
        }
    }

}
