﻿using UnityEngine;
using System.Collections;

public struct Pattern {
    public int paso1, paso2, paso3;
}

public class Spawner : MonoBehaviour {
    public Manager manager;
    
	// Use this for initialization
	void Awake () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InstanceObject(GameObject type) {
        type.transform.position = transform.position;
    }

#if UNITY_EDITOR
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(this.transform.position, this.transform.localScale);
    }
#endif
}
