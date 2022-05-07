using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormAnalysis : MonoBehaviour
{
    public StrokeClassifier strokeClassifier;
    public ControllerData controllerData;
    public GameObject shuttlecock; 
    public GameObject arrowHelper;
    public Transform spawnPoint;
    private Rigidbody shuttlecockRB;

    public GameObject correctCanvas;
    public GameObject hammergripCanvas;
    public GameObject elbowStrainCanvas;
    public GameObject noElbowCanvas;
    
    public UnityEngine.InputSystem.InputActionReference toggleReference;
    public enum StrokeClasses{  
        Correct = 0,
        Hammergrip = 1,
        ElbowStrain = 2,
        NoElbowSwing = 3,
    }
    private int counter = 1;
    private StrokeClasses strokePrediction;
    private float[] classifierOutput;

    // Start is called before the first frame update
    void Start()
    {
        arrowHelper.SetActive(true);
        shuttlecock.SetActive(true);
    }

    public void Awake(){
        toggleReference.action.started += SpawnAndLaunchShuttlecock;
    }

    public void onDestroy(){
        toggleReference.action.started -= SpawnAndLaunchShuttlecock;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnAndLaunchShuttlecock(UnityEngine.InputSystem.InputAction.CallbackContext context){
        DisableStrokeMistakeCanvas();
        shuttlecockRB = Instantiate(shuttlecock, spawnPoint.position, spawnPoint.rotation).GetComponent<Rigidbody>();
        shuttlecockRB.velocity = spawnPoint.TransformDirection(new Vector3(0, 12, 5.6f));
        shuttlecockRB.useGravity = true;
        // controllerData.ToggleRecording();
        //Invoke("StartRecording", 0.5f);        
        Invoke("StopRecordAndPredict", 2.2f);
    }
    private void StartRecording(){
        controllerData.ToggleRecording();
    }
    private void StopRecordAndPredict(){
        //controllerData.ToggleRecording();
        //classifierOutput = strokeClassifier.PredictFromCSV(controllerData.csvFilePath);
        strokePrediction = (StrokeClasses) counter;
        //Debug.Log(string.Join(", ", classifierOutput));
        EnableStrokeMistakeCanvas(strokePrediction);
        counter = (counter + 1) % 4;
    }

    private void DisableStrokeMistakeCanvas(){
        noElbowCanvas.SetActive(false);
        hammergripCanvas.SetActive(false);
        correctCanvas.SetActive(false);
        elbowStrainCanvas.SetActive(false);
        
    }
    private void EnableStrokeMistakeCanvas(StrokeClasses strokePrediction){
        switch (strokePrediction){
            case StrokeClasses.Correct:
                correctCanvas.SetActive(true);
                break;
            case StrokeClasses.Hammergrip:
                hammergripCanvas.SetActive(true);
                break;
            case StrokeClasses.ElbowStrain:
                elbowStrainCanvas.SetActive(true);
                break;
            case StrokeClasses.NoElbowSwing:
                noElbowCanvas.SetActive(true);
                break;
            default:
                break;
        }
    }
}
