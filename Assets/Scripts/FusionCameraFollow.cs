using Unity.Cinemachine;
using UnityEngine;

public class FusionCameraFollow : MonoBehaviour
{
    //public CinemachineVirtualCamera virtualCamera;
    //Sửa thành CinemachineCamera thay vì CinemachineVirtualCamera
    public CinemachineCamera virtualCamera;

    //private CinemachineFramingTransposer cinemachineFramingTransposer;
    //Sửa thành CinemachineFollow (thay cho FramingTransposer trong phiên bản mới)
    //private CinemachineFollow cinemachineFollow;
    private CinemachinePositionComposer positionComposer;

    public void AssignCamera(Transform playerTransform)
    {
        // Gán mục tiêu Follow và LookAt cho camera
        virtualCamera.Follow = playerTransform;
        virtualCamera.LookAt = playerTransform;
    }

    private void Start()
    {
        //cinemachineFollow = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body) as CinemachineFollow;
        // Lấy component Position Composer từ Virtual Camera
        positionComposer = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body) as CinemachinePositionComposer;

        if (positionComposer == null)
        {
            Debug.LogError("CinemachinePositionComposer không được tìm thấy trong Virtual Camera!");
        }
    }

    private void Update()
    {
        //if (cinemachineFollow == null)
        //    return;

        //float scroll = Input.GetAxis("Mouse ScrollWheel");

        //// 4. Sử dụng property CameraDistance thay vì m_CameraDistance
        //cinemachineFollow.CameraDistance -= scroll * 5f; // Nhân tốc độ zoom
        //cinemachineFollow.CameraDistance = Mathf.Clamp(cinemachineFollow.CameraDistance, 2f, 10f);
        if (positionComposer != null)
        {
            // Lấy giá trị cuộn chuột
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            // Điều chỉnh khoảng cách camera bằng cách thay đổi giá trị Z của TargetOffset
            Vector3 offset = positionComposer.TargetOffset;
            offset.z -= scroll * 5f; // Nhân tốc độ zoom
            offset.z = Mathf.Clamp(offset.z, -10f, -2f); // Giới hạn khoảng cách Z
            positionComposer.TargetOffset = offset; // Gán lại giá trị mới
        }
    }
}

//public class CameraFollow : MonoBehaviour
//{
//    public CinemachineCamera virtualCamera;
//    private CinemachineFramingTransposer framingTransposer;

//    public void AssignCamera(Transform playerTransform)
//    {
//        virtualCamera.Follow = playerTransform;
//        virtualCamera.LookAt = playerTransform;
//    }

//    private void Start()
//    {
//        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
//    }

//    private void Update()
//    {
//        float scroll = Input.GetAxis("Mouse ScrollWheel");
//        framingTransposer.m_CameraDistance -= scroll;
//        framingTransposer.m_CameraDistance = Mathf.Clamp(framingTransposer.m_CameraDistance, 2f, 10f);
//    }
//}