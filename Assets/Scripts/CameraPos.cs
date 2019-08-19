using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    [SerializeField]
    private Transform camTransform;

    private void Update()
    {
        CamPos();
    }

    private void CamPos()
    {
        Camera.main.transform.position = new Vector3(-2.4f, 160f, 55f);
    }
}
