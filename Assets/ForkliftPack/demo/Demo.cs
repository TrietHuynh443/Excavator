using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour {

	void OnGUI(){
		GUI.Label (new Rect (10, 50, 800, 30), "Press I to raise, K to lower the fork, L and J to bend the mechanism! See Forklift.cs for details!");
	}
}
