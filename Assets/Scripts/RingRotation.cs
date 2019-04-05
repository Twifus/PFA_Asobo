using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controle l'orientation d'un anneau flottant
/// </summary>
public class RingRotation : MonoBehaviour
{
    /// <summary>
    /// Intervalle de rotation d'un anneau
    /// </summary>
    [System.Serializable]
    public struct ZRot
    {
        public float min;
        public float max;
    }

    /// <summary>
    /// Tolérance sur l'attribution des points
    /// </summary>
    public float tolerance;

    /// <summary>
    /// Extérieur de l'annneau
    /// </summary>
    public GameObject _outer;

    /// <summary>
    /// Indicateur de direction
    /// </summary>
    public GameObject _indicator;

    /// <summary>
    /// Sens de rotation de l'anneau externe
    /// </summary>
    private int _outer_rot_side;

    /// <summary>
    /// Angle de rotation de l'indicateur
    /// </summary>
    private float _rotation;

    /// <summary>
    /// Intervalle de rotations possibles pour l'anneau
    /// </summary>
    public ZRot rotRange;
    
    private AudioSource _point;

    /// <summary>
    /// Oriente l'anneau d'un angle aléatoire dans l'intervalle spécifiée;
    /// </summary>
    void Start()
    {
        _outer_rot_side = Random.Range(0, 2) * 2 - 1; // -1 or 1
        _rotation = Random.Range(rotRange.min, rotRange.max);
        _indicator.transform.Rotate(Vector3.forward, _rotation);
        _point = GetComponent<AudioSource>();
    }
    
    /// <summary>
    /// Fait tourner l'anneau externe chaque frame
    /// </summary>
    void Update()
    {
        _outer.transform.Rotate(Vector3.right * _outer_rot_side * Time.deltaTime * 25);
    }

    /// <summary>
    /// Attribue des points lorsque le joueur passe dans l'anneau avec le bon angle
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        Vector3 reference = Vector3.ProjectOnPlane(other.transform.up, _indicator.transform.forward);
        if (Vector3.Angle(_indicator.transform.up, reference) < tolerance)
        { 
            Debug.Log("15 point");
            _point.Play(0);
            other.GetComponent<FigureManager>().UpdateScore(15);
        }
    }
}
