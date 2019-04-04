using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Proxy à UnityEngine.Input permettant de get/set des axes
/// </summary>
/// <remarks>
/// Cette classe sert de proxy à la classe UnityEngine.Input.
/// Elle permet l'écriture et la lecture de valeurs sur les axes, la classe de base d'Unity ne permettant que la lecture.
/// Chaque axe peut être émulé (valeurs écrites par l'utilisateur) ou non (valeurs lues par UnityEngine.Input) indépendament des autres.
/// Les noms des axes sont identiques à ceux renseignés dans Unity.
/// </remarks>
public class CustomInput {

    /// <summary>
    /// Dictionnaire des états d'axes
    /// </summary>
    /// <remarks>
    /// Contient l'état de chaque axe ajouté au proxy (simulé ou non)
    /// </remarks>
    private static Dictionary<string, bool> simulate = new Dictionary<string, bool>();
    
    /// <summary>
    /// Dictionnaire des valeurs d'axes
    /// </summary>
    /// <remarks>
    /// Contient la valeur de chaque axe ajouté au proxis et ayant été émulé au moins une fois
    /// </remarks>
    private static Dictionary<string, float> values =  new Dictionary<string, float>();

    /// <summary>
    /// Ajoute un nouvel axe au proxy
    /// </summary>
    /// <param name="axis">Nom de l'axe à ajouter (doit correspondre à un axe renseigné dans Unity)</param>
    private static void AddAxis(string axis)
    {
        simulate.Add(axis, false);
        values.Add(axis, 0);
    }

    /// <summary>
    /// (Dés)Active la simulation d'un axe
    /// </summary>
    /// <param name="axis">Nom de l'axe dont le mode doit changer</param>
    public static void ToggleDummyInput(string axis) {
        if (!simulate.ContainsKey(axis))
            AddAxis(axis);

        simulate[axis] = !simulate[axis];
    }

    /// <summary>
    /// Lit la valeur d'un axe
    /// </summary>
    /// <param name="axis">Nom de l'axe dont la valeur doit être lue</param>
    /// <returns>
    /// Valeur lue sur l'axe <paramref name="axis"/>
    /// </returns>
    public static float GetAxis(string axis)
    {
        if (!simulate.ContainsKey(axis))
            AddAxis(axis);

        if (simulate[axis])
            return values[axis];
        else
            return Input.GetAxis(axis);
    }

    /// <summary>
    /// Ecrit une valeur sur un axe
    /// </summary>
    /// <param name="axis">Nom de l'axe dont la valeur doit être modifié</param>
    /// <param name="value">Nouvelle valeur pour cet axe</param>
    public static void SetAxis(string axis, float value)
    {
        if (!simulate.ContainsKey(axis))
            AddAxis(axis);

        if (values.ContainsKey(axis))
            values[axis] = value;
        else
            values.Add(axis, value);
    }
}
