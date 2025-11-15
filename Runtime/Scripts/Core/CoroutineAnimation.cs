using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CoroutineAnimation
{
    public delegate void OnValueUpdate(float value);
    public delegate void Callback();

    public float duration = 1;
    public float delay = 0;

    public Interpolation interpolation;

    private float progress = 0;
    private Coroutine coroutine = null;

    public virtual void Play(MonoBehaviour behaviour, OnValueUpdate update = null, Callback begin = null, Callback end = null, bool useUnscaledTime = false)
    {
        if(coroutine != null)
        {
            behaviour.StopCoroutine(coroutine);
            coroutine = null;
        }

        coroutine = behaviour.StartCoroutine(Coroutine(update, begin, end, useUnscaledTime));
    }

    public IEnumerator Coroutine(OnValueUpdate update, Callback begin, Callback onFinish, bool useUnscaledTime)
    {
        yield return new WaitForSeconds(delay);
        
        begin?.Invoke();
        update?.Invoke(interpolation.Interpolate(0));

        progress = 0;
        float invDuration = 1.0f / duration;

        for(float i = 0; i < 1; i += invDuration * (useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime))
        {
            progress = i;
            update?.Invoke(interpolation.Interpolate(i));
            yield return null;
        }

        update?.Invoke(interpolation.Interpolate(1.0f));

        progress = 1;
        coroutine = null;

        onFinish?.Invoke();
    }

    public bool IsFinished => coroutine == null;

    public float GetProgess()
    {
        return progress;
    }

    public void Stop(MonoBehaviour behaviour)
    {
        if(coroutine != null)
        {
            behaviour.StopCoroutine(coroutine);
            coroutine = null;
        }
    }


    public void MoveTo(MonoBehaviour obj, RectTransform tr, Vector2 finalPosition, OnValueUpdate onUpdate = null, Callback onBegin = null, Callback onEnd = null, bool useUnscaledTime = false)
    {
        Vector2 initialPosition = tr.anchoredPosition;

        void OnUpdate(float i)
        {
            tr.anchoredPosition = Vector2.LerpUnclamped(initialPosition, finalPosition, i);

            onUpdate?.Invoke(i);
        }

        Play(obj, OnUpdate, onBegin, onEnd, useUnscaledTime: useUnscaledTime);
    }


    public void MoveTo(MonoBehaviour obj, Transform tr, Vector3 finalPosition, OnValueUpdate onUpdate = null, Callback onBegin = null, Callback onEnd = null, bool local = true, bool useUnscaledTime = false)
    {
        Vector3 initialPosition = local ? tr.localPosition : tr.position;

        void OnUpdate(float i)
        {
            if(local)
                tr.localPosition = Vector2.LerpUnclamped(initialPosition, finalPosition, i);
            else
                tr.position = Vector3.LerpUnclamped(initialPosition, finalPosition, i);

            onUpdate?.Invoke(i);
        }

        Play(obj, OnUpdate, onBegin, onEnd, useUnscaledTime: useUnscaledTime);
    }

    public void ScaleTo(MonoBehaviour obj, Transform tr, Vector3 finalScale, OnValueUpdate onUpdate = null, Callback onBegin = null, Callback onEnd = null, bool useUnscaledTime = false)
    {
        Vector3 initialScale = tr.localScale;

        void OnUpdate(float i)
        {
            tr.localScale = Vector3.LerpUnclamped(initialScale, finalScale, i);

            onUpdate?.Invoke(i);
        }

        Play(obj, OnUpdate, onBegin, onEnd, useUnscaledTime: useUnscaledTime);
    }
    

    public void RotateTo(MonoBehaviour obj, Transform tr, Quaternion finalRotation, OnValueUpdate onUpdate = null, Callback onBegin = null, Callback onEnd = null, bool useUnscaledTime = false)
    {
        Quaternion initialRotation = tr.localRotation;

        void OnUpdate(float i)
        {
            tr.localRotation = Quaternion.LerpUnclamped(initialRotation, finalRotation, i);

            onUpdate?.Invoke(i);
        }

        Play(obj, OnUpdate, onBegin, onEnd, useUnscaledTime: useUnscaledTime);
    }

}

