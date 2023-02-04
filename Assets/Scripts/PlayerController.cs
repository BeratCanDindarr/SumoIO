using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour,ICheckedGround,IForce
{
    public FloatingJoystick joystick;
    [SerializeField]private float playerSpeed;
    [SerializeField] private float playerRotationSpeed;

    PointSystem pointSystem;
    
    Rigidbody myRigidbody;

    [SerializeField]bool onTheGround = false;
    [SerializeField] bool onTriggered = false;

   
    // Start is called before the first frame update
    void Start()
    {
        pointSystem = GetComponent<PointSystem>();
        //Karakterin üzerinde bulunan Rigidbodynin otomatik eklenmesi için kullanýlan kod
        myRigidbody = this.GetComponent<Rigidbody>();
        GameManager.Instance.uIManager.SetPoint(pointSystem.point);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Karakterimizin Yere deðdiði sürece ve bir force yaþamadýðý sürece hareket etmesi için kullanýlan kod
        if (onTheGround && !onTriggered)
        { 
            //Karakterin joystick in konumlarýný alarak dönme rotasyonunun belirlenmesi için kullanýlan kod
            Vector3 forwardRotate = new Vector3(joystick.Horizontal,0,joystick.Vertical);
            transform.forward = Vector3.Lerp(transform.forward,forwardRotate,Time.deltaTime* playerRotationSpeed);
            var point = pointSystem.point;
            GameManager.Instance.uIManager.SetPoint(point);
            float offset = 10;
            
            switch (point)
            {
                case > 5000:
                    offset = 40;
                    break;
                case > 3000:
                    offset = 35;
                    break;
                case > 2000:
                    offset = 25;
                    break;
                case > 1000:
                    offset = 15;
                    break;
            }
            GameManager.Instance.camera.OffSetCamera(offset);
            //Karakterin Baktýðý yöne dümdüz hareket etmesini saðlayan kod
            myRigidbody.velocity = transform.forward*playerSpeed;
        }
    }

    //Karakterimize uygullanan güç ün hesaplandýðý kod
    public void Force(GameObject _object, float _force,float _point)
    {

        Vector3 forceForward = new Vector3(gameObject.transform.position.x - _object.transform.position.x, 0, gameObject.transform.position.z - _object.transform.position.z);
        float myPoint = pointSystem.point;
        float point = (myPoint - _point) * 0.1f;
        if (point > 100)
        {
            point = 100;
        }
        else if(point < -100)
        {
            point = -100;
        }
        _force -= point;
        Debug.Log(_force);
        myRigidbody.AddForce(forceForward * _force);
        StartCoroutine(WaitTrigger());
    }

    // GÜç uygulandýktan sonra 0.5 saniye beklemesi için yazýlan kod
    IEnumerator WaitTrigger()
    {
        onTriggered = true;
        yield return new WaitForSeconds(0.5f);
        
        onTriggered = false;
    }
    //Karakterin Ground'un üzerinde olup yada olmadýðý bildirildiðinde yapýlacaklar
    public void OnTheGroundCheck(bool _actived)
    {
        onTheGround = _actived;
    }

 
}
