using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class GazelleManager : MonoBehaviour
{
    private bool _isGrassFound, _isExecuteOnce;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(Camera.main.transform);
    }

    // Update is called once per frame
    void Update()
    {
        _isGrassFound = GameObject.FindWithTag("Grass");
        if (_isGrassFound && !_isExecuteOnce)
        {
            Debug.Log("Grass Has Been found and this Has executed ONCE!.");
            Vector3 grassPosition = GameObject.FindGameObjectWithTag("Grass").transform.position;
            _isExecuteOnce = true;
            transform.LookAt(grassPosition);
            GetComponent<Animation>().Play("Gazelle Walk");
            DOTween.To(() => transform.position, x => transform.position = x, grassPosition, 10f);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Grass"))
        {
            GetComponent<Animation>().Play("Gazelle Idle");
            other.GetComponentInChildren<TextMeshPro>().text = "";
        }
    }
}
