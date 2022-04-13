using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using TensorFlowLite;
    public class StrokeClassifier : MonoBehaviour
{

    Interpreter interpreter;

    bool isProcessing = false;
    float[,] inputs = new float[200,16];
    float[] outputs;

    string[] classes = {"correct","hammergrip","elbow_strain","no_elbow_swing"};

    void Start()
    {
        byte[] model_byte = System.IO.File.ReadAllBytes(Application.streamingAssetsPath + "/model.tflite");
        var options = new InterpreterOptions()
            {
                threads = 2
            };

        interpreter = new Interpreter(model_byte, options);
        var inputInfo = interpreter.GetInputTensorInfo(0);
        var outputInfo = interpreter.GetOutputTensorInfo(0);
        
        Debug.Log(inputInfo);
        Debug.Log(outputInfo);

        outputs = new float[outputInfo.shape[1]];
        interpreter.ResizeInputTensor(0, new int[] { 1, 200, 16 });
        interpreter.AllocateTensors();
        


    }

    void OnDestroy()
    {
        interpreter?.Dispose();
    }

    public float[] PredictFromCSV(string path)
    {
        if (!isProcessing)
        {
            ReadAndProcessCSV(path);
            Invoke();
            
            return outputs;
        }
        return new float[0];
    }

    void Invoke()
    {
        isProcessing = true;

        float startTime = Time.realtimeSinceStartup;
        interpreter.SetInputTensorData(0, inputs);
        interpreter.Invoke();
        interpreter.GetOutputTensorData(0, outputs);
        float duration = Time.realtimeSinceStartup - startTime;

        Debug.Log($"Process time: {duration: 0.00000} sec");
        Debug.Log("---");
        for (int i = 0; i < outputs.Length; i++)
        {
            Debug.Log($"{i}: {outputs[i]: 0.00}");
        }

        isProcessing = false;
    }

    public string GetPredictionClass(){
        float maxValue = outputs.Max();
        int maxIndex = outputs.ToList().IndexOf(maxValue);
        return classes[maxIndex];
    }
    public void ReadAndProcessCSV(string path){
        StreamReader strReader = new StreamReader(path);
        List<float[]> data = new List<float[]>();  
        bool endOfFile = false;
        strReader.ReadLine(); // skip header 
        while(!endOfFile){
            string dataString = strReader.ReadLine();
            if (dataString == null){
                endOfFile = true;
                break;
            }
            string[] dataStringArray = dataString.Split(',');
            List<string> row_data = new List<string>();
            for (int i = 0; i < dataStringArray.Count(); i++){
                if(2 < i && i < 6){
                    continue;
                }
                row_data.Add(dataStringArray[i]);
            } 
            float[] float_row_data = row_data.Select(float.Parse).ToArray();
            // Debug.Log(string.Join(", ", float_row_data));

            data.Add(float_row_data);
        }

        PopulateInput(data);
    }

    private int GetS(int j, int L, int L_prime){
        var term = j*L/L_prime;
        if (term < 1){
            return 1;
        }

        else if (1.0 <= term && term <= L) {
            return (int) Mathf.Floor(term);
        }
        
        else if(term > L) {
            return L;
        }
        return 0;
    }

    private void PopulateInput(List<float[]> data){
        int L = data.Count();
        int L_prime = 200;
        for (int j = 0; j < L_prime; j++){
            for (int i = 0; i < 16; i++) {
                int term_1 = GetS(j, L, L_prime);
                float term_2 = j*L/L_prime;
                float term_3 = data[term_1][i];
                float term_4 = term_2*(term_3);
                float recalculated_feature = term_1 + 1 - term_4 + (term_2 - term_1) * term_3;
                inputs[j,i] = recalculated_feature;
            }
        }
    }

}

