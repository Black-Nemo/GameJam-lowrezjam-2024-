using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(PixelPerfectCamera))]
public class SetupPixelPerfectCamera : MonoBehaviour
{
    [SerializeField]private Camera _camera;
    private void Start()
    {
        PixelPerfectCamera pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
        pixelPerfectCamera.assetsPPU = 64; // Her bir birimde 64 piksel
        pixelPerfectCamera.refResolutionX = 64; // Ana ekran genişliği
        pixelPerfectCamera.refResolutionY = 64; // Ana ekran yüksekliği
        _camera.orthographicSize = 32; // Kamera ortogonal boyutu (64/2)
    }
}
