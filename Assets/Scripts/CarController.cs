﻿using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {
   
    public Manager manager;
    public Transform camara;
    private Vector3 direccion, posicion;
    private Quaternion origRot;
    private float velocidad, velocidadRot;
    private float tiempo;
    private bool giro, golpeIzquierda, golpeDerecha;

    // Use this for initialization
    void Awake () {
        direccion = this.transform.rotation.eulerAngles;
        posicion = this.transform.position;
        origRot = this.transform.rotation;
        tiempo = 0.0f;
    }

    void Start() {
        giro = true;
    }
    //cambio
    // Update is called once per frame
    void FixedUpdate () {
        velocidad = manager.velocidad * -10.0f;
        velocidadRot = velocidad * 5.0f;

#if UNITY_EDITOR
                if (!giro || (Input.GetAxis("Horizontal") < 0.1f && Input.GetAxis("Horizontal") > -0.1f)) {
                    giro = false;
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, origRot, ( 1.0f - manager.velocidad) * (Time.time - tiempo));
                    direccion = this.transform.rotation.eulerAngles;
                    if (this.transform.rotation.Equals(origRot)) {
                        giro = true;
                    }
                }
                    direccion.y += Input.GetAxis("Horizontal") * Time.deltaTime * velocidadRot;
                    posicion.z -= Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;

                    direccion.y = Mathf.Clamp(direccion.y, 80.0f, 100.0f);
                    posicion.z = Mathf.Clamp(posicion.z, -2.0f, 2.0f);

                    //this.transform.MoveRotation(Quaternion.Euler(direccion * Time.deltaTime) * rb.rotation);
                    this.transform.rotation = Quaternion.Euler(direccion);
                    this.transform.position = posicion;
                    tiempo = Time.time;

#elif UNITY_ANDROID
        if (!giro || (Input.acceleration.x < 0.1f && Input.acceleration.x > -0.1f)) {
                    giro = false;
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, origRot, ( 1.0f - manager.velocidad) * (Time.time - tiempo));
                    direccion = this.transform.rotation.eulerAngles;
                    if (this.transform.rotation.Equals(origRot)) {
                        giro = true;
                    }
                }
                    direccion.y += Input.acceleration.x * Time.deltaTime * velocidadRot;
                    posicion.z -= Input.acceleration.x * Time.deltaTime * velocidad;

                    direccion.y = Mathf.Clamp(direccion.y, 80.0f, 100.0f);
                    posicion.z = Mathf.Clamp(posicion.z, -2.0f, 2.0f);

                    this.transform.rotation = Quaternion.Euler(direccion);
                    this.transform.position = posicion;
                    tiempo = Time.time;
#endif
        
        camara.position = new Vector3(camara.position.x, camara.position.y, posicion.z);
    }

    IEnumerator Shake() {
        float duration = Mathf.Clamp01(-manager.velocidad);
        float magnitude = -manager.velocidad / 2;
        float elapsed = 0.0f;

        while (elapsed < duration) {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float z = Random.value * 2.0f - 1.0f;

            z *= magnitude * damper;


            camara.position = new Vector3(camara.position.x, camara.position.y, camara.position.z + z);

            yield return null;
        }
    }

    void OnTriggerEnter(Collider c) {
        if (c.tag == "Obstaculo") {
            StartCoroutine(Shake());
            manager.velocidad *= 0.8f;
        }
    }
}
