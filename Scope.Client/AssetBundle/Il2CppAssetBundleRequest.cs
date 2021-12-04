// -----------------------------------------------------------------------
// <copyright file="Il2CppAssetBundleRequest.cs" company="Scope SL">
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

    public class Il2CppAssetBundleRequest : AsyncOperation
    {
        static Il2CppAssetBundleRequest()
        {
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<Il2CppAssetBundleRequest>();

            get_assetDelegateField = IL2CPP.ResolveICall<get_assetDelegate>("UnityEngine.AssetBundleRequest::get_asset");
            get_allAssetsDelegateField = IL2CPP.ResolveICall<get_allAssetsDelegate>("UnityEngine.AssetBundleRequest::get_allAssets");
        }

        public Il2CppAssetBundleRequest(IntPtr ptr)
            : base(ptr)
        {
        }

        public Object asset
        {
            get
            {
                IntPtr ptr = get_assetDelegateField(this.Pointer);
                if (ptr == IntPtr.Zero)
                    return null;
                return new Object(ptr);
            }
        }

        public Il2CppReferenceArray<Object> allAssets
        {
            get
            {
                IntPtr ptr = get_allAssetsDelegateField(this.Pointer);
                if (ptr == IntPtr.Zero)
                    return null;
                return new Il2CppReferenceArray<Object>(ptr);
            }
        }

        private delegate IntPtr get_assetDelegate(IntPtr _this);

        private static get_assetDelegate get_assetDelegateField;

        private delegate IntPtr get_allAssetsDelegate(IntPtr _this);

        private static get_allAssetsDelegate get_allAssetsDelegateField;
    }
}