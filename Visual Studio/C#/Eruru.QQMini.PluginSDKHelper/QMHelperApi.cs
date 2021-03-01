using System;
using System.IO;
using QQMini.PluginSDK.Core;

namespace Eruru.QQMini.PluginSDKHelper {

	public static class QMHelperApi {

		public static bool IsDebugMode { get; set; }
		public static Action<string> OnSend;
		public static Action<string> OnDebug;

		const string DebugPluginDataDirectory = "Data/";

		public static void SendGroupMessage (long robotQQ, long sendGroup, string message) {
			if (IsDebugMode) {
				OnSend?.Invoke (message.ToString ());
				return;
			}
			QMApi.CurrentApi.SendGroupMessage (robotQQ, sendGroup, message);
		}

		public static void SendGroupTempMessage (long robotQQ, long fromGroup, long sendQQ, string message) {
			if (IsDebugMode) {
				OnSend?.Invoke (message.ToString ());
				return;
			}
			QMApi.CurrentApi.SendGroupTempMessage (robotQQ, fromGroup, sendQQ, message);
		}

		public static void SendFriendMessage (long robotQQ, long sendGroup, string message) {
			if (IsDebugMode) {
				OnSend?.Invoke (message.ToString ());
				return;
			}
			QMApi.CurrentApi.SendFriendMessage (robotQQ, sendGroup, message);
		}

		public static string GetPluginDataDirectory () {
			if (IsDebugMode) {
				Directory.CreateDirectory (DebugPluginDataDirectory);
				return DebugPluginDataDirectory;
			}
			return QMApi.CurrentApi.GetPluginDataDirectory ();
		}

		public static void Debug (string text) {
			if (IsDebugMode) {
				OnDebug?.Invoke (text);
				return;
			}
			QMLog.CurrentApi.Debug (text);
		}
		public static void Debug (object text) {
			Debug (text.ToString ());
		}

	}

}