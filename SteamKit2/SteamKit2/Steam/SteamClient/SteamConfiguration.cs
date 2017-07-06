﻿/*
 * This file is subject to the terms and conditions defined in
 * file 'license.txt', which is part of this source code package.
 */


using System;
using System.Threading;
using SteamKit2.Discovery;

namespace SteamKit2
{
    /// <summary>
    /// Configuration object to use.
    /// This object should not be mutated after it is passed to one or more <see cref="SteamClient"/> objects.
    /// </summary>
    public sealed class SteamConfiguration
    {
        /// <summary>
        /// Creates a <see cref="SteamConfiguration"/> object.
        /// </summary>
        public SteamConfiguration()
        {
            serverListProvider = new NullServerListProvider();
            webAPIBaseAddress = WebAPI.DefaultBaseAddress;
        }

        IServerListProvider serverListProvider;
        Uri webAPIBaseAddress;

        SmartCMServerList serverList;

        /// <summary>
        /// Whether or not to use the Steam Directory to discover available servers.
        /// </summary>
        public bool AllowDirectoryFetch { get; set; } = true;

        /// <summary>
        /// The Steam Cell ID to prioritize when connecting.
        /// </summary>
        public uint CellID { get; set; } = 0;

        /// <summary>
        /// The connection timeout used when connecting to Steam serves.
        /// </summary>
        public TimeSpan ConnectionTimeout { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// The supported protocol types to use when attempting to connect to Steam.
        /// If <see cref="ProtocolTypes.Tcp"/> and <see cref="ProtocolTypes.Udp"/> are both specified, TCP will take precedence
        /// and UDP will not be used.
        /// </summary>
        public ProtocolTypes ProtocolTypes { get; set; } = ProtocolTypes.Tcp;

        /// <summary>
        /// The server list provider to use.
        /// </summary>
        public IServerListProvider ServerListProvider
        {
            get => serverListProvider;
            set => serverListProvider = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// The Universe to connect to. This should always be <see cref="EUniverse.Public"/> unless
        /// you work at Valve and are using this internally. If this is you, hello there.
        /// </summary>
        public EUniverse Universe { get; set; } = EUniverse.Public;

        /// <summary>
        /// The base address of the Steam Web API to connect to.
        /// </summary>
        public Uri WebAPIBaseAddress
        {
            get => webAPIBaseAddress;
            set => webAPIBaseAddress = value ?? throw new ArgumentNullException(nameof(value));
        }

        internal SmartCMServerList ServerList
            => LazyInitializer.EnsureInitialized(ref serverList, () => new SmartCMServerList(this));

    }
}
