using Unity.Cinemachine;
using UnityEngine;

public class LockCam : CinemachineExtension
{
    [SerializeField] float m_xPosition = 0;
    [SerializeField] float m_yPosition = 10;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam, 
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
       if(stage == CinemachineCore.Stage.Body)
       {
            Vector3 pos = state.RawPosition;
            pos.x = m_xPosition;
            pos.y = m_yPosition;
            state.RawPosition = pos;
       } 
    }
}
