public interface IFlyingObject {

    //float posX { get; }
    //float posY { get; }
    //float posZ { get; }
    System.Numerics.Vector3 pos { get; }

    //float rotationX { get; }
    //float rotationY { get; }
    //float rotationZ { get; }
    //float rotationW { get; }
    System.Numerics.Quaternion rotation { get; }

    float roll { get; }
    float pitch { get; }
    float yaw { get; }

    // System.Numerics n'est pas compatible dans cette version
    // vector3 up { get; }
    // vector3 forward { get; }
    // vector3 right { get; }
    System.Numerics.Vector3 up { get; }
    System.Numerics.Vector3 forward { get; }
    System.Numerics.Vector3 right { get; }

    //float speedX { get; }
    //float speedY { get; }
    //float speedZ { get; }
    //float speedMagnitude { get; }
    System.Numerics.Vector3 speed { get; }

    float time { get; }
}
