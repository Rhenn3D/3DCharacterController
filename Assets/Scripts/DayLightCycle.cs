using UnityEngine;

public class DayLightCycle : MonoBehaviour
{

    public int _rotationSpeedX = 5;
    public int _rotationSpeedY = 5;
    void Update()
    {
        transform.Rotate(_rotationSpeedX * Time.deltaTime, _rotationSpeedY * Time.deltaTime, 0);
    }
}
