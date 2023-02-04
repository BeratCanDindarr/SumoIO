using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Zeminde oldu�unu kontrol etmesi i�in yazd���m interface
public interface ICheckedGround  
{
    void OnTheGroundCheck(bool _actived);
}
//G�� uyguland���nda yapmas� gerekeni g�stermek i�in kulland���m kod
public interface IForce
{
    void Force(GameObject _object,float _force,float _point);
}
