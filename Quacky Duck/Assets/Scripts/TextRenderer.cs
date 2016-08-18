using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TextRenderer : MonoBehaviour {
	
	void Start () 
	{
		//A magical hack to allow a 3D text object to function within Unity 2Ds sorting layers
		GetComponent<Renderer>().sortingLayerName = "Foreground";
		GetComponent<Renderer>().sortingOrder = 1;	
	}
}
