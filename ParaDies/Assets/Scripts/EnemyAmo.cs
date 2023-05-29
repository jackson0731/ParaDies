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
        // �ˬd�O�_�w�F��ؼФj�p
        if (currentScale +0.01f < targetScale)
        {
            ChargeOK = false;
            // ��s�Y��ɶ�
            scaleTimer += Time.deltaTime / 100;

            // �p��s���Y����
            float newScale = Mathf.Lerp(currentScale, targetScale, scaleTimer / scaleDuration);

            // ��s�����Y���ݩ�
            objectToScale.localScale = Vector3.one * newScale;

            // ��s��e�Y����
            currentScale = newScale;
        }
        else
        {
            ChargeOK = true;
        }
    }
}
