// -----------------------------------------------------------------------
// <copyright file="Hardware.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.API.Features
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.NetworkInformation;
    using UnityEngine;

    /// <summary>
    /// A set of tools to easily interact with client's hardware.
    /// </summary>
    public class Hardware
    {
        /// <summary>
        /// Gets the NetBIOS name of this local computer.
        /// </summary>s
        public string MachineName => Environment.MachineName ?? "Unknown";

        /// <summary>
        /// Gets the username of the person who's currently logged on to the Windows operating system.
        /// </summary>
        public string UserName => Environment.UserName ?? "Unknown";

        /// <summary>
        /// Gets an <see cref="OperatingSystem"/> object containing the current platform identifier and version number.
        /// </summary>
        public OperatingSystem OSVersion => Environment.OSVersion;

        /// <summary>
        /// Gets or creates the Scope Environment folder located in the ApplicationData directory.
        /// </summary>
        public string AppData
        {
            get
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Scope Environment");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                return path;
            }
        }

        /// <summary>
        /// Gets the MAC address.
        /// </summary>
        public string MACAddress => NetworkInterface.GetAllNetworkInterfaces().Where(networkInterface =>
        networkInterface.OperationalStatus is OperationalStatus.Up &&
        networkInterface.NetworkInterfaceType is not NetworkInterfaceType.Loopback).Select(networkInterface =>
        networkInterface.GetPhysicalAddress().ToString()).FirstOrDefault() ?? "Unknown";
    }
}
