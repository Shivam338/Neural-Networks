using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrainingSet
{
    public double[] input;
    public double output;
}

public class Percepteron : MonoBehaviour
{
    public TrainingSet[] ts;

    double[] weights = { 0, 0 };
    double bias = 0;
    double totalError = 0;

    double DotProductBias(double[] v1, double[] v2)
    {
        if (v1 == null || v2 == null)
            return -1;

        if (v1.Length != v2.Length)
            return -1;

        double d = 0;
        for(int i=0; i<v1.Length; i++)
        {
            d += v1[i] * v2[i];
        }
        d += bias;
        return d;
    }

    double CalcOutput(int i)
    {
        double dp = DotProductBias(weights, ts[i].input);
        if (dp > 0)
            return (1);
        return (0);
    }

    void UpdateWeights(int j)
    {
        double error = ts[j].output - CalcOutput(j);
        totalError += Mathf.Abs((float)error);
        for(int i =0; i < weights.Length; i++)
        {
            weights[i] = weights[i] + error * ts[j].input[i];
        }
        bias += error;
    }

    void InitialiseWeight()
    {
        for(int i=0; i< weights.Length; i++)
        {
            weights[i] = Random.Range(-1.0f, 1.0f);
        }
        bias = Random.Range(-1.0f, 1.0f);
    }

    double CalcOutput(double i1, double i2)
    {
        double[] inp = new double[] { i1, i2 };
        double dp = DotProductBias(weights, inp);
        if (dp >= 0)
            return (1);
        return (0);
    }

    void Train(int generations)
    {
        InitialiseWeight();
        for(int i =0; i< generations; i++)
        {
            totalError = 0;
            for(int j =0; j < ts.Length; j++)
            {
                UpdateWeights(j);
                Debug.Log("W1 : " + (weights[0]) + " W2: " + (weights[1]) + " B: " + bias);
            }
            Debug.Log("TOTAL ERROR: " + totalError);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Train(8);
        Debug.Log("Test 0 0 : " + CalcOutput(0, 0));
        Debug.Log("Test 0 1 : " + CalcOutput(0, 1));
        Debug.Log("Test 1 0 : " + CalcOutput(1, 0));
        Debug.Log("Test 1 1 : " + CalcOutput(1, 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
