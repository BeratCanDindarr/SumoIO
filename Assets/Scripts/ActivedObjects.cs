using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivedObjects : MonoBehaviour
{
    [SerializeField]Name name;

    private void OnEnable()
    {
        //Kendini gamemanager de bulunan kendi kýsmýna ekleyecek kod
        GameManager.Instance.AddObject(name, gameObject, true);
    }
    private void OnDisable()
    {
        //Kendini gamemanager de bulunan kendi kýsmýna ekleyecek kod
        GameManager.Instance.AddObject(name, gameObject, false);
    }
}
