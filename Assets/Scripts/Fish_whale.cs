using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_whale : MonoBehaviour
{
    private float timer_change = 0f;
    private float timer_change_max = 1f;

    public void Start()
    {
        this.transform.localRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        this.timer_change_max = Random.Range(2f, 8f);
    }

    void Update()
    {
        this.timer_change += 1f * Time.deltaTime;
        this.transform.Translate(Vector3.back * 1f * Time.deltaTime);
        if (this.timer_change > this.timer_change_max)
        {
            this.change_rotate();
            this.timer_change = 0f;
        }
    }

    private void change_rotate()
    {
        this.transform.rotation *= Quaternion.Euler(0, 180, 0);
    }
}
