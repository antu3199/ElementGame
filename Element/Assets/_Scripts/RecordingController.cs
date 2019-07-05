using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RecordingController : MonoBehaviour
{
    [SerializeField] private GameObject startRecordbtn, stopRecordBtn;

    private bool canPress = true;
    private void Start()
    {
        GameStateManager.Instance.recorder.updateStateCallback = this.UpdateButtonState;
        this.UpdateButtonState();
    }

    private void UpdateButtonState() {
      canPress = true;
      this.startRecordbtn.SetActive(!GameStateManager.Instance.recorder.isRecording);
      this.stopRecordBtn.SetActive(GameStateManager.Instance.recorder.isRecording);
    }

    public void OnClickStartRecord()
    {
        if (canPress && GameStateManager.Instance.recorder.isRecording == false) {
          canPress = false;
          GameStateManager.Instance.recorder.PrepareRecorder();
          StartCoroutine(DelayCallRecord());
          #if UNITY_EDITOR
          canPress = true;
          #endif
        }

    }
    private IEnumerator DelayCallRecord()
    {
        yield return new WaitForSeconds(0.1f);
        GameStateManager.Instance.recorder.StartRecording();
    }
    public void OnClickStopRecord()
    {
        if (canPress && GameStateManager.Instance.recorder.isRecording == true) {
          canPress = false;
          GameStateManager.Instance.recorder.StopRecording();
          StartCoroutine(DelaySaveVideo());
          #if UNITY_EDITOR
          canPress = true;
          #endif
        }
    }
    private IEnumerator DelaySaveVideo()
    {
        yield return new WaitForSeconds(0.5f);
        GameStateManager.Instance.recorder.SaveVideoToGallery();
    }
}
