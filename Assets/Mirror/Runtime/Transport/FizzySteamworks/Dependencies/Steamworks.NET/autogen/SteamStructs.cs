// This file is provided under The MIT License as part of Steamworks.NET.
// Copyright (c) 2013-2019 Riley Labrecque
// Please see the included LICENSE.txt for additional information.

// This file is automatically generated.
// Changes to this file will be reverted when you update Steamworks.NET

#if UNITY_ANDROID || UNITY_IOS || UNITY_TIZEN || UNITY_TVOS || UNITY_WEBGL || UNITY_WSA || UNITY_PS4 || UNITY_WII || UNITY_XBOXONE || UNITY_SWITCH
	#define DISABLESTEAMWORKS
#endif

#if !DISABLESTEAMWORKS

using System.Runtime.InteropServices;
using IntPtr = System.IntPtr;

namespace Steamworks {
	// friend game played information
	[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
	public struct FriendGameInfo_t {
		public CGameID m_gameID;
		public uint m_unGameIP;
		public ushort m_usGamePort;
		public ushort m_usQueryPort;
		public CSteamID m_steamIDLobby;
	}

	//-----------------------------------------------------------------------------
	// Purpose: information about user sessions
	//-----------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
	public struct FriendSessionStateInfo_t {
		public uint m_uiOnlineSessionInstances;
		public byte m_uiPublishedToFriendsSessionInstance;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct InputAnalogActionData_t {
		// Type of data coming from this action, this will match what got specified in the action set
		public EInputSourceMode eMode;
		
		// The current state of this action; will be delta updates for mouse actions
		public float x, y;
		
		// Whether or not this action is currently available to be bound in the active action set
		public byte bActive;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct InputDigitalActionData_t {
		// The current state of this action; will be true if currently pressed
		public byte bState;
		
		// Whether or not this action is currently available to be bound in the active action set
		public byte bActive;
	}

	[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
	public struct InputMotionData_t {
		// Sensor-fused absolute rotation; will drift in heading
		public float rotQuatX;
		public float rotQuatY;
		public float rotQuatZ;
		public float rotQuatW;
		
		// Positional acceleration
		public float posAccelX;
		public float posAccelY;
		public float posAccelZ;
		
		// Angular velocity
		public float rotVelX;
		public float rotVelY;
		public float rotVelZ;
	}

	[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
	public struct SteamItemDetails_t {
		public SteamItemInstanceID_t m_itemId;
		public SteamItemDef_t m_iDefinition;
		public ushort m_unQuantity;
		public ushort m_unFlags; // see ESteamItemFlags
	}

	[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
	public struct SteamPartyBeaconLocation_t {
		public ESteamPartyBeaconLocationType m_eType;
		public ulong m_ulLocationID;
	}

	// connection state to a specified user, returned by GetP2PSessionState()
	// this is under-the-hood info about what's going on with a SendP2PPacket(), shouldn't be needed except for debuggin
	[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
	public struct P2PSessionState_t {
		public byte m_bConnectionActive;		// true if we've got an active open connection
		public byte m_bConnecting;			// true if we're currently trying to establish a connection
		public byte m_eP2PSessionError;		// last error recorded (see enum above)
		public byte m_bUsingRelay;			// true if it's going through a relay server (TURN)
		public int m_nBytesQueuedForSend;
		public int m_nPacketsQueuedForSend;
		public uint m_nRemoteIP;				// potential IP:Port of remote host. Could be TURN server.
		public ushort m_nRemotePort;			// Only exists for compatibility with older authentication api's
	}

	//-----------------------------------------------------------------------------
	// Purpose: Structure that contains an array of const char * strings and the number of those strings
	//-----------------------------------------------------------------------------
	[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
	public struct SteamParamStringArray_t {
		public IntPtr m_ppStrings;
		public int m_nNumStrings;
	}

	// Details for a single published file/UGC
	[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
	public struct SteamUGCDetails_t {
		public PublishedFileId_t m_nPublishedFileId;
		public EResult m_eResult;												// The result of the operation.
		public EWorkshopFileType m_eFileType;									// Type of the file
		public AppId_t m_nCreatorAppID;										// ID of the app that created this file.
		public AppId_t m_nConsumerAppID;										// ID of the app that will consume this file.
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.k_cchPublishedDocumentTitleMax)]
		public string m_rgchTitle;				// title of document
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.k_cchPublishedDocumentDescriptionMax)]
		public string m_rgchDescription;	// description of document
		public ulong m_ulSteamIDOwner;										// Steam ID of the user who created this content.
		public uint m_rtimeCreated;											// time when the published file was created
		public uint m_rtimeUpdated;											// time when the published file was last updated
		public uint m_rtimeAddedToUserList;									// time when the user added the published file to their list (not always applicable)
		public ERemoteStoragePublishedFileVisibility m_eVisibility;			// visibility
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bBanned;													// whether the file was banned
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAcceptedForUse;											// developer has specifically flagged this item as accepted in the Workshop
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bTagsTruncated;											// whether the list of tags was too long to be returned in the provided buffer
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.k_cchTagListMax)]
		public string m_rgchTags;								// comma separated list of all tags associated with this file
		// file/url information
		public UGCHandle_t m_hFile;											// The handle of the primary file
		public UGCHandle_t m_hPreviewFile;										// The handle of the preview file
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.k_cchFilenameMax)]
		public string m_pchFileName;							// The cloud filename of the primary file
		public int m_nFileSize;												// Size of the primary file
		public int m_nPreviewFileSize;										// Size of the preview file
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.k_cchPublishedFileURLMax)]
		public string m_rgchURL;						// URL (for a video or a website)
		// voting information
		public uint m_unVotesUp;												// number of votes up
		public uint m_unVotesDown;											// number of votes down
		public float m_flScore;												// calculated score
		// collection details
		public uint m_unNumChildren;
	}

	// a single entry in a leaderboard, as returned by GetDownloadedLeaderboardEntry()
	[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
	public struct LeaderboardEntry_t {
		public CSteamID m_steamIDUser; // user with the entry - use SteamFriends()->GetFriendPersonaName() & SteamFriends()->GetFriendAvatar() to get more info
		public int m_nGlobalRank;	// [1..N], where N is the number of users with an entry in the leaderboard
		public int m_nScore;			// score as set in the leaderboard
		public int m_cDetails;		// number of int32 details available for this entry
		public UGCHandle_t m_hUGC;		// handle for UGC attached to the entry
	}

	/// Store key/value pair used in matchmaking queries.
	///
	/// Actually, the name Key/Value is a bit misleading.  The "key" is better
	/// understood as "filter operation code" and the "value" is the operand to this
	/// filter operation.  The meaning of the operand depends upon the filter.
	[StructLayout(LayoutKind.Sequential)]
	public struct MatchMakingKeyValuePair_t {
		MatchMakingKeyValuePair_t(string strKey, string strValue) {
			m_szKey = strKey;
			m_szValue = strValue;
		}
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_szKey;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_szValue;
	}

	// structure that contains client callback data
	// see callbacks documentation for more details
	/// Internal structure used in manual callback dispatch
	[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
	public struct CallbackMsg_t {
		public int m_hSteamUser; // Specific user to whom this callback applies.
		public int m_iCallback; // Callback identifier.  (Corresponds to the k_iCallback enum in the callback structure.)
		public IntPtr m_pubParam; // Points to the callback structure
		public int m_cubParam; // Size of the data pointed to by m_pubParam
	}

	/// Structure that describes a gameserver attempting to authenticate
	/// with your central server allocator / matchmaking service ("game coordinator").
	/// This is useful because the game coordinator needs to know:
	///
	/// - What data center is the gameserver running in?
	/// - The routing blob of the gameserver
	/// - Is the gameserver actually trusted?
	///
	/// Using this structure, you can securely communicate this information
	/// to your server, and you can do this WITHOUT maintaining any
	/// whitelists or tables of IP addresses.
	///
	/// See ISteamNetworkingSockets::GetGameCoordinatorServerLogin
	[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
	public struct SteamDatagramGameCoordinatorServerLogin {
		/// Server's identity
		public SteamNetworkingIdentity m_identity;
		
		/// Routing info.  Note that this includes the POPID
		public SteamDatagramHostedAddress m_routing;
		
		/// AppID that the server thinks it is running
		public AppId_t m_nAppID;
		
		/// Unix timestamp when this was generated
		public RTime32 m_rtime;
		
		/// Size of application data
		public int m_cbAppData;
		
		/// Application data.  This is any additional information
		/// that you need to identify the server not contained above.
		/// (E.g. perhaps a public IP as seen by the coordinator service.)
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.k_cbMaxSteamDatagramGameCoordinatorServerLoginAppData)]
		public byte[] m_appData;
	}

	/// Describe the state of a connection.
	[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
	public struct SteamNetConnectionInfo_t {
		
		/// Who is on the other end?  Depending on the connection type and phase of the connection, we might not know
		public SteamNetworkingIdentity m_identityRemote;
		
		/// Arbitrary user data set by the local application code
		public long m_nUserData;
		
		/// Handle to listen socket this was connected on, or k_HSteamListenSocket_Invalid if we initiated the connection
		public HSteamListenSocket m_hListenSocket;
		
		/// Remote address.  Might be all 0's if we don't know it, or if this is N/A.
		/// (E.g. Basically everything except direct UDP connection.)
		public SteamNetworkingIPAddr m_addrRemote;
		public ushort m__pad1;
		
		/// What data center is the remote host in?  (0 if we don't know.)
		public SteamNetworkingPOPID m_idPOPRemote;
		
		/// What relay are we using to communicate with the remote host?
		/// (0 if not applicable.)
		public SteamNetworkingPOPID m_idPOPRelay;
		
		/// High level state of the connection
		public ESteamNetworkingConnectionState m_eState;
		
		/// Basic cause of the connection termination or problem.
		/// See ESteamNetConnectionEnd for the values used
		public int m_eEndReason;
		
		/// Human-readable, but non-localized explanation for connection
		/// termination or problem.  This is intended for debugging /
		/// diagnostic purposes only, not to display to users.  It might
		/// have some details specific to the issue.
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.k_cchSteamNetworkingMaxConnectionCloseReason)]
		public string m_szEndDebug;
		
		/// Debug description.  This includes the connection handle,
		/// connection type (and peer information), and the app name.
		/// This string is used in various internal logging messages
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.k_cchSteamNetworkingMaxConnectionDescription)]
		public string m_szConnectionDescription;
		
		/// Internal stuff, room to change API easily
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		public uint[] reserved;
	}

	/// Quick connection state, pared down to something you could call
	/// more frequently without it being too big of a perf hit.
	[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
	public struct SteamNetworkingQuickConnectionStatus {
		
		/// High level state of the connection
		public ESteamNetworkingConnectionState m_eState;
		
		/// Current ping (ms)
		public int m_nPing;
		
		/// Connection quality measured locally, 0...1.  (Percentage of packets delivered
		/// end-to-end in order).
		public float m_flConnectionQualityLocal;
		
		/// Packet delivery success rate as observed from remote host
		public float m_flConnectionQualityRemote;
		
		/// Current data rates from recent history.
		public float m_flOutPacketsPerSec;
		public float m_flOutBytesPerSec;
		public float m_flInPacketsPerSec;
		public float m_flInBytesPerSec;
		
		/// Estimate rate that we believe that we can send data to our peer.
		/// Note that this could be significantly higher than m_flOutBytesPerSec,
		/// meaning the capacity of the channel is higher than you are sending data.
		/// (That's OK!)
		public int m_nSendRateBytesPerSecond;
		
		/// Number of bytes pending to be sent.  This is data that you have recently
		/// requested to be sent but has not yet actually been put on the wire.  The
		/// reliable number ALSO includes data that was previously placed on the wire,
		/// but has now been scheduled for re-transmission.  Thus, it's possible to
		/// observe m_cbPendingReliable increasing between two checks, even if no
		/// calls were made to send reliable data between the checks.  Data that is
		/// awaiting the Nagle delay will appear in these numbers.
		public int m_cbPendingUnreliable;
		public int m_cbPendingReliable;
		
		/// Number of bytes of reliable data that has been placed the wire, but
		/// for which we have not yet received an acknowledgment, and thus we may
		/// have to re-transmit.
		public int m_cbSentUnackedReliable;
		
		/// If you asked us to send a message right now, how long would that message
		/// sit in the queue before we actually started putting packets on the wire?
		/// (And assuming Nagle does not cause any packets to be delayed.)
		///
		/// In general, data that is sent by the application is limited by the
		/// bandwidth of the channel.  If you send data faster than this, it must
		/// be queued and put on the wire at a metered rate.  Even sending a small amount
		/// of data (e.g. a few MTU, say ~3k) will require some of the data to be delayed
		/// a bit.
		///
		/// In general, the estimated delay will be approximately equal to
		///
		///		( m_cbPendingUnreliable+m_cbPendingReliable ) / m_nSendRateBytesPerSecond
		///
		/// plus or minus one MTU.  It depends on how much time has elapsed since the last
		/// packet was put on the wire.  For example, the queue might have *just* been emptied,
		/// and the last packet placed on the wire, and we are exactly up against the send
		/// rate limit.  In that case we might need to wait for one packet's worth of time to
		/// elapse before we can send again.  On the other extreme, the queue might have data
		/// in it waiting for Nagle.  (This will always be less than one packet, because as soon
		/// as we have a complete packet we would send it.)  In that case, we might be ready
		/// to send data now, and this value will be 0.
		public SteamNetworkingMicroseconds m_usecQueueTime;
		
		/// Internal stuff, room to change API easily
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public uint[] reserved;
	}

	//
	// Ping location / measurement
	//
	/// Object that describes a "location" on the Internet with sufficient
	/// detail that we can reasonably estimate an upper bound on the ping between
	/// the two hosts, even if a direct route between the hosts is not possible,
	/// and the connection must be routed through the Steam Datagram Relay network.
	/// This does not contain any information that identifies the host.  Indeed,
	/// if two hosts are in the same building or otherwise have nearly identical
	/// networking characteristics, then it's valid to use the same location
	/// object for both of them.
	///
	/// NOTE: This object should only be used in the same process!  Do not serialize it,
	/// send it over the wire, or persist it in a file or database!  If you need
	/// to do that, convert it to a string representation using the methods in
	/// ISteamNetworkingUtils().
	[StructLayout(LayoutKind.Sequential, Pack = Packsize.value)]
	public struct SteamNetworkPingLocation_t {
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
		public byte[] m_data;
	}

}

#endif // !DISABLESTEAMWORKS
