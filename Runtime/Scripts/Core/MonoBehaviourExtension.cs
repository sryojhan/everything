using UnityEngine;

public static class MonobehaviourExtension
{
    public static T GetOrAddComponent<T>(this MonoBehaviour obj) where T : MonoBehaviour
    {
        T comp = obj.GetComponent<T>();

        if (!comp) comp = obj.gameObject.AddComponent<T>();

        return comp;
    }

}
