using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    GameObject player;
    public int speedRotation = 3;
    public int speedRotCar = 4;
    public int speed = 1;
    public GameObject AxisColesoL;  //Пустой объект на котором находится переднее левое колесо. Нужно для правильного вращения колеса вокруг вертикальной оси вращения.
    public GameObject AxisColesoR;  // Пустой объект на котором находится переднее правое колесо. Тоже нужно для правильного вращения колеса вокруг вертикальной оси вращения. 
    public GameObject Coleso_LZ;  //Объект левого заднего колеса
    public GameObject Coleso_RZ;  //Объект правого заднего колеса
    public GameObject Coleso_LP;  //Объект левого переднего колеса
    public GameObject Coleso_RP;  //Объект правого переднего колеса
    public GameObject korpus;  //корпус автомобиля, который будет наклоняться при ускорении и торможении
    public GameObject Light; //Свет заднего хода
    Quaternion Rot;
    int AngleCar;
    float a = 0;
    int XXX = 0;
    int YYY = 0;
    int sd;
    float ugol = 0;
    void Start()
    {
        player = (GameObject)this.gameObject;
    }
    void FixedUpdate()
    {
        player.transform.position += player.transform.forward * speed * a / 30 * Time.deltaTime;
        Coleso_LZ.transform.Rotate(Vector3.right * speedRotation * a * 2);
        Coleso_RZ.transform.Rotate(Vector3.right * speedRotation * a * 2);
        Coleso_LP.transform.Rotate(a * 2, 0, 0);
        Coleso_RP.transform.Rotate(a * 2, 0, 0);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            XXX = 1;
            if (a <= 30)
            {
                a = a + 1;
            }
            if (AngleCar <= 4)
            {
                korpus.transform.Rotate(-speedRotation / 2.8f, 0, 0);
                AngleCar += 1;
            }
            sd = 1;
        }
        if (!Input.GetKey(KeyCode.S) & (!Input.GetKey(KeyCode.W)))
        {
            if (XXX == 1)
            {
                if (a > 0)
                {
                    a = a - 1;
                }
                if (AngleCar >= 0)
                {
                    korpus.transform.Rotate(speedRotation / 2.8f, 0, 0);
                    AngleCar -= 1;
                }
            }
            if (XXX == 2)
            {
                if (a < 0)
                {
                    a = a + 1;
                }
                if (AngleCar <= 0)
                {
                    korpus.transform.Rotate(-speedRotation / 2.8f, 0, 0);
                    AngleCar += 1;
                }
            }
            if ((XXX != 1) & (XXX != 2))
            {
                a = 0;
            }
            sd = 0;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            XXX = 2;
            if (a >= -20)
            {
                a = a - 1;
            }
            if (AngleCar >= -4)
            {
                korpus.transform.Rotate(speedRotation / 2.8f, 0, 0);
                AngleCar -= 1;
            }
            sd = 1;
            if (a < 0)
            {
                Light.gameObject.SetActive(true);
            }
        }
        else
        {
            Light.gameObject.SetActive(false);
        }
        if ((!Input.GetKey(KeyCode.A)) & (!Input.GetKey(KeyCode.D)))
        {
            if (ugol > 0)
            {
                AxisColesoR.transform.Rotate(Vector3.down * -speedRotCar);
                AxisColesoL.transform.Rotate(Vector3.down * -speedRotCar);
                ugol -= 0.5f;
            }
            if (ugol < 0)
            {
                AxisColesoR.transform.Rotate(Vector3.down * speedRotCar);
                AxisColesoL.transform.Rotate(Vector3.down * speedRotCar);
                ugol += 0.5f;
            }
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.W))
            {
                YYY = 1;
                player.transform.Rotate(Vector3.down * speedRotation * a / 30);
            }
            if (Input.GetKey(KeyCode.S))
            {
                YYY = 2;
                player.transform.Rotate(Vector3.down * speedRotation * a / 30);
            }
            if (!Input.GetKey(KeyCode.S) & (!Input.GetKey(KeyCode.W)))
            {
                if (YYY == 2)
                {
                    player.transform.Rotate(Vector3.down * speedRotation * a / 30);
                }
                if (YYY == 1)
                {
                    player.transform.Rotate(Vector3.down * speedRotation * a / 30);
                }
            }
            if (ugol < 3)
            {
                AxisColesoR.transform.Rotate(Vector3.down * speedRotCar);
                AxisColesoL.transform.Rotate(Vector3.down * speedRotCar);
                ugol += 0.5f;
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.W))
            {
                YYY = 2;
                player.transform.Rotate(Vector3.up * speedRotation * a / 30);
            }
            if (Input.GetKey(KeyCode.S))
            {
                YYY = 1;
                player.transform.Rotate(Vector3.up * speedRotation * a / 30);
            }
            if (!Input.GetKey(KeyCode.S) & (!Input.GetKey(KeyCode.W)))
            {
                if (YYY == 1)
                {
                    player.transform.Rotate(Vector3.down * speedRotation * -a / 30);
                }
                if (YYY == 2)
                {
                    player.transform.Rotate(Vector3.down * speedRotation * -a / 30);
                }
            }
            if (ugol > -3)
            {
                AxisColesoR.transform.Rotate(Vector3.down * -speedRotCar);
                AxisColesoL.transform.Rotate(Vector3.down * -speedRotCar);
                ugol -= 0.5f;
            }
        }
    }
}
