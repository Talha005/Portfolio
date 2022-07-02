using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum states
    {
        straight,
        wavy,
        loop
    }
    public states currentState;
    public float speed;
    Vector3 hiddenPosition;
    [Header("Wave Variables")]
    public float amplitude;
    public float period;
    public float shift;
    public float ychange;
    private float newX;
    private float newY;
    private Rigidbody myRigidbody;
    Vector3 direction = new Vector3(0, -1, 0);
    public void wakeup()
    {
        this.hiddenPosition = transform.position;
    }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = this.direction * this.speed;
        //checks for method of movement
        switch (currentState)
        {     
            case states.straight:
                myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, -speed, 0);
                break;
            case states.wavy:
                newY = transform.position.y - ychange;
                newX = amplitude * Mathf.Sin(period * newY) + shift;
                Vector3 tempPosition = new Vector3(newX, newY, 0);
                transform.position = tempPosition;
                break;
            case states.loop:
                {
                    // Radius.
                    float radius =4.5f;
                    float rate = 1.0f;
                    float y = Mathf.Sin(Time.time * rate) * radius;
                    float x = Mathf.Cos(Time.time * rate) * radius;
                    //newX = amplitude * Mathf.Sin(period * newY) + shift;
                    //newY = amplitude * Mathf.Cos(period * newX) + shift;
                    Vector3 localCirclePosition = new Vector3(x, y, 0);

                    // Calculate the body position to move down.
                    this.hiddenPosition += Time.deltaTime * velocity;

                    transform.position = this.hiddenPosition + localCirclePosition;
                }
                break;
            
        }
    }
}
