using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    private GameObject currentBuilding;

    void Update() {
        if (currentBuilding != null) {
            MoveBuildingToMouse();
            ReleaseBuilding();
            CancelBuilding();
        } else {
            // StartPlacingBuilding();
        }
    }

    public void StartPlacingBuilding(GameObject buildingPrefab)
    {
        Building building = buildingPrefab.GetComponent<Building>();

        if (GameManager.instance.money > building.cost)
        {
            currentBuilding = Instantiate(buildingPrefab);
            currentBuilding.GetComponent<BoxCollider2D>().enabled = false;
            GameManager.instance.isBuilding = true;
        } else {
            Debug.Log("돈없다");
        }
    }

    void MoveBuildingToMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;  // Z축 값을 0으로 설정하여 2D 위치로 만듦
        currentBuilding.transform.position = mousePosition;
        currentBuilding.GetComponent<Renderer>().material.color = IsValidPlacement() ? Color.green : Color.red;
    }

    void ReleaseBuilding()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Building building = currentBuilding.GetComponent<Building>();
            currentBuilding.GetComponent<Renderer>().material.color = Color.white;
            currentBuilding.GetComponent<BoxCollider2D>().enabled = true;
            currentBuilding = null;
            
            GameManager.instance.isBuilding = false;
            GameManager.instance.money -= building.cost;
        }
    }

    void CancelBuilding() {
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(currentBuilding);
            currentBuilding = null;
            GameManager.instance.isBuilding = false;
        }
    }

    bool IsValidPlacement()
    {
        return true;
    }
}