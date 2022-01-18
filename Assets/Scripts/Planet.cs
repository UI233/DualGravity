using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // helper
    // Defination of the trail
    public Vector3 center;
    public float radius;
    public float angularVelocity;
    private float currentAngle;
    private Vector3 latePosition;
    // Force property of this object
    public float gm;
    // Start is called before the first frame update
    void Start()
    {
        currentAngle = 0.0f;
        latePosition = transform.position;
    }
    
    // functions called oer update
    private void UpdateNextPos()
    {
        currentAngle += angularVelocity * Time.deltaTime;
        Vector3 pos = new Vector3(center.x + Mathf.Cos(currentAngle) * radius, 
                                  center.y + Mathf.Sin(currentAngle) * radius, 
                                  transform.position.z);
        transform.position = pos;
    }
    private void SelfRotation()
    {

    }
    // Update is called once per frame
    void Update()
    {
        UpdateNextPos();
        SelfRotation();
    }

    void LateUpdate()
    {
        latePosition = transform.position;
    }

    // Apply force to player
    public Vector2 GetGravity(Vector3 objectPos)
    {
        Vector3 diff = latePosition - objectPos;
        Vector2 diff2d = new Vector2(diff.x, diff.y);
        float eps = 1e-3f;
        if (diff2d.sqrMagnitude < eps)
            return new Vector2(0.0f, 0.0f);
        return gm * diff2d / diff2d.sqrMagnitude;
    }
}
