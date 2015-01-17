using UnityEngine;
using System.Collections;

public class Grapher : MonoBehaviour
{

    //amount of points in graph
    public int resolution = 10;
    public float size = 0.1f;
    public float scale = 1.0f;

    private int curRes;
    private float curScale;
    private ParticleSystem.Particle[] points;

    public float verticalStrech_a;
    public float horizontalStrech_b;
    public float verticalShift_c;
    public float horizontalShift_d;
    public float phase;

    public bool modify;

    public enum FunctionOption
    {
        Linear,
        Exponential,
        Exp,
        Parabola,
        Sine
    }

    private delegate float FunctionDelegate(float a, float b, float c, float d, float x);
    private static FunctionDelegate[] functionDelegates = {
		Linear,
		Exponential,
        Exp,
		Parabola,
		Sine
	};

    public FunctionOption function;

    void Start()
    {
        CreatePoints();
    }

    private void CreatePoints()
    {
        points = new ParticleSystem.Particle[resolution];

        float increment = 1f / (resolution - 1);

        curRes = resolution;
        curScale = scale;

        for (int i = 0; i < resolution; i++)
        {
            float x = scale * i * increment;
            points[i].position = new Vector3(x, 0f, 0f);
            points[i].color = new Color(x, 0f, 0f);
            points[i].size = size;
            Debug.Log("x: " + x);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (curRes != resolution || curScale != scale)
        {
            CreatePoints();
        }


        FunctionDelegate f = functionDelegates[(int)function];

        for (int i = 0; i < resolution; i++)
        {
            Vector3 p = points[i].position;
            p.y = f(verticalStrech_a, horizontalStrech_b, verticalShift_c, phase, p.x);
            //p.y = Exponential(p.x);
            Color c = points[i].color;
            c.g = p.y;
            points[i].color = c;
            points[i].position = p;
        }

        //transform.position.x = horizontalShift_d;
        transform.position = new Vector3(horizontalShift_d, 0, 0);



        particleSystem.SetParticles(points, points.Length);

    }

    private static float Linear(float a, float b, float c, float d, float x)
    {
        //y = mx + b
        return a * x + c;
    }

    private static float Exponential(float a, float b, float c, float d, float x)
    {
        //y = a*base^x +c
        return a * Mathf.Pow(b, x) + c - 1;
    }

    //a = vert strech, b = horiz strech , c = vert shift, d = horiz shift
    private static float Exp(float a, float b, float c, float d, float x)
    {
        return a * Mathf.Exp(b * x ) + c;
    }

    private static float Parabola(float a, float b, float c, float d, float x)
    {
        x = 2f * x - 1f;
        return a * Mathf.Pow(x, 2) + b * x + c;
    }

    //sine function w/ animation
    private static float Sine(float a, float b, float c, float d, float x)
    {
        //return 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * x + Time.timeSinceLevelLoad);
        return a * Mathf.Sin(b * Mathf.PI * x + d) + c;
    }


}
