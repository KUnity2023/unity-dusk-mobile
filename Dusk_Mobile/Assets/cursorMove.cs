using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cursorMove : MonoBehaviour
{
    public float movementSpeed = 2f;  // 움직임 속도
    public float amplitude = 20f;     // 움직임의 크기
    public float frequency = 1.0f;    // 움직임의 주기

    private RectTransform rectTransform;
    private Vector2 startPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    void Update()
    {
        // 시간에 따른 Y축의 이동량 계산
        float yOffset = amplitude * Mathf.Sin(frequency * Time.time);

        // 새로운 위치 설정
        Vector2 newPosition = startPosition + new Vector2(0f, yOffset);

        // 이미지를 부드럽게 이동
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, newPosition, movementSpeed * Time.deltaTime);
    }
}
