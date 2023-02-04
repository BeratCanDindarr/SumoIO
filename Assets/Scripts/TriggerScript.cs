using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    ICheckedGround CheckController;

    Rigidbody myRigidbody;

    bool rayActived = false;

    [SerializeField]PointSystem pointSystem;

    GameObject lastTouchEnemy;

    private void OnEnable()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        CheckController = gameObject.GetComponent<ICheckedGround>();
        pointSystem = gameObject.GetComponent<PointSystem>();
    }
    private void Update()
    {
        if (!rayActived)
        {
            BeamThrowing();
        }
    }
    
    void BeamThrowing()
    {
        RaycastHit hit;
        // Yeni ray(I��n) tan�mlayarak karakterin �zerinden A�a��ya do�ru at�lan ray i�in kullan�lan kod yap�s�
        Ray ray = new Ray(gameObject.transform.position, -Vector3.up);
        if (Physics.Raycast(ray, out hit))
        {

            // Att���m�z ray'in(I��n�n) neye �arpt���n� hit parametresiyle tutuyoruz ve bu hit parametresinin yani ���n�n de�di�i objenin tag'�na bakarak o objenin ne oldu�una bak�p ona g�re i�lem yap�yoruz.
            if (hit.collider.CompareTag("Ground"))
            {
                Debug.Log("dogru");
                CheckController.OnTheGroundCheck(true);
            }
            else if (hit.collider.CompareTag("Water"))
            {
                Debug.Log("yanl��");
                CheckController.OnTheGroundCheck(false);
                Destroy(gameObject, 0.5f);
            }
            StartCoroutine(WaitRay());
        }
    }
    // ���n atma s�kl���n� d���rmek i�in kulland���m bekleme foksiyonu
    IEnumerator WaitRay()
    {
        rayActived = true;
        yield return new WaitForSeconds(0.5f);
        rayActived = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Player yada Enemy tagl� birine �arparsa karakterin uygulamas� gereken forcun bulundu�u k�s�m
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            float y = transform.eulerAngles.y - collision.transform.eulerAngles.y;
            y = Mathf.Abs(y);
            float _force = CheckedForce(y);
            float _point = pointSystem.point;
            collision.gameObject.GetComponent<IForce>().Force(gameObject, _force, _point);
            lastTouchEnemy = collision.gameObject;
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        //Yemege de�diginde puan� art�rmas� ve o yemegin geri pool a g�ndermesini saglayan kod
        if (other.gameObject.CompareTag("Food"))
        {
            Debug.Log("deneme");
            pointSystem.AddPoint(100f);
            GameManager.Instance.RandomFoodSpawner(1);

            GameManager.Instance.poolManager.SetPoolObject(other.gameObject);
        }
    }

    //�arp��an karakterlerin a��lar�na g�re Neresinden �arpt���m�z� anlamam�za yard�mc� olan kod
    float CheckedForce(float _rotationPoint)
    {
        float _force = 0;
        switch (_rotationPoint)
        {
            case <= 60:
                Debug.Log("Arka");
                _force = 500;
                break;
            case <= 150:
                Debug.Log("yan");
                _force = 300;
                break;
            case <= 240:
                Debug.Log("�n");
                _force = 200;
                break;
            case <= 320:
                Debug.Log("yan");
                _force = 300;
                break;
            case <= 360:
                Debug.Log("Arka");
                _force = 500;
                break;
        }
        return _force;
    }

    private void OnDestroy()
    {
        if (lastTouchEnemy != null)
        {

            lastTouchEnemy.GetComponent<PointSystem>().AddPoint(pointSystem.point);
        }

    }

}
