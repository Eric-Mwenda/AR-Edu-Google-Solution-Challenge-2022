using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class TigerManager : MonoBehaviour
{
    private bool _isGazelleFound, _isExecuteOnce;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animation>().Play("Tiger Idle");
        transform.LookAt(Camera.main.transform);
    }

    // Update is called once per frame
    void Update()
    {
        _isGazelleFound = GameObject.FindWithTag("Gazelle");
        if (_isGazelleFound && !_isExecuteOnce)
        {
            Debug.Log("Gazelle Has Been found and this Has executed ONCE!.");
            Vector3 gazellePosition = GameObject.FindGameObjectWithTag("Gazelle").transform.position;
            _isExecuteOnce = true;
            transform.LookAt(gazellePosition);
            GetComponent<Animation>().Play("Tiger Creep");
            DOTween.To(() => transform.position, x => transform.position = x, gazellePosition, 10f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("I am TIGER and I've Triggered with " + other.transform.tag);
       
       if (other.transform.CompareTag("Gazelle"))
       {
           Debug.Log("I am TIGER & I've collided with " + other.transform.tag);

           GetComponent<Animation>().Play("Tiger Attack");

           other.gameObject.GetComponent<Animation>().Play("Death");
           other.GetComponentInChildren<TextMeshPro>().text = "";
       }
    }
}
