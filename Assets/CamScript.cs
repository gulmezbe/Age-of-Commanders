using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class CamScript : MonoBehaviourPunCallbacks
{
    
        new Camera camera;
        public bool flipHorizontal = false;
        void Awake()
        {
            camera = GetComponent<Camera>();
        }
        void OnPreCull()
        {
            camera.ResetWorldToCameraMatrix();
            camera.ResetProjectionMatrix();
            Vector3 scale = new Vector3(flipHorizontal ? -1 : 1, 1, 1);
            camera.projectionMatrix = camera.projectionMatrix * Matrix4x4.Scale(scale);
        }
        void OnPreRender()
        {
            if (PhotonNetwork.NickName == "ali")
            {
                flipHorizontal = true;
            }
            else
            {
                flipHorizontal = false;
            }
            GL.invertCulling = flipHorizontal;
        }


        void OnPostRender()
        {
            GL.invertCulling = false;
        }
    
}
