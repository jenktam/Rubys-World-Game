﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;
    public int maxHealth = 5;
    int currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    public int health
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
        }
    }

    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        // makes Unity render 10 frames/sec instead of default 60 frames/sec
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;

        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        // ** HORIZONTAL MOVEMNT ** 
        // press left: axis -1, making position.x = -0.1f
        // press right: axis +1, making positon.x = +0.1f.
        // press no key: axis 0, making position 0f;

         //** TIME ** 
        // must update movement speed by multiplying it with time it takes for Unity to render a frame.
        // ex: if game runs 10 frame/sec, each frame takes 0.1 sec. 60 frames/sec = each frame takes 0.0167 sec
        // Time.deltaTime important in making sure character runs at the same speed, regardless of the number of frames rendered by our game.
        // it is now **frame independent**.

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Debug.Log($"(x,t): ({horizontal}, {vertical})");
        Vector2 position = rigidbody2d.position;
       
        // added Time.deltaTime to make sure movement based on units/seconds not frames/second
        // slightly change to movement code multiplies amount the GameObject moves by the value of the axis.
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        transform.position = position;

        rigidbody2d.MovePosition(position);


        if(isInvincible)
        {
            // invincibility timer
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0)
            {
                isInvincible = false;
            }
    }

}

    public void ChangeHealth(int amount)
    {
        // if hurting player
        if(amount < 0)
        {
            if (isInvincible) return;

            // if not invincible, hurt player
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log($"{currentHealth}/{maxHealth}");
    }
}
