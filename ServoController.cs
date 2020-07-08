using UnityEngine;
using System.Collections;
using Uniduino;
using UnityEngine.UI;

#if (UNITY_3_0 || UNITY_3_0_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5)		
public class Servo : Uniduino.Examples.Servo { } // for unity 3.x
#endif

namespace Uniduino.Examples
{

    public class ServoController : MonoBehaviour
    {
        public int _duration = 90;
        public int _timer = 0;
        public float _increment = 0.01f;

        //TEXT//
        public Text trigger;
        public Text padPress;
        public Text Gripper;
        string overwrite;
        string overwrite02;
        public Slider sliderTemp;
        public Arduino arduino;
        GameObject arduino2;
        public Slider sliderAxis_04;
        public Text ValueAxis_04;
        string overwriteAXIS_04;
        public Slider sliderAxis_05;
        public Text ValueAxis_05;
        string overwriteAXIS_05;
        public Slider sliderAxis_06;
        public Text ValueAxis_06;
        string overwriteAXIS_06;

        public int a;
        public int b;
        public int c;


        public int pin00 = 7;
        public int pin01 = 8;
        public int pin02 = 9;
        public int pin03 = 10;
        public int pinR0 = 0;
        public int pinPhotoresist;

        void Start()
        {
            //arduino = Arduino.global;

            arduino2 = GameObject.Find("Uniduino1");
            arduino = arduino2.GetComponent<Arduino>();
            arduino.Setup(ConfigurePins);
            InvokeRepeating("OutputTime", _increment, _increment);
            StartCoroutine(loop());

        }





        void OutputTime()


        {

            if (GameObject.Find("Controller (left)").GetComponent<SteamVR_TrackedController>().triggerPressed)
            {
                _timer += 1;
            }

            else

                _timer -= 1;


            if (_timer > _duration)

                _timer = _duration;

            if (_timer < 0)

                _timer = 0;
        }

        

        void ConfigurePins()
        {
            arduino.pinMode(pin00, PinMode.SERVO);
            arduino.pinMode(pin01, PinMode.SERVO);
            arduino.pinMode(pin02, PinMode.SERVO);
            arduino.pinMode(pin03, PinMode.SERVO);
            arduino.pinMode(0, PinMode.ANALOG);
            arduino.reportAnalog(0, 1);
        }

        private void Update()

        {
            overwriteAXIS_04 = a.ToString();
            ValueAxis_04.text = (overwriteAXIS_04);
            overwriteAXIS_05 = b.ToString();
            ValueAxis_05.text = (overwriteAXIS_05);
            overwriteAXIS_06 = c.ToString();
            ValueAxis_06.text = (overwriteAXIS_06);

            if (GameObject.Find("Controller (left)").GetComponent<SteamVR_TrackedController>().triggerPressed)
            {
                trigger.text = "ON";


            }

            else

                trigger.text = "OFF";

            Debug.Log((GameObject.Find("Controller (left)").GetComponent<SteamVR_TrackedController>().triggerPressed));




            if (GameObject.Find("Controller (left)").GetComponent<SteamVR_TrackedController>().padPressed)
            {
                padPress.text = "RIGHT";


            }

            else

                padPress.text = "OFF";

            Debug.Log((GameObject.Find("Controller (left)").GetComponent<SteamVR_TrackedController>().padPressed));



            if (GameObject.Find("Controller (left)").GetComponent<SteamVR_TrackedController>().gripped)
            {
                Gripper.text = "LEFT";


            }

            else

                Gripper.text = "OFF";

            Debug.Log((GameObject.Find("Controller (left)").GetComponent<SteamVR_TrackedController>().gripped));




        }


        IEnumerator loop()
        {
            while (true)

            {

                a = (int)(transform.localEulerAngles.x);
                b = (int)(transform.localEulerAngles.y);
                  c = (int)((transform.localEulerAngles.z)/4);
                //c = 0;








                // print(transform.eulerAngles.z);



                if ((a >= 0) && (a <= 180))

                {
                    a = a;

                }

                else
                {

                    a = 360 - a;

                }


                if ((b >= 270) && (b <= 360))

                {
                    b = b - 180;


                }

                else
                {

                    b = 180 - b / 3;

                }




                /*if (GameObject.Find("Controller (left)").GetComponent<SteamVR_TrackedController>().triggerPressed)
                  {
                      if ((c >= 0) && (c <= 180))
                      {
                          c = c + 1;
                          yield return new WaitForSeconds(0.005F);
                      }
                      else
                      {

                          c = 0;

                      }

                  }



                  Debug.Log((GameObject.Find("Controller (left)").GetComponent<SteamVR_TrackedController>().triggerPressed));*/





               if ((c >= 0) && (c <= 180))

                  {
                      c = c;

                  }

                  else
                  {

                      c = 360 - c;

                  }


                /*  c = (int)(transform.localPosition.x);

                     double d;
                     d = Mathf.Abs ( c / Mathf.Cos(b));

                     d = d * 450;

                 if ((d >= 0) && (d <= 90))

                     {
                         d = d;

                     }

                     else
                     {

                         d = 900 - a;

                     }*/


                arduino.analogWrite(pin00, (180 - a));
                yield return new WaitForSeconds(0.005F);

                arduino.analogWrite(pin01, (180 - b));
                yield return new WaitForSeconds(0.005F);


                arduino.analogWrite(pin02, (c));
                yield return new WaitForSeconds(0.005F);

                arduino.analogWrite(pin03, (_timer));
                yield return new WaitForSeconds(0.005F);

                yield return new WaitForSeconds(0.005F);

                pinPhotoresist = (arduino.analogRead(pinR0));
                yield return new WaitForSeconds(0.005F);





                sliderAxis_04.value = Mathf.MoveTowards(a, 100.0f, 0.15f);
                sliderAxis_05.value = Mathf.MoveTowards(b, 100.0f, 0.15f);
                sliderAxis_06.value = Mathf.MoveTowards(c, 100.0f, 0.15f);


            }
        }
    }
}