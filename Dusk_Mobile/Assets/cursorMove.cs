using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cursorMove : MonoBehaviour
{
    public float movementSpeed = 2f;  // ������ �ӵ�
    public float amplitude = 20f;     // �������� ũ��
    public float frequency = 1.0f;    // �������� �ֱ�

    private RectTransform rectTransform;
    private Vector2 startPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    void Update()
    {
        // �ð��� ���� Y���� �̵��� ���
        float yOffset = amplitude * Mathf.Sin(frequency * Time.time);

        // ���ο� ��ġ ����
        Vector2 newPosition = startPosition + new Vector2(0f, yOffset);

        // �̹����� �ε巴�� �̵�
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, newPosition, movementSpeed * Time.deltaTime);
    }
}
