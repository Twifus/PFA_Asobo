public interface IFlyingObject {

    System.Numerics.Vector3 pos { get; }

    System.Numerics.Quaternion rotation { get; }

    float roll { get; }
    float pitch { get; }
    float yaw { get; }

    float rollScalar { get; }
    float pitchScalar { get; }
    float yawScalar { get; }

    System.Numerics.Vector3 up { get; }
    System.Numerics.Vector3 forward { get; }
    System.Numerics.Vector3 right { get; }

    System.Numerics.Vector3 speed { get; }

    float time { get; }
}
