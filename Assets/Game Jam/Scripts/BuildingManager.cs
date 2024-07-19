using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public GameObject buildButton;
    public int[] currBuildingCount;

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
            StartCoroutine(ReleaseBuilding());
            CancelBuilding();
        } else {
            // StartPlacingBuilding();
        }
    }

    public void StartPlacingBuilding(GameObject buildingPrefab)
    {
        Building building = buildingPrefab.GetComponent<Building>();

        if (currentBuilding) {
            Destroy(currentBuilding);
            currentBuilding = null;
            GameManager.instance.isBuilding = false;
        }

        if (building.data.isRestrictedCount && currBuildingCount[building.data.id] >= building.data.maxCount) {
            Debug.Log("최대 개수 초과");
            return;
        }

        if (GameManager.instance.money >= building.cost)
        {
            buildButton.SetActive(false);
            currentBuilding = Instantiate(buildingPrefab);
            currentBuilding.GetComponent<SpriteRenderer>().sortingLayerName = "Layer 3";
            currentBuildingStatus = currentBuilding.GetComponent<Building>(); 
            currentBuildingCollider = currentBuilding.GetComponent<Collider2D>();

            currBuildingCount[building.data.id]++;

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

    IEnumerator ReleaseBuilding() {
        if (Input.GetMouseButtonDown(0) && IsValidPlacement()) {
            Building building = currentBuilding.GetComponent<Building>();

            buildButton.SetActive(true);
            currentBuilding.GetComponent<Renderer>().material.color = Color.white;
            currentBuilding.GetComponent<SpriteRenderer>().sortingLayerName = "Layer 1";
            currentBuilding.GetComponent<SpriteRenderer>().sortingOrder = -(int)(currentBuilding.transform.position.y * 100);
            currentBuildingCollider.isTrigger = false;

            currentBuilding = null;
            currentBuildingCollider = null;
        
            GameManager.instance.isBuilding = false;
            GameManager.instance.money -= building.cost;

            yield return null;
            yield return null;
            yield return null;
            building.isBuilded = true;
        }
    }

    void CancelBuilding() {
        if (Input.GetMouseButtonDown(1))
        {
            currBuildingCount[currentBuilding.GetComponent<Building>().data.id]--;
            Destroy(currentBuilding);
            currentBuilding = null;
            GameManager.instance.isBuilding = false;
            buildButton.SetActive(true);
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