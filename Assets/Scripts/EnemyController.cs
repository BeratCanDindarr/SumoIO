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
        
       

        //Karakterin �zerinde bulunan Rigidbodynin otomatik eklenmesi i�in kullan�lan kod
        myRigidbody = this.GetComponent<Rigidbody>();

    }
    private void Start()
    {
        
        pointSystem = GetComponent<PointSystem>();
    }
    private void Update()
    {
        // Yere de�di�i s�rece ve bir force ya�amad��� s�rece hareket etmesi i�in kullan�lan kod
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
    //Enemy'in Ground'un �zerinde olup yada olmad��� bildirildi�inde yap�lacaklar
    public void OnTheGroundCheck(bool _actived)
    {
        onTheGround = _actived;
    }
    //Kar��s�ndaki ki�inin kendisine uygulayaca�� g�� hesab�
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
   
    //g�� uyguland�ktan 0.5 saniye boyunca karakterin hareket edememesi i�in kullan�lan kod
    IEnumerator WaitTrigger()
    {
        onTriggered = true;
        yield return new WaitForSeconds(0.5f);
        onTriggered = false;
    }
    void TargetDetect()
    {
        // �nce aktif olan yemeklerin i�inde en yak�n olan� bulup buraya not al�yoruz ve onun karaktere olan uzakl���n� buluyoruz
        var food = FindClosesFood();
        float currentFoodDistance = Vector3.Distance(transform.position, food.transform.position);
        Debug.Log(food);
        // Sonra aktif olan d��manlar�n i�inde en yak�n olan� bulup buraya not al�yoruz ve onun karaktere olan uzakl��� buluyoruz
        var enemy = FindClosesEnemy();
        float currentEnemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
        Debug.Log(enemy);
        // hangisinin karaktere daha yak�n olduguna bak�p ona g�re d��man yapay zekas�n�n hangisine yonlenecegine bak�yoruz
        if (currentEnemyDistance <= currentFoodDistance)
        {
            target = enemy;
        }
        else
        {
            target = food;
        }
    }


    // En yak�n d��man� buldu�umuz metod
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

    // En yak�n yemegi buldu�umuz metod
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
