using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laboratory : MonoBehaviour
{
    void Update()
    {
        if (!gameObject.GetComponent<Building>().isBuilded) return;
        if (Input.GetMouseButtonDown(0))  // Left mouse button click
        {
            DetectBuildingClick();
        }
    }

    void DetectBuildingClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            GameObject clickedObject = hit.collider.gameObject;
            if (clickedObject == gameObject)
            {
                ShowBuildingUI(clickedObject);
            }
        }
    }

    void ShowBuildingUI(GameObject building)
    {
        GameManager.instance.ActiveLaboratoryUI();
    }
}
