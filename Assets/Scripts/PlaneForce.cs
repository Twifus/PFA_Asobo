using UnityEngine;

/*
*
*   Classe de base pour les forces
*   Calcule les forces à appliquer à l'avion, 
*   renvoie un Vector3D à ajouter à l'avion
*   Doit être appliqué à des rigid_body
*
 */

public abstract class PlaneForce
{
    protected PlaneSettings _settings;

    /*
        Constructeurs
     */
     
    //crée une force avec des PlaneSettings en paramètre
    public PlaneForce(PlaneSettings settings) => Settings = settings;

    //Accesseurs
    public PlaneSettings Settings { protected get { return _settings; } set => _settings = value; }
    
    //génère une force étant donné un rigidbody
    public abstract Vector3D CreateForce(RigidBody obj);
}