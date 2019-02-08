
public enum figure_id {LOOP, BARREL, CUBANEIGHT};

public class Figure {

    figure_id id;
    public float quality;
}

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

    //Temps de récupération du point depuis le début du jeu
    public float time;
}
