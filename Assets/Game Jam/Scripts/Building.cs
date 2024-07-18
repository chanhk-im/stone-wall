using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public int health;
    public int maxHealth;
    private int originalMaxHealth;
    public int cost;
    public bool isBuilded;
    public bool isInsideNoBuildZone;

    private SpriteRenderer sprite;
    public GameObject buildingInfoPrefab;
    private GameObject buildingInfoInstance;
    private bool isShowInfo;
    private Camera mainCamera;

    private void Awake() {
        health = maxHealth;
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        originalMaxHealth = maxHealth;
        mainCamera = Camera.main;
        isShowInfo = false;
    }

    public void Update() {
        if (!isBuilded) return;
        if (Input.GetMouseButtonDown(1))
        {
            DetectBuildingClick();
        }

        if (buildingInfoInstance) {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position + Vector3.up * 2);
            buildingInfoInstance.transform.position = screenPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        isInsideNoBuildZone = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        isInsideNoBuildZone = false;
    }

    public IEnumerator HitByEnemy(int damage) {
        sprite.color = Color.red;
        health -= damage;
        if (health < 0) {
            Destroy(gameObject);
        }

        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

    private void OnDisable()
    {
        maxHealth = originalMaxHealth;
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
                if (!isShowInfo) {
                    ShowBuildingUI(clickedObject);
                    isShowInfo = true;
                }
                    
                else {
                    isShowInfo = false;
                    HideBuildingUI();
                }
            }
        }
    }

    void ShowBuildingUI(GameObject building)
    {
        buildingInfoInstance = Instantiate(buildingInfoPrefab);
        buildingInfoInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);
        Text[] texts = buildingInfoInstance.GetComponentsInChildren<Text>();
        if (texts.Length == 2) {
            texts[0].text = "건물";
            texts[1].text = string.Format("체력: {0} / {1}", health, maxHealth);
        }
    }

    void HideBuildingUI() {
        Destroy(buildingInfoInstance);
        buildingInfoInstance = null;
    }
}
