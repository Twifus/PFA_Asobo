using UnityEngine;

/*
*
*   Classe de base pour les forces
*   Calcule les forces à appliquer à l'avion, 
*   renvoie un Vector3D à ajouter à l'avion
*   Doit être appliqué à des rigid_body
*
 */

public class PlaneForceLift : PlaneForce
{
    //génère une force étant donné un rigidbody
    public abstract Vector3D CreateForce(RigidBody obj) {
        
    }
}