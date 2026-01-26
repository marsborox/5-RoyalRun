using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] ParticleSystem speedUpParticleSystem;
    [SerializeField] float minFOV = 20f;
    [SerializeField] float maxFOV = 120f;
    [SerializeField] float zoomDuration = 0.5f;
    [SerializeField] float zoomSpeedModifier = 5f;

    CinemachineCamera cinemachineCamera;
    private void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }
    public void ChangeCameraFOV(float speedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutine(speedAmount));
        if (speedAmount > 0)
        {
            speedUpParticleSystem.Play();
        }
    }
    IEnumerator ChangeFOVRoutine(float speedAmount)
    {//we have time min and max FOV and amount by which we will increase
        //we will increase eventually to destiantion vlaue over time
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(startFOV+speedAmount * zoomSpeedModifier, minFOV,maxFOV);//to make sure we wot go outside of boundaries

        float elapsedTime = 0f;

        while (elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration;
            elapsedTime += Time.deltaTime;
            //LERP slowly interpoladte from one to other value
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV,targetFOV,elapsedTime / zoomDuration);
            yield return null;//just wait till end of frame
        }
        cinemachineCamera.Lens.FieldOfView = targetFOV;
    }
}
