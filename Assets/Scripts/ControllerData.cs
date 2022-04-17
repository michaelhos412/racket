using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using System.IO;

public class ControllerData : MonoBehaviour
{
    // Start is called before the first frame update

    // public Text devicePositionText;
    // public Text deviceVelocityText;
    // public Text deviceAngularVelocityText;
    // public Text deviceAccelerationText;
    // public Text deviceRotationText;
    // public Text deviceAngularAccelerationText;
    // public Text strokePredictionText;
 
    // public Text isRecordingText;
    private XRNode RightNode = XRNode.RightHand;
 
    private List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
    private UnityEngine.XR.InputDevice device;
    public UnityEngine.InputSystem.InputActionReference toggleReference;
    private bool devicePositionChosen;
    private Vector3 devicePositionValue = Vector3.zero;
    private Vector3 deviceVelocityValue = Vector3.zero;
    private Vector3 deviceAngularVelocityValue = Vector3.zero;
    private Vector3 deviceAccelerationValue = Vector3.zero;
    private Quaternion deviceRotationValue = Quaternion.identity;
    private Vector3 deviceAngularAccelerationValue = Vector3.zero;
    private TextWriter tw;
    public string csvFilePath; 
    private bool isRecording = false;
    private string filename;
    private float[] classifierOutput;
    private int counter;
    //Access to the hardware device and gets its information saving it in the variable device
    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(RightNode, devices);
        if(devices.Count > 0){
            device = devices[0];
        }
 
    }
 
    // Checks if the device is enable, if not it takes action and calls the GetDevice function
    void OnEnable()
    {
        if (!device.isValid)
        {
            GetDevice();
        }
    }
    // public void Awake(){
    //     toggleReference.action.started += ToggleRecording;
    // }

    // public void onDestroy(){
    //     toggleReference.action.started -= ToggleRecording;
    // }

    void Update()
    {
        if (!device.isValid)
        {
            GetDevice();
        }
 
                // capturing position changes
        InputFeatureUsage<Vector3> devicePositionsUsage = CommonUsages.devicePosition;
        InputFeatureUsage<Vector3> deviceVelocityUsage = CommonUsages.deviceVelocity;
        InputFeatureUsage<Vector3> deviceAngularVelocityUsage = CommonUsages.deviceAngularVelocity;
        InputFeatureUsage<Vector3> deviceAccelerationUsage = CommonUsages.deviceAcceleration;
        InputFeatureUsage<Quaternion> deviceRotation = CommonUsages.deviceRotation;
        InputFeatureUsage<Vector3> deviceAngularAcceleration = CommonUsages.deviceAngularAcceleration;


        if(isRecording){
            device.TryGetFeatureValue(devicePositionsUsage, out devicePositionValue);
            device.TryGetFeatureValue(deviceVelocityUsage, out deviceVelocityValue);
            device.TryGetFeatureValue(deviceAngularVelocityUsage, out deviceAngularVelocityValue);
            device.TryGetFeatureValue(deviceAccelerationUsage, out deviceAccelerationValue);
            device.TryGetFeatureValue(deviceRotation, out deviceRotationValue);
            device.TryGetFeatureValue(deviceAngularAcceleration, out deviceAngularAccelerationValue);
            
            // devicePositionText.text = devicePositionValue.ToString("F1");
            // deviceVelocityText.text = deviceVelocityValue.ToString("F1");
            // deviceAngularVelocityText.text = deviceAngularVelocityValue.ToString("F1");
            // deviceAccelerationText.text = deviceAccelerationValue.ToString("F1");
            // deviceRotationText.text = deviceRotationValue.ToString("F1");
            // deviceAngularAccelerationText.text = deviceAngularAccelerationValue.ToString("F1");

            tw.WriteLine(devicePositionValue.x.ToString("F5") + "," + devicePositionValue.y.ToString("F5") + "," + devicePositionValue.z.ToString("F5") + "," + 
                         deviceVelocityValue.x.ToString("F5") + ","  + deviceVelocityValue.y.ToString("F5") + ","  + deviceVelocityValue.z.ToString("F5") + "," +
                          deviceAngularVelocityValue.x.ToString("F5") + ","  + deviceAngularVelocityValue.y.ToString("F5") + ","  + deviceAngularVelocityValue.z.ToString("F5") + "," +
                           deviceAccelerationValue.x.ToString("F5") + ","  + deviceAccelerationValue.y.ToString("F5") + ","  + deviceAccelerationValue.z.ToString("F5") + "," +
                            deviceRotationValue.x.ToString("F5") + ","  + deviceRotationValue.y.ToString("F5") + ","  + deviceRotationValue.z.ToString("F5") + "," + deviceRotationValue.w.ToString("F5") + ',' +
                             deviceAngularAccelerationValue.x.ToString("F5") + ","  + deviceAngularAccelerationValue.y.ToString("F5") + ","  + deviceAngularAccelerationValue.z.ToString("F5"));
                
        }
        
                
    }

    public void ToggleRecording(){
        if(isRecording == false){
            // counter += 1;
            csvFilePath = CreateCSV();
            // isRecordingText.text = "stroke number: " + counter.ToString();
            
        }
        else{
            Debug.Log("recording finished");
            tw.Close();

            // isRecordingText.text = "Not recording...";
        }
        isRecording = !isRecording;
    }

    private string CreateCSV(){
        filename = Application.streamingAssetsPath + "/PredictData/predict" + counter+ ".csv";
        counter += 1;
        tw = new StreamWriter(filename, false);
        tw.WriteLine("posX, posY, posZ, velX, velY, velZ, angVelX, angVelY, angVelZ, accX, accY, accZ, rotX, rotY, rotZ, rotW, angAccX, angAccY, angAccZ");
        return filename;
    }   

}
