using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiningBuilding : MonoBehaviour
{
    public int income;
    private int originalIncome;
    public float incomeTic;
    public GameObject incomeTextPrefab;
    GameObject incomeTextInstance;
    float lastDepositTime = 0f;
    private Camera mainCamera;


    void Start() {
        StartCoroutine(DepositMoney());
        mainCamera = Camera.main;
        originalIncome = income;
    }

    void Update() {
        if (incomeTextInstance) {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position + Vector3.up * 2);
            incomeTextInstance.transform.position = screenPosition;
        }
    }

    // Update is called once per frame
    private IEnumerator DepositMoney() {
        while (true) {
            yield return new WaitForSeconds(incomeTic);
            Deposit();
        }
    }

    private void Deposit() {
        if (Time.time >= lastDepositTime + incomeTic)
        {
            GameManager.instance.money += income;
            StartCoroutine(PrintIncome());
        }
    }

    IEnumerator PrintIncome() {
        incomeTextInstance = Instantiate(incomeTextPrefab);
        incomeTextInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);
        incomeTextInstance.GetComponent<Text>().text = string.Format("+{0}G", income);
        yield return new WaitForSeconds(0.5f);
        Destroy(incomeTextInstance);
        incomeTextInstance = null;
    }

    private void OnDisable()
    {
        income = originalIncome;
        Debug.Log(income);
    }
}
