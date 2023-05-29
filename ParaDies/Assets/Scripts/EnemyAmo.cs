using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAmo : MonoBehaviour
{

    public Transform objectToScale;
    public float scaleSpeed = 0.05f;
    public float targetScale = 1f;
    public float scaleDuration = 2f;

    private float currentScale;
    private float scaleTimer;

    public bool ChargeOK = false;

    // Start is called before the first frame update
    void Start()
    {
        currentScale = objectToScale.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        // 檢查是否已達到目標大小
        if (currentScale +0.01f < targetScale)
        {
            ChargeOK = false;
            // 更新縮放時間
            scaleTimer += Time.deltaTime / 100;

            // 計算新的縮放比例
            float newScale = Mathf.Lerp(currentScale, targetScale, scaleTimer / scaleDuration);

            // 更新物件的縮放屬性
            objectToScale.localScale = Vector3.one * newScale;

            // 更新當前縮放比例
            currentScale = newScale;
        }
        else
        {
            ChargeOK = true;
        }
    }
}
