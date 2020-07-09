using UnityEngine;
using System.Collections;
using Uniduino;
using UnityEngine.UI;

#if (UNITY_3_0 || UNITY_3_0_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5)		
public class Servo : Uniduino.Examples.Servo { } // for unity 3.x
#endif

namespace Uniduino.Examples
{

    public class ServoCam : MonoBehaviour
    {

        public Arduino arduino;
        GameObject arduino2;
        public Slider sliderAxis_01;
        public Text ValueAxis_01;
        string overwriteAXIS_01;
        public Slider sliderAxis_02;
        public Text ValueAxis_02;
        string overwriteAXIS_02;
        public Slider sliderAxis_03;
        public Text ValueAxis_03;
        string overwriteAXIS_03;
        public int a;
        public int b;
        public int c;
        public int d;

        public int pin00 = 7;
        public int pin01 = 8;
        public int pin02 = 9;
        public int pin03 = 10;

        void Start()
        {

            arduino2 = GameObject.Find("Uniduino");
            arduino = arduino2.GetComponent<Arduino>();
            //arduino = Arduino.global;
            arduino.Setup(ConfigurePins);

            StartCoroutine(loop());


        }

        void ConfigurePins()
        {
            arduino.pinMode(pin00, PinMode.SERVO);
            arduino.pinMode(pin01, PinMode.SERVO);
            arduino.pinMode(pin02, PinMode.SERVO);
            arduino.pinMode(pin03, PinMode.SERVO);

        }

        private void Update()
        {
            overwriteAXIS_01 = a.ToString();
            ValueAxis_01.text = (overwriteAXIS_01);
            overwriteAXIS_02 = b.ToString();
            ValueAxis_02.text = (overwriteAXIS_02);
            overwriteAXIS_03 = c.ToString();
            ValueAxis_03.text = (overwriteAXIS_03);
        }


        IEnumerator loop()
        {
            while (true)

            {

                a = (int)(transform.eulerAngles.x);
                b = (int)(transform.eulerAngles.y);
                c = (int)(transform.eulerAngles.z);
                d = (int)((transform.eulerAngles.z) / 4);

                if (a > 180)

                    a = (a / 2 - 90);

                else

                    a = (a / 2 + 90);

                if (b > 180)

                    b = (b / 2 - 90);

                else

                    b = (b / 2 + 90);

                if (c > 180)

                    c = (c / 2 - 90);

                else

                    c = (c / 2 + 90);

                // a = (int)(-90 + (a - 0) * (90 - 0) / (360 - 0))+90;
                //b = (int)(-90 + (b - 0) * (90 - 0) / (360 - 0)) + 90;
                //c = (int)(-90 + (c - 0) * (90 - 0) / (360 - 0)) + 90;
                // print(transform.eulerAngles.z);

                /*if (a > 180)

                { a = a-180; }

                else

                { a=a+1; }

                if (b > 180)

                { b = b-180; }

                else

                { b = b + 1; }

                if (c > 180)

                { c = c - 180; }

                else

                { c = c + 1; }

                /*if(a<0)

                { a = 0; }

                if (a > 180)

                { a = 180; }

                if (b < 0)

                { b = 0; }

                if (b > 180)

                { b = 180; }*/

                // print(a);
                //print(b);
                //print(c);

                arduino.analogWrite(pin00, (a));
                yield return new WaitForSeconds(0.005F);

                arduino.analogWrite(pin01, (b));
                yield return new WaitForSeconds(0.005F);


                //arduino.analogWrite(pin01, 180);
                // yield return new WaitForSeconds(1);


                arduino.analogWrite(pin02, (c));
                yield return new WaitForSeconds(0.005F);

                arduino.analogWrite(pin03, (d));
                yield return new WaitForSeconds(0.005F);


                sliderAxis_01.value = Mathf.MoveTowards(a, 100.0f, 0.15f);
                sliderAxis_02.value = Mathf.MoveTowards(b, 100.0f, 0.15f);
                sliderAxis_03.value = Mathf.MoveTowards(c, 100.0f, 0.15f);


            }
        }
    }
}