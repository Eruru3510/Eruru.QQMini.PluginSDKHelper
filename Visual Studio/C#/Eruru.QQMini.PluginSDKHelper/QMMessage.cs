using System;
using System.Text;

namespace Eruru.QQMini.PluginSDKHelper {

	public class QMMessage<TTag> {

		public QMMessageType Type { get; }
		public TTag Tag { get; set; }
		public long RobotQQ { get; }
		public long Group { get; }
		public long QQ { get; }
		public long MessageID { get; }
		public long MessageNumber { get; }
		public string Text { get; }
		public DateTime DateTime { get; }

		public QMMessage (QMMessageType type, long robotQQ, long group, long qq, long messageID, long messageNumber, string text) {
			Type = type;
			RobotQQ = robotQQ;
			Group = group;
			QQ = qq;
			MessageID = messageID;
			MessageNumber = messageNumber;
			Text = text ?? throw new ArgumentNullException (nameof (text));
			DateTime = DateTime.Now;
		}
		public QMMessage (QMMessageType type, long robotQQ, long qq, long messageID, long messageNumber, string text) :
		this (type, robotQQ, default, qq, messageID, messageNumber, text) {
			if (text is null) {
				throw new ArgumentNullException (nameof (text));
			}
		}

		public void Reply (StringBuilder text, bool at, bool spaceAfterAt, bool newLineAfterAt) {
			if (text is null) {
				throw new ArgumentNullException (nameof (text));
			}
			Send (Type, RobotQQ, Group, QQ, text, at, spaceAfterAt, newLineAfterAt);
		}
		public void Reply (string text, bool at, bool spaceAfterAt, bool newLineAfterAt) {
			if (text is null) {
				throw new ArgumentNullException (nameof (text));
			}
			Send (Type, RobotQQ, Group, QQ, text, at, spaceAfterAt, newLineAfterAt);
		}
		public void Reply (object text, bool at, bool spaceAfterAt, bool newLineAfterAt) {
			if (text is null) {
				throw new ArgumentNullException (nameof (text));
			}
			Send (Type, RobotQQ, Group, QQ, text.ToString (), at, spaceAfterAt, newLineAfterAt);
		}
		public void Reply (StringBuilder text) {
			if (text is null) {
				throw new ArgumentNullException (nameof (text));
			}
			Send (Type, RobotQQ, Group, QQ, text);
		}
		public void Reply (string text) {
			if (text is null) {
				throw new ArgumentNullException (nameof (text));
			}
			Send (Type, RobotQQ, Group, QQ, text);
		}
		public void Reply (object text) {
			if (text is null) {
				throw new ArgumentNullException (nameof (text));
			}
			Send (Type, RobotQQ, Group, QQ, text.ToString ());
		}

		public override string ToString () {
			return Text;
		}

		public static implicit operator string (QMMessage<TTag> message) {
			if (message is null) {
				throw new ArgumentNullException (nameof (message));
			}
			return message.Text;
		}

		public static void Send (QMMessageType type, long robotQQ, long group, long qq, StringBuilder text, bool at = true, bool spaceAfterAt = true, bool newLineAfterAt = true) {
			if (text is null) {
				throw new ArgumentNullException (nameof (text));
			}
			switch (type) {
				case QMMessageType.Group:
					text = At (qq, text, at, spaceAfterAt, newLineAfterAt);
					QMHelperApi.SendGroupMessage (robotQQ, group, text.ToString ());
					break;
				case QMMessageType.GroupTemp:
					QMHelperApi.SendGroupTempMessage (robotQQ, group, qq, text.ToString ());
					break;
				case QMMessageType.Friend:
					QMHelperApi.SendFriendMessage (robotQQ, qq, text.ToString ());
					break;
				default:
					throw new NotImplementedException (type.ToString ());
			}
		}
		public static void Send (QMMessageType type, long robotQQ, long group, long qq, string text, bool at = true, bool spaceAfterAt = true, bool newLineAfterAt = true) {
			if (text is null) {
				throw new ArgumentNullException (nameof (text));
			}
			Send (type, robotQQ, group, qq, new StringBuilder (text), at, spaceAfterAt, newLineAfterAt);
		}

		static StringBuilder At (long qq, StringBuilder text, bool at, bool spaceAfterAt, bool newLineAfterAt) {
			if (text is null) {
				throw new ArgumentNullException (nameof (text));
			}
			StringBuilder stringBuilder = new StringBuilder ();
			if (at && qq > 0) {
				stringBuilder.Append ($"[@{qq}]");
				if (spaceAfterAt) {
					stringBuilder.Append (' ');
				}
				if (newLineAfterAt) {
					stringBuilder.AppendLine ();
				}
			}
			stringBuilder.Append (text);
			return stringBuilder;
		}

	}

}