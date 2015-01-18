using UnityEngine;
using System.Collections;

public class Grapher2 : MonoBehaviour
{
    private ParticleSystem.Particle[] points;

    public float verticalStrech_a;
    public float horizontalStrech_b;
    public float verticalShift_c;
    public float horizontalShift_d;
    public float phase;

    public float r, xrad, yrad, zrad;

    public float scale = 1;
    public float size = 0.1f;

    [Range(10, 100)]
    public int resolution = 10;

    public enum FunctionOption
    {
        Linear,
        Exponential,
        Parabola,
        Sine,
        CSine,
        Ripple,
        Ellipsoid,
        Cone,
        Sheethyperboloid,

    }
    public FunctionOption function;

    private int currentResolution;
    private float curScale;
    private float curSize;

    private void CreatePoints()
    {
        currentResolution = resolution;
        curScale = scale;
        curSize = size;
        points = new ParticleSystem.Particle[resolution * resolution];
        float increment = 1f / (resolution - 1);
        int i = 0;
        for (int x = 0; x < resolution; x++)
        {
            for (int z = 0; z < resolution; z++)
            {
                Vector3 p = new Vector3( x * increment, 0f,  z * increment) * scale;
                points[i].position = p;
                points[i].color = new Color(p.x, 0f, p.z);
                points[i++].size = size ;
            }
        }
    }

    void Update()
    {
        if (currentResolution != resolution || curScale != scale || curSize != size || points == null)
        {
            CreatePoints();
        }
        //FunctionDelegate f = functionDelegates[(int)function];

        transform.position = new Vector3(horizontalShift_d, transform.position.y, transform.position.z);

        float t = Time.timeSinceLevelLoad;
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 p = points[i].position;

            //Function check
            if (function == FunctionOption.Linear)
            {
                p.y = Linear(p, t, verticalStrech_a, verticalShift_c);
            }
            else if (function == FunctionOption.Exponential)
            {
                p.y = Exponential(p, t, verticalStrech_a, verticalShift_c);
            }
            else if (function == FunctionOption.Parabola)
            {
                p.y = Parabola(p, t, xrad, zrad, yrad, verticalShift_c);
            }
            else if (function == FunctionOption.Ellipsoid)
            {
                p.y = Ellipsoid(p, t, r , xrad, zrad, yrad, verticalShift_c);
            }
            else if (function == FunctionOption.Cone)
            {
                p.y = Cone(p, t, yrad, xrad, zrad, verticalShift_c);
            }
            else if (function == FunctionOption.Sheethyperboloid)
            {
                p.y = Sheethyperboloid(p, t, xrad, zrad, yrad, verticalShift_c);
            }
            else if (function == FunctionOption.Sine)
            {
                p.y = Sine(p, t, verticalShift_c);//demo
            }
            else if (function == FunctionOption.CSine)
            {
                p.y = CSine(p, t, verticalShift_c);//demo
            }
            else if (function == FunctionOption.Ripple)
            {
                p.y = Ripple(p, t , verticalShift_c);//demo
            }
            
            points[i].position = p;
            Color c = points[i].color;
            c.g = p.y;
            points[i].color = c;
        }
        particleSystem.SetParticles(points, points.Length);
    }


    //linear plane, variable dim// a is amplitude, c is vertical shift
    private float Linear(Vector3 p, float t, float a, float c)
    {
        return p.x * a + c;
    }
    //1 sided parabola 1 variable plane, variable dim, a is amplitude, c is vertical shift
    private float Exponential(Vector3 p, float t, float a, float c)
    {

        return (p.x * p.x) * a + c;
    }
    //paraboloid, variable dim, xr is x radius modifyer, zr is z radius modifier, c is vertical shift,yr is vertical stretch
    private float Parabola(Vector3 p, float t, float xr, float zr, float yr, float c)
    {
        function = FunctionOption.Parabola;
        p.x = 2f * p.x - 1f;
        p.z = 2f * p.z - 1f;

        return c + ((p.x * p.x) / (xr * xr)) + ((p.z * p.z) / (zr * zr));
    }

    //find way to get bottom half too
    //ellipsoid 3d surface, variable dimensions// r = radius, xr = x rad stretch, zr = z rad stretch, yr = y rad stretch
    private float Ellipsoid(Vector3 p, float t, float r, float xr, float zr, float yr, float c)
    {
        function = FunctionOption.Ellipsoid;
        p.x = 2f * p.x - 1f;
        p.z = 2f * p.z - 1f;
        return Mathf.Sqrt(r * r - ((p.x * p.x) / (xr * xr)) - ((p.z * p.z) / (zr * zr))) * yr + c;
    }

    //find way to get bottom half
    //cone, variable dimensions//  xr = x rad stretch, zr = z rad stretch, yr = y rad stretch
    private float Cone(Vector3 p, float t, float yr, float xr, float zr , float c)
    {
        function = FunctionOption.Cone;
        p.x = 2f * p.x - 1f;
        p.z = 2f * p.z - 1f;
        return Mathf.Sqrt(((p.x * p.x) / (xr * xr)) + ((p.z * p.z) / (zr * zr))) * yr + c;
    }

    //need to get bottom half
    //1sheet hyperboloid, variable dimensions//r = radius, xr = x rad stretch, zr = z rad stretch, yr = y rad stretch
    private float Sheethyperboloid(Vector3 p, float t, float xr, float zr, float yr, float c)
    {
        function = FunctionOption.Sheethyperboloid;
        p.x = 2f * p.x - 1f;
        p.z = 2f * p.z - 1f;
        return Mathf.Sqrt((1f + ((p.z * p.z) / (zr * zr)) - ((p.x * p.x) / (xr * xr)))) * yr + c;
    }


    /////////animated graphs are below:

    //1 variable sine plane, anim
    private float Sine(Vector3 p, float t , float c)
    {
        function = FunctionOption.Sine;
        return 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * p.x + t) + c;
    }

    //complicated multi sine plane, anim
    private float CSine(Vector3 p, float t, float c)
    {
        function = FunctionOption.CSine;
        return 0.50f +
            0.25f * Mathf.Sin(4 * Mathf.PI * p.x + 4 * t) * Mathf.Sin(2 * Mathf.PI * p.z + t) +
                0.10f * Mathf.Cos(3 * Mathf.PI * p.x + 5 * t) * Mathf.Cos(5 * Mathf.PI * p.z + 3 * t) +
                0.15f * Mathf.Sin(Mathf.PI * p.x + 0.6f * t) + c;
    }
    //cool ripple plane, anim
    private float Ripple(Vector3 p, float t, float c)
    {
        function = FunctionOption.Ripple;
        p.x -= 0.5f;
        p.z -= 0.5f;
        float squareRadius = p.x * p.x + p.z * p.z;
        return 0.5f + Mathf.Sin(15f * Mathf.PI * squareRadius - 2f * t) / (2f + 100f * squareRadius) + c;
    }

}