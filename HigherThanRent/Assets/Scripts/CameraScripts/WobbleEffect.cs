using JetBrains.Annotations;
using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class WobbleEffect : MonoBehaviour
{

    public Material wobbleEffectMaterial;
    public bool wobbleActive = false;
    public float frequency = 1f;
    public float shift = 0f;
    public float amplitude = 0f;
    public float maxAmplitude = 0.2f;
    public float shiftSpeed = 5f;
    public float amplitudeSpeed = 0.025f;    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetFrequency (float frequency)
    {
        wobbleEffectMaterial.SetFloat("_frequency", frequency);
    }

    private void SetShift(float shift)
    {
        wobbleEffectMaterial.SetFloat("_shift", shift);
    }

    private void SetAmplitude(float amplitude)
    {
        wobbleEffectMaterial.SetFloat("_amplitude", amplitude);
    }

    public void StartWobble()
    {
        if (!wobbleActive)
        { 
            wobbleActive = true;
            StartCoroutine(WobbleCoroutine());
        }
    }

    public void StopWobble()
    {
        wobbleActive = false;
    }

    private IEnumerator WobbleCoroutine()
    {
        SetFrequency(frequency);

        while (amplitude < maxAmplitude)
        {
            if (wobbleActive)
            {
                SetShift(shift);
                SetAmplitude(amplitude);

                amplitude += amplitudeSpeed * 2 *Time.deltaTime;
                shift += Time.deltaTime * 2 *shiftSpeed;

                yield return null;
            }
            else
            {
                break;
            }
        }

        if (wobbleActive)
        {
            amplitude = maxAmplitude;
            SetAmplitude(amplitude);
        }

        while (wobbleActive)
        {
            SetShift(shift);
            shift += Time.deltaTime * 2 * shiftSpeed;
            
            yield return null;
        }

        while (amplitude > 0f)
        {
            if (!wobbleActive)
            {
                SetShift(shift);
                SetAmplitude(amplitude);

                amplitude -= amplitudeSpeed * 2 *Time.deltaTime;
                shift += Time.deltaTime * 2 * shiftSpeed;

                yield return null;
            }
            else
            {
                break;
            }
        }

        if (!wobbleActive)
        {
            shift = 0f;
            amplitude = 0f;
            SetAmplitude(amplitude);
            SetShift(shift);
        }
    }
}
