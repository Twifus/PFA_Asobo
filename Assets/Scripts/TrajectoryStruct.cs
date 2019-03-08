
public enum figure_id {LOOP, BARREL, CUBANEIGHT};

public class Figure {

    public figure_id id;
    public float quality;

    public Figure(figure_id id = figure_id.LOOP, float quality = 0f)
    {
        this.id = id;
        this.quality = quality;
    }
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

    //Angles de l'avion
    public float roll;
    public float pitch;
    public float yaw;

    //Temps de récupération du point depuis le début du jeu
    public float time;

    public float inputRoll;
    public float inputPitch;
    public float inputYaw;
    public float inputAccelerate;

    public Coordinate()
    {
        xpos = 0f;
        ypos = 0f;
        zpos = 0f;

        xangle = 0f;
        yangle = 0f;
        zangle = 0f;
        wangle = 0f;

        roll = 0f;
        pitch = 0f;
        yaw = 0f;

        time = 0f;

        inputRoll = CustomInput.GetAxis("Roll");
        inputPitch = CustomInput.GetAxis("Pitch");
        inputYaw = CustomInput.GetAxis("Yaw");
        inputAccelerate = CustomInput.GetAxis("Accelerate");
    }

}
