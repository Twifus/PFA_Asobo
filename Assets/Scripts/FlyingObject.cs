/// <summary>
/// Interface décrivant un objet dont la trajectoire peut être soumise à un détecteur de figures
/// </summary>
public interface IFlyingObject {

    /// <summary>
    /// Position 3D de l'objet
    /// </summary>
    System.Numerics.Vector3 pos { get; }

    /// <summary>
    /// Rotation de l'objet
    /// </summary>
    System.Numerics.Quaternion rotation { get; }

    /// <summary>
    /// Angle Roll de l'objet
    /// </summary>
    float roll { get; }

    /// <summary>
    /// Angle Pitch de l'objet
    /// </summary>
    float pitch { get; }

    /// <summary>
    /// Angle Yaw de l'objet
    /// </summary>
    float yaw { get; }

    float rollScalar { get; }
    float pitchScalar { get; }
    float yawScalar { get; }

    /// <summary>
    /// Vecteur Up de l'objet
    /// </summary>
    System.Numerics.Vector3 up { get; }

    /// <summary>
    /// Vecteur Forward de l'object
    /// </summary>
    System.Numerics.Vector3 forward { get; }

    /// <summary>
    /// Vecteur Right de l'objet
    /// </summary>
    System.Numerics.Vector3 right { get; }

    /// <summary>
    /// Vecteur vitesse de l'objet
    /// </summary>
    System.Numerics.Vector3 speed { get; }

    /// <summary>
    /// Temps actuel associé à l'objet
    /// </summary>
    float time { get; }
}
