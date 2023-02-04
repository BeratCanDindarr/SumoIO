using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Zeminde olduðunu kontrol etmesi için yazdýðým interface
public interface ICheckedGround  
{
    void OnTheGroundCheck(bool _actived);
}
//Güç uygulandýðýnda yapmasý gerekeni göstermek için kullandýðým kod
public interface IForce
{
    void Force(GameObject _object,float _force,float _point);
}
