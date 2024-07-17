using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    private GameObject currentBuilding;
    private Building currentBuildingStatus;
    Collider2D currentBuildingCollider;
    bool isInUnableArea;

    void Awake() {
        isInUnableArea = false;
    }

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

        if (GameManager.instance.money >= building.cost)
        {
            currentBuilding = Instantiate(buildingPrefab);
            currentBuildingStatus = currentBuilding.GetComponent<Building>(); 
            currentBuildingCollider = currentBuilding.GetComponent<Collider2D>();

            // 충돌 무시 설정
            currentBuildingCollider.isTrigger = true;
        } else {
            Debug.Log("돈없다");
        }
    }

    void MoveBuildingToMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;  // Z축 값을 0으로 설정하여 2D 위치로 만듦
        currentBuilding.transform.position = mousePosition;
        if (IsValidPlacement()) {
            currentBuilding.GetComponent<Renderer>().material.color = Color.green;
        } else {
            currentBuilding.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void ReleaseBuilding()
    {
        if (Input.GetMouseButtonDown(0) && IsValidPlacement())
        {
            Building building = currentBuilding.GetComponent<Building>();
            currentBuilding.GetComponent<Renderer>().material.color = Color.white;
            currentBuildingCollider.isTrigger = false;

            currentBuilding = null;
            currentBuildingCollider = null;
        
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
        if (currentBuildingStatus.isInsideNoBuildZone || IsOverlapping()) return false;
        return true;
    }

    bool IsOverlapping()
    {
        Bounds bounds = currentBuildingCollider.bounds;
        Collider2D[] overlaps = Physics2D.OverlapAreaAll(bounds.min, bounds.max);
        foreach (var overlap in overlaps)
        {
            // Debug.Log(overlap.gameObject.name);
            if (overlap != currentBuildingCollider)
            {
                return true;
            }
        }
        return false;
    }
}