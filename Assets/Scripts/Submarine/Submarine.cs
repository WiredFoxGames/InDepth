using System.Collections;
using UnityEngine;


public class Submarine : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth = 100;

    public float maxSpeed = 5;
    public float maxBooster = 500;
    public float maxPitchSpeed = 3;
    public float maxTurnSpeed = 50;
    public float acceleration = 2;
    float speedPercent = 0;
    private float curBooster = 100;
    private bool boosted = false;

    public float smoothSpeed = 3;
    public float smoothTurnSpeed = 3;

    public Transform propeller;
    public Transform rudderPitch;
    public Transform rudderYaw;
    public float propellerSpeedFac = 2;
    public float rudderAngle = 30;

    Vector3 velocity;
    float yawVelocity;
    float rollVelocity;
    float pitchVelocity;
    float currentSpeed;
    public Material propSpinMat;

    public GameObject bullet;

    void Start()
    {
        //Cursor.visible = false;
        currentSpeed = (int)(maxSpeed);
    }

    void Update()
    {
        if (curHealth > 0)
        {
            float accelDir = 0;
            if (Input.GetKey(KeyCode.Q))
            {
                accelDir -= 1;
            }

            if (Input.GetKey(KeyCode.E))
            {
                accelDir += 1;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (curBooster >= maxBooster)
                {
                    boosted = true;
                }
            }
        
            MovementManager(accelDir);
        }
        
        else
        {
            OnDeath();
        }
        
        

    }

    public void MovementManager(float accelDir)
    {
        
        if (boosted)
        {
            curBooster -= 5;
            currentSpeed += acceleration * Time.deltaTime * accelDir;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            currentSpeed = currentSpeed * 3;
            speedPercent = currentSpeed / maxSpeed;

            if (curBooster < 20)
            {
                boosted = false;
            }
        }
        else
        {
            if (curBooster < maxBooster)
            {
                curBooster += 1;
            }
            currentSpeed += acceleration * Time.deltaTime * accelDir;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            speedPercent = currentSpeed / maxSpeed;
        }

        Vector3 targetVelocity = transform.forward * currentSpeed;
        velocity = Vector3.Lerp(velocity, targetVelocity, Time.deltaTime * smoothSpeed);

        float targetPitchVelocity = Input.GetAxisRaw("Vertical") * maxPitchSpeed;
        pitchVelocity = Mathf.Lerp(pitchVelocity, targetPitchVelocity, Time.deltaTime * smoothTurnSpeed);

        float targetYawVelocity = Input.GetAxisRaw("Horizontal") * maxTurnSpeed;
        yawVelocity = Mathf.Lerp(yawVelocity, targetYawVelocity, Time.deltaTime * smoothTurnSpeed);
        transform.localEulerAngles +=
            (Vector3.up * yawVelocity + Vector3.left * pitchVelocity) * Time.deltaTime * speedPercent;
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        rudderYaw.localEulerAngles = Vector3.up * yawVelocity / maxTurnSpeed * rudderAngle;
        rudderPitch.localEulerAngles = Vector3.left * pitchVelocity / maxPitchSpeed * rudderAngle;

        propeller.Rotate(Vector3.forward * Time.deltaTime * propellerSpeedFac * speedPercent, Space.Self);
        propSpinMat.color =
            new Color(propSpinMat.color.r, propSpinMat.color.g, propSpinMat.color.b, speedPercent * .3f);
    }

    public void OnDeath()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;
    }
}