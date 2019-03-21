using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageAnimator : MonoBehaviour
{
    public Sprite image1;
    public Sprite image2;

    public float animInterval = .2f;

    Image renderImage;
    int currentImage = 0;
    float currentTime = 0f;

    private void Awake()
    {
        renderImage = GetComponent<Image>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > animInterval)
        {
            Debug.Log(currentImage);

            if (currentImage == 0)
            {
                currentImage++;
                renderImage.sprite = image1;
                currentTime = 0f;
                return;
            }
            else
            {
                currentImage = 0;
                renderImage.sprite = image2;
                currentTime = 0f;
                return;
            }
        }
    }
}
