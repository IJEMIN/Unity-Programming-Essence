// ----------------------------------------------------------------------------
// <copyright file="PhotonTransformViewEditor.cs" company="Exit Games GmbH">
//   PhotonNetwork Framework for Unity - Copyright (C) 2018 Exit Games GmbH
// </copyright>
// <summary>
//   This is a custom editor for the TransformView component.
// </summary>
// <author>developer@exitgames.com</author>
// ----------------------------------------------------------------------------


namespace Photon.Pun
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(PhotonTransformView))]
    public class PhotonTransformViewEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                EditorGUILayout.HelpBox("Editing is disabled in play mode.", MessageType.Info);
                return;
            }

            PhotonTransformView view = (PhotonTransformView)target;

            view.m_SynchronizePosition = PhotonGUI.ContainerHeaderToggle("Synchronize Position", view.m_SynchronizePosition);
            view.m_SynchronizeRotation = PhotonGUI.ContainerHeaderToggle("Synchronize Rotation", view.m_SynchronizeRotation);
            view.m_SynchronizeScale = PhotonGUI.ContainerHeaderToggle("Synchronize Scale", view.m_SynchronizeScale);
        }
    }
}