using UnityEngine;
using ProjectBorderland.Core.Manager;

namespace ProjectBorderland.Miscellaneous
{
    /// <summary>
    /// Rotates object to always face main camera.
    /// </summary>
    public class FaceCamera : MonoBehaviour
    {
        //==============================================================================
        // Variables
        //==============================================================================



        //==============================================================================
        // Functions
        //==============================================================================
        #region MonoBehaviour methods
        private void Update()
        {
            LookAtCamera();
        }
        #endregion



        #region ProjectBorderland methods
        private void LookAtCamera()
        {
            transform.LookAt(GameManager.Instance.MainCamera.transform);
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
        #endregion
    }
}