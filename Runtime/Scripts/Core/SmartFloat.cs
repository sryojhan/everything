using UnityEngine;

[System.Serializable]
public class SmartFloat
{
    public delegate void OnValueChangedEvent(float previousValue, float newValue);
    public OnValueChangedEvent OnValueChanged;

    [SerializeField]
    private float value;
    public float Value
    {
        get => value;
        set
        {
            OnValueChanged(this.value, value);
            this.value = value;
        }
    }

    public SmartFloat(float initialValue = 0)
    {
        value = initialValue;
    }

    public static implicit operator float(SmartFloat sf)
    {
        return sf == null ? sf.Value : 0;
    }
}
