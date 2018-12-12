/*
*
*   Contient les constantes liées à l'avion et ses forces
*   A initialiser avec un fichier JSON
*
 */
public class PlaneSettings
{
    public float WingArea;
    public float LiftCoeff;
    public float DragCoeff;
    public float ThrustPower;
    public float ThrustCoeff;
    public float RollIntensity;
    public float PitchIntensity;
    public float YawIntensity;
    //Densité de l'aire (par defaut 1.184)
    public float AirDensity = 1.184f;
}