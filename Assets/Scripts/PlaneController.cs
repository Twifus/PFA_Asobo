using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Contrôleur de la physique d'un avion
/// </summary>
public class PlaneController : MonoBehaviour
{
    /// <summary>
    /// RigidBody de l'avion
    /// </summary>
    private Rigidbody _body;

    /// <summary>
    /// Emplacement du centre de masse
    /// </summary>
    public Transform CenterOfMass;

    /// <summary>
    /// Point d'application des forces du moteur
    /// </summary>
    public Transform Engine;

    /// <summary>
    /// Point d'application des forces de l'aile gauche
    /// </summary>
    public Transform LeftWing;

    /// <summary>
    /// Point d'application des forces de l'ailre droite
    /// </summary>
    public Transform RightWing;

    /// <summary>
    /// Point d'application des forces de la queue
    /// </summary>
    public Transform Tail;

    /// <summary>
    /// Prefab d'explosion instancié lors d'un impact
    /// </summary>
    public GameObject explosionPrefab;

    /// <summary>
    /// Force de pousée
    /// </summary>
    private Vector3 _thrust;

    /// <summary>
    /// Force de portance de l'aile gauche
    /// </summary>
    private Vector3 _llift;

    /// <summary>
    /// Force de portance de l'aile droite
    /// </summary>
    private Vector3 _rlift;

    /// <summary>
    /// Force de trainée
    /// </summary>
    private Vector3 _drag;

    /// <summary>
    /// Instance de Plane associée à l'avion
    /// </summary>
    private Plane _plane;
    
    private bool _crashed;
    private GameObject _explosion;
    
    void Start()
    {
        _plane = Plane.NewPlane(gameObject);
        _body = _plane.Rigidbody;
        if (CenterOfMass != null)
            _body.centerOfMass = CenterOfMass.localPosition;
    }

    /// <remarks>
    /// Application périodique des forces en fonction des inputs
    /// </remarks>
    void FixedUpdate()
    {
        if (!_crashed)
        {
            /* Lift */
            _llift = _rlift = Vector3.zero;
            Vector3 baseLift = 0.5f * PlaneSettings.LiftCoeff * PlaneSettings.AirDensity * _body.velocity.sqrMagnitude * transform.up;

            _llift = (CustomInput.GetAxis("Pitch") + PlaneSettings.RollIntensity * CustomInput.GetAxis("Roll")) * baseLift;
            _rlift = (CustomInput.GetAxis("Pitch") - PlaneSettings.RollIntensity * CustomInput.GetAxis("Roll")) * baseLift;

            /* Drag */
            _drag = -0.5f * PlaneSettings.DragCoeff * PlaneSettings.AirDensity * _body.velocity.sqrMagnitude * _body.velocity.normalized;

            /* Thrust */
            _thrust = PlaneSettings.ThrustPower * PlaneSettings.ThrustMultiplier * transform.forward;
            _thrust = _thrust * CustomInput.GetAxis("Accelerate");

            _body.AddForceAtPosition(_thrust, Engine.position);
            _body.AddForceAtPosition(_llift, LeftWing.position);
            _body.AddForceAtPosition(_rlift, RightWing.position);
            _body.AddForce(_drag);

            _body.AddForceAtPosition(- PlaneSettings.YawIntensity * baseLift.magnitude * CustomInput.GetAxis("Yaw") * transform.right, Tail.position); // Yaw
        }
    }

    /// <remarks>
    /// Redémarrage de la scène en cas de crash
    /// </remarks>
    private void Update()
    {
        if (_crashed && !_explosion)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// (DEBUG) Affichage des forces dans l'éditeur
    /// </summary>
    private void OnDrawGizmos()
    {
        Vector3 bodyPos = transform.position;
        Vector3 enginePos = Engine.transform.position;
        Vector3 lwPos = LeftWing.transform.position;
        Vector3 rwPos = RightWing.transform.position;

        Gizmos.DrawLine(enginePos, enginePos + _thrust / 1000);
        Gizmos.DrawLine(lwPos, lwPos + _llift / 1000);
        Gizmos.DrawLine(rwPos, rwPos + _rlift / 1000);
        Gizmos.DrawLine(bodyPos, bodyPos + _drag / 1000);
    }

    /// <summary>
    /// Detection de l'impact avec un obstacle
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" && Vector3.Dot(collision.contacts[0].normal, collision.relativeVelocity) > 15f)
        {
            _explosion = Instantiate(explosionPrefab, transform.position, new Quaternion(0, 0, 0, 0));
            _crashed = true;
        }
    }
}
