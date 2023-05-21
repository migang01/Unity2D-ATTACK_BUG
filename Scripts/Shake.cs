using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{

    public static Shake instance;
    private float startY;

    private float shakeTimeRemaining, shakePower, shakeFadeTime, shakeRotation;
    public float rotationMultiplier;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // startY is camera's Y position
        startY = transform.position.y;
    }

    private void LateUpdate()
    {
        if (shakeTimeRemaining > 0 && !GameManager.isCanvasOn)
        {
            // slightly reduce time to shake camera
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-.1f, .1f) * shakePower;
            float yAmount = Random.Range(-.1f, .1f) * shakePower;

            // shake camera randomly
            transform.position += new Vector3(xAmount, yAmount, 0f);

            // slightly reduce power of shaking camera
            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
            // slightly reduce shake rotation
            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
        }
        // rotate camera randomly about Z axis
        transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));
    }

    public void StartShake(float length, float power)
    {
        shakeTimeRemaining = length;
        shakePower = power;

        shakeFadeTime = power / length;

        shakeRotation = power * rotationMultiplier;
    }
}