// -----------------------------------------------------------------------
// <copyright file="Il2CppAssetBundleCreateRequest.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace UnityEngine
{
#pragma warning disable SA1201 // Elements should appear in the correct order
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable SA1300 // Element should begin with upper-case letter
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
#pragma warning disable SA1310 // Field names should not contain underscore
#pragma warning disable IDE1006 // Naming Styles

    using System;
    using UnhollowerBaseLib;

    public class Il2CppAssetBundleCreateRequest : AsyncOperation
    {
        static Il2CppAssetBundleCreateRequest()
        {
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<Il2CppAssetBundleCreateRequest>();

            get_assetBundleDelegateField = IL2CPP.ResolveICall<get_assetBundleDelegate>("UnityEngine.AssetBundleCreateRequest::get_assetBundle");
        }

        public Il2CppAssetBundleCreateRequest(IntPtr ptr)
            : base(ptr)
        {
        }

        public Il2CppAssetBundle assetBundle
        {
            [UnhollowerBaseLib.Attributes.HideFromIl2Cpp]
            get
            {
                IntPtr ptr = get_assetBundleDelegateField(this.Pointer);
                if (ptr == IntPtr.Zero)
                    return null;
                return new Il2CppAssetBundle(ptr);
            }
        }

        private delegate IntPtr get_assetBundleDelegate(IntPtr _this);

        private static get_assetBundleDelegate get_assetBundleDelegateField;
    }
}