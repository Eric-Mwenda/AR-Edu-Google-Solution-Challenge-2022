using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARManager : MonoBehaviour
{
    public GameObject cursorObject;
    public ARRaycastManager rayCastManager;
    public GameObject[] animalPrefabsArray;
    public bool useCursor;
    // Start is called before the first frame update
    void Start()
    {
        cursorObject.SetActive(useCursor);
    }

    // Update is called once per frame
    void Update()
    {
        if (useCursor)
        {
            UpdateCursor();
            if (Input.GetKeyDown(KeyCode.T))
            {
                Instantiate(animalPrefabsArray[1], transform.position, transform.rotation);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                Vector3 randomPosition = new Vector3(Random.Range(0, 5f), 0, Random.Range(0, 5f));
                Instantiate(animalPrefabsArray[2], transform.position + randomPosition, transform.rotation);
            }
        }

        /*if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (useCursor)
            {
                Instantiate(animalPrefabsArray[Random.Range(0, animalPrefabsArray.Length)], transform.position, transform.rotation);
            }
            else
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                rayCastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
                if (hits.Count > 0)
                {
                    Instantiate(animalPrefabsArray[Random.Range(0, animalPrefabsArray.Length)], hits[0].pose.position, hits[0].pose.rotation);
                }
            }
        }*/
    }

    private void UpdateCursor()
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayCastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }

    public void InstantiateObjectAtIndex(int objectIndex)
    {
        
        if (useCursor)
        {
            Instantiate(animalPrefabsArray[objectIndex], transform.position, transform.rotation);
            /*if (objectIndex == 3)
            {
                Vector3 randomPosition = new Vector3(Random.Range(-15f, 15f), 0, Random.Range(-15f, 15f));
                Instantiate(animalPrefabsArray[objectIndex], transform.position + randomPosition, transform.rotation);
            }*/
        }
        else
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            rayCastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
            if (hits.Count > 0)
            {
                Instantiate(animalPrefabsArray[objectIndex], hits[0].pose.position, hits[0].pose.rotation);
            }
        }
        if (objectIndex == 3 && GameObject.FindWithTag("Grass"))
        {
            var grasses = GameObject.FindGameObjectsWithTag("Grass");
            foreach (var grass in grasses)
            {
                grass.transform.LookAt(Camera.main.transform);
            }
        }
    }
}
