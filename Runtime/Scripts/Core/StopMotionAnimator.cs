using UnityEngine;


[RequireComponent(typeof(Animator))]
public class StopMotionAnimator : MonoBehaviour
{
    //TODO: mejorar un poquito este script

    public Animator _animator;
    [Header("Stop motion")]
    public float frameRate = 12.0f; 
    public bool enableStopMotion = true; 

    private float _timer;
    private float _frameDuration;

    [Header("Rotation")]
    public bool enableAngleSnap = true;
    public float angleStep = 30;

    void Awake()
    {
        CalculateFrameDuration();
    }

    void OnValidate()
    {
        if(_animator == null) _animator = GetComponent<Animator>();
        CalculateFrameDuration();
    }

    void Update()
    {
        ManageStopMotion();
    }
    private void LateUpdate()
    {
        ManageAngleSnap();
    }

    void ManageStopMotion()
    {
        if (!enableStopMotion)
        {
            if (!_animator.enabled) _animator.enabled = true;
            return;
        }

        if (_animator.enabled) _animator.enabled = false;

        _timer += Time.deltaTime;

        if (_timer >= _frameDuration)
        {
            _animator.Update(_timer);
            _timer %= _frameDuration;
        }
    }

    void ManageAngleSnap()
    {
        if (!enableAngleSnap) return;
        float cameraAngle = Camera.main.transform.eulerAngles.y;
        float parentAngle = transform.parent.eulerAngles.y;

        float relativeAngle = parentAngle - cameraAngle;
        float snappedRelative = Mathf.Round(relativeAngle / angleStep) * angleStep;

        float finalAngle = snappedRelative + cameraAngle;
        transform.rotation = Quaternion.Euler(transform.parent.eulerAngles.x, finalAngle, transform.parent.eulerAngles.z);
    }


    private void CalculateFrameDuration()
    {
        if (frameRate > 0)
            _frameDuration = 1.0f / frameRate;
        else
            _frameDuration = 0f;
    }


    public void SetStopMotionEffect(bool isActive)
    {
        enableStopMotion = isActive;

        if (!isActive)
        {
            _animator.enabled = true;
        }
    }

    public void SetFrameRate(float newFps)
    {
        frameRate = newFps;
        CalculateFrameDuration();
    }
}