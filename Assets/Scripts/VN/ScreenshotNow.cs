using System.Collections;
using UnityEngine;

public class ScreenshotNow : MonoBehaviour
{
    [SerializeField]
    private Canvas _notInPic;

    public void Screenshot(string savePath)
    {
        StartCoroutine(TakeScreenShot(savePath));
    }

    private IEnumerator TakeScreenShot(string savePath)
    {
        // Wait till the last possible moment before screen rendering to hide the UI
        yield return null;
        _notInPic.enabled = false;

        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();

        // Take screenshot
        ScreenCapture.CaptureScreenshot(savePath);

        // Show UI after we're done
        _notInPic.enabled = true;

    }
}
