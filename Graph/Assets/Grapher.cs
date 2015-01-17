using UnityEngine;
using System.Collections;

public class Grapher : MonoBehaviour {

    //amount of points in graph
    public int resolution = 10;
    public float size = 0.1f;

    private int curRes;
    private ParticleSystem.Particle[] points;

    public float verticalStrech;
    public float horizontalStrech;
    public float verticalShift;
    public float horizontalShift;

    public enum FunctionOption
    {
        Linear,
        Exponential,
        Parabola,
        Sine
    }

    private delegate float FunctionDelegate(float a, float b, float c, float d, float x);
    private static FunctionDelegate[] functionDelegates = {
		Linear,
		Exponential,
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

        for (int i = 0; i < resolution; i++)
        {
            float x = i * increment;
            points[i].position = new Vector3(x, 0f, 0f);
            points[i].color = new Color(x, 0f, 0f);
            points[i].size = size;
        }
    }


	// Update is called once per frame
	void Update () 
    {
        if (curRes != resolution)
        {
            CreatePoints();
        }

        FunctionDelegate f = functionDelegates[(int)function];

        for (int i = 0; i < resolution; i++)
        {
            Vector3 p = points[i].position;
            p.y = f(verticalStrech, horizontalStrech, verticalShift, horizontalShift,p.x);
            //p.y = Exponential(p.x);
            points[i].position = p;
        }
        
        particleSystem.SetParticles(points, points.Length);
	
	}

    private static float Linear(float a, float b, float c, float d, float x)
    {
        return a*x;
    }

    private static float Exponential(float a, float b, float c, float d, float x)
    {
        return Mathf.Pow(a,b*(x + d)) + c;
    }

    //a = vert strech, b = horiz strech , c = vert shift, d = horiz shift
    private static float ExponentialGen(float a, float b, float c, float d, float x)
    {
        return a * Mathf.Exp(b*x + d) + c;
    }

    private static float Parabola(float a, float b, float c, float d, float x)
    {
        x = 2f * x - 1f;
        return x * x;
    }

    //sine function w/ animation
    private static float Sine(float a, float b, float c, float d, float x)
    {
        return 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * x + Time.timeSinceLevelLoad);
    }


}
