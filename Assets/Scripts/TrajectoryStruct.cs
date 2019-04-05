/// <summary>
/// Enumération des identifinats pour chaque figure pouvant être reconnu
/// </summary>
public enum figure_id {LOOP, BARREL, CUBANEIGHT};

/// <summary>
/// Structure de renvoie des algortithmes de détection : contient un identifiant de figure et une qualité lié à cette figure
/// </summary>
public class Figure {

    public figure_id id;
    public float quality;

    public Figure(figure_id id = figure_id.LOOP, float quality = 0f)
    {
        this.id = id;
        this.quality = quality;
    }
}

/// <summary>
/// Ancienne structure utilisée par le programme de reconaissance : obsolète.
/// </summary>
public class Coordinate {

    //Position de l'avion suivant x,y,z
    public float xpos;
    public float ypos;
    public float zpos;

    //Rotation de l'avion suivant x,y,z(,w)
    public float xangle;
    public float yangle;
    public float zangle;
    public float wangle;

    //Angles de l'avion
    public float roll;
    public float pitch;
    public float yaw;

    //Temps de récupération du point depuis le début du jeu
    public float time;

    public Coordinate(float x = 0f, float y = 0f, 
        float z = 0f, float xangle = 0f, 
        float yangle = 0f, float zangle = 0f, 
        float wangle = 0f, float time = 0f)
    {
        this.xpos = x;
        this.ypos = y;
        this.zpos = z;

        this.xangle = xangle;
        this.yangle = yangle;
        this.zangle = zangle;
        this.wangle = wangle;

        this.time = time;
    }

}
