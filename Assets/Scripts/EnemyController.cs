using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour,ICheckedGround,IForce
{
   
    public GameObject target;
    Rigidbody myRigidbody;
    [SerializeField]int enemySpeed;
    [SerializeField] float rotationSpeed;

    PointSystem pointSystem;
    bool onTriggered = false;
    [SerializeField] bool onTheGround = false;

    private void OnEnable()
    {
        
       

        //Karakterin üzerinde bulunan Rigidbodynin otomatik eklenmesi için kullanýlan kod
        myRigidbody = this.GetComponent<Rigidbody>();

    }
    private void Start()
    {
        
        pointSystem = GetComponent<PointSystem>();
    }
    private void Update()
    {
        // Yere deðdiði sürece ve bir force yaþamadýðý sürece hareket etmesi için kullanýlan kod
        if (!onTriggered && onTheGround)
        {
            TargetDetect();
            var lookpos = target.transform.position - gameObject.transform.position;
            lookpos.y = 0;
            var rotation = Quaternion.LookRotation(lookpos);
            transform.rotation = Quaternion.Slerp(transform.rotation,rotation,Time.deltaTime* rotationSpeed);
            myRigidbody.velocity = transform.forward * enemySpeed;

        }

    }
    //Enemy'in Ground'un üzerinde olup yada olmadýðý bildirildiðinde yapýlacaklar
    public void OnTheGroundCheck(bool _actived)
    {
        onTheGround = _actived;
    }
    //Karþýsýndaki kiþinin kendisine uygulayacaðý güç hesabý
    public void Force(GameObject _object, float _force,float _point)
    {

        Vector3 forceForward = new Vector3(gameObject.transform.position.x - _object.transform.position.x, 0, gameObject.transform.position.z - _object.transform.position.z);
        float myPoint = pointSystem.point;
        float point = (myPoint - _point) *0.1f;
        if (point > 100)
        {
            point = 100;
        }
        else if (point < -100)
        {
            point = -100;
        }
        _force -= point;
        myRigidbody.AddForce(forceForward * _force);
        StartCoroutine(WaitTrigger());
    }
   
    //güç uygulandýktan 0.5 saniye boyunca karakterin hareket edememesi için kullanýlan kod
    IEnumerator WaitTrigger()
    {
        onTriggered = true;
        yield return new WaitForSeconds(0.5f);
        onTriggered = false;
    }
    void TargetDetect()
    {
        // Önce aktif olan yemeklerin içinde en yakýn olaný bulup buraya not alýyoruz ve onun karaktere olan uzaklýðýný buluyoruz
        var food = FindClosesFood();
        float currentFoodDistance = Vector3.Distance(transform.position, food.transform.position);
        Debug.Log(food);
        // Sonra aktif olan düþmanlarýn içinde en yakýn olaný bulup buraya not alýyoruz ve onun karaktere olan uzaklýðý buluyoruz
        var enemy = FindClosesEnemy();
        float currentEnemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
        Debug.Log(enemy);
        // hangisinin karaktere daha yakýn olduguna bakýp ona göre düþman yapay zekasýnýn hangisine yonlenecegine bakýyoruz
        if (currentEnemyDistance <= currentFoodDistance)
        {
            target = enemy;
        }
        else
        {
            target = food;
        }
    }


    // En yakýn düþmaný bulduðumuz metod
    GameObject FindClosesEnemy()
    {
        float closesDistance = Mathf.Infinity;
        GameObject _obje = null;
        foreach (var enemy in GameManager.Instance.activedEnemys)
        {
            float currentDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (currentDistance < closesDistance && gameObject != enemy )
            {
                closesDistance = currentDistance;
                _obje = enemy;
            }
        }
        Debug.Log(_obje);
        return _obje;
    }

    // En yakýn yemegi bulduðumuz metod
    GameObject FindClosesFood()
    {
        float closesDistance = Mathf.Infinity;
        GameObject _obje = null;
        foreach (var food in GameManager.Instance.activedFoods)
        {
            float currentDistance = Vector3.Distance(transform.position, food.transform.position);
            if (currentDistance < closesDistance )
            {
                closesDistance = currentDistance;
                _obje = food;
            }
        }
        Debug.Log(_obje);
        return _obje;
    }

}
