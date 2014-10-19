using System;
using Eto.Forms;
using Eto.Drawing;

namespace Eto.GtkSharp
{
	public class PasswordBoxHandler : GtkControl<Gtk.Entry, PasswordBox, PasswordBox.ICallback>, PasswordBox.IHandler
	{
		public PasswordBoxHandler()
		{
			Control = new Gtk.Entry();
			Control.Visibility = false;
			Control.SetSizeRequest(20, -1);
			Control.ActivatesDefault = true;
		}

		public override void AttachEvent(string id)
		{
			switch (id)
			{
				case TextControl.TextChangedEvent:
					Control.Changed += Connector.HandleTextChanged;
					break;
				default:
					base.AttachEvent(id);
					break;
			}
		}

		protected new PasswordBoxConnector Connector { get { return (PasswordBoxConnector)base.Connector; } }

		protected override WeakConnector CreateConnector()
		{
			return new PasswordBoxConnector();
		}

		protected class PasswordBoxConnector : GtkControlConnector
		{
			public new PasswordBoxHandler Handler { get { return (PasswordBoxHandler)base.Handler; } }

			public void HandleTextChanged(object sender, EventArgs e)
			{
				Handler.Callback.OnTextChanged(Handler.Widget, EventArgs.Empty);
			}
		}

		public char PasswordChar
		{
			get { return Control.InvisibleChar; }
			set { Control.InvisibleChar = value; }
		}

		public override string Text
		{
			get { return Control.Text; }
			set { Control.Text = value ?? string.Empty; }
		}

		public Color TextColor
		{
			get { return Control.Style.Text(Gtk.StateType.Normal).ToEto(); }
			set { Control.ModifyText(Gtk.StateType.Normal, value.ToGdk()); }
		}

		public bool ReadOnly
		{
			get { return !Control.IsEditable; }
			set { Control.IsEditable = !value; }
		}

		public int MaxLength
		{
			get { return Control.MaxLength; }
			set { Control.MaxLength = value; }
		}

		public override Color BackgroundColor
		{
			get { return Control.Style.Base(Gtk.StateType.Normal).ToEto(); }
			set { Control.ModifyBase(Gtk.StateType.Normal, value.ToGdk()); }
		}
	}
}
