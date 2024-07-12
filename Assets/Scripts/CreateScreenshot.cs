using UnityEngine;

public class CreateScreenshot : MonoBehaviour
{
    [SerializeField] private bool _isCanScreen;
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.K) && _isCanScreen)
        {
            ScreenCapture.CaptureScreenshot("Screenshot.png");
        }
    }
}
