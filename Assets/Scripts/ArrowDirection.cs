using UnityEngine;

//Attach this script to a GameObject to rotate around the target position.
public class ArrowDirection : MonoBehaviour
{
    //Assign a GameObject in the Inspector to rotate around
    Vector3 mousePosition;
    public bool isEnabled;
    [Range(0,4)]
    public int dir = 1;
    private float publicAngle;
    private float _angle;
    public void SetActive (bool val) {
        isEnabled = val;
        gameObject.SetActive(val);
    }

    void Update()
    {   
        if (!isEnabled) return;
        // Spin the object around the target at 20 degrees/second.
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePosition.z = transform.position.z - Camera.main.transform.position.z; // это только для перспективной камеры необходимо
        float angle = Vector2.Angle(Vector2.right, mousePosition - transform.position);//угол между вектором от объекта к мыше и осью х
        publicAngle = transform.position.y < mousePosition.y ? angle  : -angle ;
        angle = transform.position.y < mousePosition.y ? angle - dir * 90 : -angle - dir * 90;
        transform.eulerAngles = new Vector3(0f, 0f, angle);//немного магии на последок
    }
    public Vector2 GetNormalizedRotation () {
        //Debug.Log("angel: " + publicAngle);
        return new Vector2 (x(publicAngle), y(publicAngle));
        
    }
    public int GetDirByAngle() {
        return publicAngle > -90 && publicAngle < 90 ? 1 : -1;
    }
    float x (float angle) {
        float devideData = 0;
        if (angle > 135) devideData = -45; // from 180 to 135
        else if (angle > 90) devideData = -angle + 90; // from 90 to 135
        else if (angle > 45) devideData = 90 - angle; // from 45 to 90
        else if (angle > 0) devideData = 45; // from 0 to 45
        else if (angle > -45) devideData = 45; // from -45 to 0
        else if (angle > -90) devideData = (90 + angle); // from -90 to -45
        else if (angle > -135) devideData = (90 +  angle ); // from -135 to -90
        else if (angle > -180) devideData = -45; // from -180 to -135
        return devideData / 45;
    }
    float y (float angle) {
        float devideData = 0;
        if (angle > 135) devideData = 180 - angle; // from 180 to 135
        else if (angle > 45) devideData = 45; // from 45 to 135
        else if (angle > 0) devideData = angle; // from 0 to 45
        else if (angle > -45) devideData = angle; // from -45 to 0
        //else if (angle > -90) devideData = (-90 - angle);
        else if (angle > -135) devideData = -45; // from -135 to -45
        else if (angle > -180) devideData = (-180 - angle); // from -180 to -135
        return devideData / 45;
    }
}