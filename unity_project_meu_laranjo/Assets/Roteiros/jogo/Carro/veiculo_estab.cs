using UnityEngine;
using System.Collections;

public class veiculo_estab : MonoBehaviour {

    public WheelCollider rodaTrazEsq, rodaTrazDir;
    public float forca = 10000, estabilidadeXvel = 800;
    private Rigidbody corpoRigido;
    private bool estaNoChao1, estaNoChao2;

    void Start () {
        corpoRigido = GetComponent<Rigidbody> ();
        corpoRigido.centerOfMass += new Vector3 (0, -0.3f, -0.3f);
    }

    void Update () {
        float forcaEsquerdaTraz = 1;
        float forcaDireitaTraz = 1;
        // CHECAR COLISOES
        WheelHit hit;
        estaNoChao1 = rodaTrazEsq.GetGroundHit (out hit);
        if (estaNoChao1) {
        forcaEsquerdaTraz = (-rodaTrazEsq.transform.InverseTransformPoint (hit.point).y - rodaTrazEsq.radius) / rodaTrazEsq.suspensionDistance;
        }
        estaNoChao2 = rodaTrazDir.GetGroundHit (out hit);
        if (estaNoChao2) {
        forcaDireitaTraz = (-rodaTrazDir.transform.InverseTransformPoint (hit.point).y - rodaTrazDir.radius) / rodaTrazDir.suspensionDistance;
        }
        // APLICAR FORCAS DESCOBERTAS
        float antiRollForce = (forcaEsquerdaTraz-forcaDireitaTraz)*forca;
        if (estaNoChao1) {
        corpoRigido.AddForceAtPosition (rodaTrazEsq.transform.up * -antiRollForce, rodaTrazEsq.transform.position);
        }
        if (estaNoChao2) {
        corpoRigido.AddForceAtPosition (rodaTrazDir.transform.up * -antiRollForce, rodaTrazDir.transform.position);
        }
    }

    void FixedUpdate(){
        if (estaNoChao1 || estaNoChao2) {
        corpoRigido.AddForce (-transform.up * (5000 + estabilidadeXvel * Mathf.Abs ((corpoRigido.velocity.magnitude * 3.6f))));
        }
        corpoRigido.velocity = Vector3.ClampMagnitude (corpoRigido.velocity, 300);
    }
}