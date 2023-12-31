﻿/*
Copyright (c) 2023 Mimy Quality
Released under the MIT license
https://opensource.org/licenses/mit-license.php
*/

namespace MimyLab.PenOptimizationUtility
{
    using UdonSharp;
    using UnityEngine;
    using VRC.SDKBase;
    using VRC.Udon;

    [AddComponentMenu("Pen Optimization Utility/Material Sharing Helper")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class MaterialSharingHelper : UdonSharpBehaviour
    {
        public Material sharedMaterial_PC;
        public Material sharedMaterial_Android;
        public int materialIndex = 0;
        public MaterialPropertyOverwriter[] sharingTargets;

        protected bool _isAndroid = false;
        protected Material _sharedMaterial;

        protected bool _initialized = false;
        protected void Initialize()
        {
            if (_initialized) { return; }

#if UNITY_ANDROID
            _isAndroid = true;
#endif

            _sharedMaterial = (_isAndroid) ? sharedMaterial_Android : sharedMaterial_PC;

            _initialized = true;
        }
        protected virtual void Start()
        {
            Initialize();

            SetSharedMaterialToMeshes();
        }

        public virtual void Setup() { }

        public void SetSharedMaterialToMeshes()
        {
            for (int i = 0; i < sharingTargets.Length; i++)
            {
                if (!sharingTargets[i]) { continue; }

                sharingTargets[i]._SetSharedMaterial(_sharedMaterial, materialIndex);
            }
        }
    }
}
