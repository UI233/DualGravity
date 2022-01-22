using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Planet : MonoBehaviour
{
    // helper
    // Defination of the trail
    public Vector3 center;
    public float radiusA;
    public float radiusB;
    public float angularVelocity;
    public PlanetInputAction controls;
    private float currentAngle;
    public float initialAngle;
    private Vector3 latePosition;
    public Animator anim;
    // Force property of this object
    public float gm;
    public float inputScale;
    protected void Awake()
    {
        controls = new PlanetInputAction();
    }

    protected void OnEnable()
    {
        controls.Enable();
    }
    // Start is called before the first frame update
    virtual protected void Start()
    {
        currentAngle = 0.0f;
        latePosition = transform.position;
        currentAngle = initialAngle;
        anim = GetComponent<Animator>();
    }
    
    // functions called oer update
    private void UpdateNextPos()
    {
        currentAngle += angularVelocity * Time.deltaTime % 360.0f;
        float theta = currentAngle / 360.0f * 2.0f * Mathf.PI;
        Vector3 pos = new Vector3(center.x + Mathf.Cos(theta) * radiusA, 
                                  center.y + Mathf.Sin(theta) * radiusB, 
                                  transform.position.z);
        transform.position = pos;
    }
    private void SelfRotation()
    {

    }
    // Update is called once per frame
    virtual protected void Update()
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
        return inputScale * gm * diff2d / diff2d.sqrMagnitude;
    }

    public void SetLock(bool value)
    {
        anim.SetBool("Lock", value);
    }

    public void SetNear(bool value)
    {
        anim.SetBool("Near", value);
    }

    public void SetCollid()
    {
        anim.SetTrigger("Collide");
    }
 }
