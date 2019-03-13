using System.Collections;
using System.Collections.Generic;

public interface IPlane {

    float posX { get; }
    float posY { get; }
    float posZ { get; }

    float rotationX { get; }
    float rotationY { get; }
    float rotationZ { get; }
    float rotationW { get; }

    // System.Numerics n'est pas compatible dans cette version
    // vector3 up { get; }
    // vector3 forward { get; }
    // vector3 right { get; }

    float speedX { get; }
    float speedY { get; }
    float speedZ { get; }
    float speedMagnitude { get; }

    //float accelerationX { get; }
    //float accelerationY { get; }
    //float accelerationZ { get; }
    //float accelerationMagnitude { get; }
}
