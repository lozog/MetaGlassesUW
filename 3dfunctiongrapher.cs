
using UnityEngine;

public class Grapher2 : MonoBehaviour {
	
	public enum FunctionOption {
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
	
	private delegate float FunctionDelegate (Vector3 p, float t);
	private static FunctionDelegate[] functionDelegates = {
		Linear,
		Exponential,
		Parabola,
		Sine,
		CSine,
		Ripple,
		Ellipsoid,
		Cone,
		Sheethyperboloid, //1 sheet hyperboloid
	};
	
	public FunctionOption function;
	
	[Range(10, 100)]
	public int resolution = 10;
	
	private int currentResolution;
	private ParticleSystem.Particle[] points;
	
	private void CreatePoints () {
		currentResolution = resolution;
		points = new ParticleSystem.Particle[resolution * resolution];
		float increment = 1f / (resolution - 1);
		int i = 0;
		for (int x = 0; x < resolution; x++) {
			for (int z = 0; z < resolution; z++) {
				Vector3 p = new Vector3(x * increment, 0f, z * increment);
				points[i].position = p;
				points[i].color = new Color(p.x, 0f, p.z);
				points[i++].size = 0.1f;
			}
		}
	}
	
	void Update () {
		if (currentResolution != resolution || points == null) {
			CreatePoints();
		}
		FunctionDelegate f = functionDelegates[(int)function];
		float t = Time.timeSinceLevelLoad;
		for (int i = 0; i < points.Length; i++) {
			Vector3 p = points[i].position;
			p.y = f(p, t);
			points[i].position = p;
			Color c = points[i].color;
			c.g = p.y;
			points[i].color = c;
		}
		particleSystem.SetParticles(points, points.Length);
	}


	//linear plane, variable dim// a is amplitude, c is vertical shift
	private static float Linear (Vector3 p, float t, float a, float c) {
		return p.x*a+c;
	}
	//1 sided parabola 1 variable plane, variable dim, a is amplitude, c is vertical shift
	private static float Exponential (Vector3 p, float t,float a, float c) {
		return (p.x * p.x)*a+c;
	}
	//paraboloid, variable dim, xr is x radius modifyer, zr is z radius modifier, c is vertical shift,yr is vertical stretch
	private static float Parabola (Vector3 p, float t, float xr, float zr, float yr, float c){
		p.x = 2f * p.x - 1f;
		p.z = 2f * p.z - 1f;

		return c + ( (p.x * p.x)/(xr*xr) ) + ( (p.z * p.z)/(zr*zr) );
	}
	
	//find way to get bottom half too
	//ellipsoid 3d surface, variable dimensions// r = radius, xr = x rad stretch, zr = z rad stretch, yr = y rad stretch
	private static float Ellipsoid (Vector3 p, float t,float r, float xr, float zr,float yr){
				p.x = 2f * p.x - 1f;
				p.z = 2f * p.z - 1f;
		return Mathf.Sqrt(r*r - ( (p.x * p.x)/(xr*xr) ) - ( (p.z * p.z)/(zr*zr) ))*yr;
		}

	//find way to get bottom half
	//cone, variable dimensions//  xr = x rad stretch, zr = z rad stretch, yr = y rad stretch
	private static float Cone (Vector3 p, float t,float yr, float xr, float zr){
				p.x = 2f * p.x - 1f;
				p.z = 2f * p.z - 1f;
		return Mathf.Sqrt( ((p.x * p.x)/(xr*xr)) + ((p.z * p.z)/(zr*zr)) ) * yr;
		}

	//need to get bottom half
	//1sheet hyperboloid, variable dimensions//r = radius, xr = x rad stretch, zr = z rad stretch, yr = y rad stretch
	private static float Sheethyperboloid (Vector3 p, float t, float xr, float zr,float yr){
		p.x = 2f * p.x - 1f;
		p.z = 2f * p.z - 1f;
		return Mathf.Sqrt((1f + ( (p.z * p.z)/(zr*zr) ) - ((p.x * p.x)/(xr*xr))))*yr;
	}


	/////////animated graphs are below:

	//1 variable sine plane, anim
	private static float Sine (Vector3 p, float t){
		return 0.5f + 0.5f * Mathf.Sin (2 * Mathf.PI * p.x + t);
	}
	
	//complicated multi sine plane, anim
	private static float CSine (Vector3 p, float t){
		return 0.50f +
			0.25f * Mathf.Sin(4 * Mathf.PI * p.x + 4 * t) * Mathf.Sin(2 * Mathf.PI * p.z + t) +
				0.10f * Mathf.Cos(3 * Mathf.PI * p.x + 5 * t) * Mathf.Cos(5 * Mathf.PI * p.z + 3 * t) +
				0.15f * Mathf.Sin(Mathf.PI * p.x + 0.6f * t);
	}
	//cool ripple plane, anim
	private static float Ripple (Vector3 p, float t){
		p.x -= 0.5f;
		p.z -= 0.5f;
		float squareRadius = p.x * p.x + p.z * p.z;
		return 0.5f + Mathf.Sin(15f * Mathf.PI * squareRadius - 2f * t) / (2f + 100f * squareRadius);
	}
	
}