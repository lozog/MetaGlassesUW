//linear function
private static float Linear (float x) {
	return x;
}
//simple parabola
private static float Exponential (float x) {
	return x * x;
}
//2 sided parabola
private static float Parabola (float x){
	x = 2f * x - 1f;
	return x * x;
}
//sine function w/ animation
private static float Sine (float x){
	return 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * x + Time.timeSinceLevelLoad);
}

//cos function : "y= [sin(2pi*x +delta t) + 1]/2
private static float Cos (float x){
	return 0.5f + 0.5f * Mathf.Cos(2 * Mathf.PI * x + Time.timeSinceLevelLoad);
}

private static float Tan (float x){
	return 0.5f + 0.5f * Mathf.Tan(2 * Mathf.PI * x + Time.timeSinceLevelLoad);
}


//a: vertical stretch; b: horizontal shift; c: vertical shift;
private static float Cubic (float a, float b, float c, float x) {
	float p1 =1;
	for (int i=0; i < 3; ++i){
		p1 *= (x-B);
	}
	return a*p1+c;
}