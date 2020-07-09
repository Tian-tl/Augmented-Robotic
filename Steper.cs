using UnityEngine;
using System.Collections;
using Uniduino;
using UnityEngine.UI;


#if (UNITY_3_0 || UNITY_3_0_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5) 
public class Servo : Uniduino.Examples.Servo { } // for unity 3.x
#endif

namespace Uniduino.Examples
{

    public class Steper : MonoBehaviour
    {
        public Arduino arduino;

        int dirPin = 3;
        int stepPin = 2;
        int step = 0;
        int stepLimit = 100;

        // The time delay between changing each pin value
        public float motor_TimeDelay = 0.0005f;

        int i;

        void Start()
        {
            // Conifugre the pins
            arduino.Setup(ConfigurePins);

            //StartCoroutine(loop());
        }

        // Configure those pins!  Make sure you get the Positive and Negative wires/pins right!
        void ConfigurePins()
        {
            arduino.pinMode(dirPin, PinMode.OUTPUT);
            arduino.pinMode(stepPin, PinMode.OUTPUT);

        }

        void Update()
        {

            //InvokeRepeating("loop", 0f, 0.05f);
            //StartCoroutine(loop());

            if (Input.GetKey(KeyCode.Space) && step < stepLimit)
            {
                arduino.digitalWrite(dirPin, 1);
                for (int i = 0; i < 30; i++)
                {

                    arduino.digitalWrite(stepPin, 1);

                    arduino.digitalWrite(stepPin, 0);
                }
                step++;
            }
            else if (step > 0)
            {

                arduino.digitalWrite(dirPin, 0);
                for (int i = 0; i < 30; i++)
                {

                    arduino.digitalWrite(stepPin, 1);

                    arduino.digitalWrite(stepPin, 0);
                }
                step--;
            }

            /* arduino.digitalWrite(dirPin, 0);
             for (int i = 0; i < 1500; i++)
             {
                 arduino.digitalWrite(stepPin, 1);

                 arduino.digitalWrite(stepPin, 0);

             }*/
        }

        /*IEnumerator loop()  
        {
            while (true)
            {

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("Rotating!");
                    arduino.digitalWrite(dirPin, 1);

                    arduino.digitalWrite(stepPin, 1);
                    yield return new WaitForSeconds(motor_TimeDelay);
                    arduino.digitalWrite(stepPin, 0);
                    yield return new WaitForSeconds(motor_TimeDelay);
                }
                else {
                
                    Debug.Log("Not rotating!");


                }
            }
        }*/
    }

}