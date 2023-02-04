using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class PointSystem : MonoBehaviour
{
    public float point;
    public GameObject pointPanel;
    TextMeshProUGUI text;

   //Gelen puaný karakterin üzerine kaydeden kod
    public void AddPoint(float _value)
    {
        point += _value;
        SpawnPointPanel(_value);
        CharactersScale(_value * 0.001f);
    }
    //Puan aldýkca karakterlerin üzerinde çýkan puan penceresi 
    public void SpawnPointPanel(float _value)
    {
        var a = Instantiate(pointPanel);
        a.transform.position = gameObject.transform.position;
        text = a.GetComponentInChildren<TextMeshProUGUI>();
        text.text = _value.ToString();
        a.transform.DOScale(1.2f,1);
        Destroy(a,1f);
    }

    //Puan aldýkca karakterlerin boyunu artýrmakla görevli kod
    public void CharactersScale(float _value)
    {
        gameObject.transform.DOScale(gameObject.transform.localScale.x+_value,1);
        gameObject.transform.position += new Vector3(0,_value,0);
    }
   
}
