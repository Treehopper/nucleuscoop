﻿namespace x360ce.Engine
{
	using System;
	using System.Web.Services;
	using System.Diagnostics;
	using System.Web.Services.Protocols;
	using System.Xml.Serialization;
	using System.ComponentModel;
	using System.Threading;
	using System.Web.Services.Description;
	using System.Windows.Forms;
	using x360ce.Engine.Data;
	using System.Collections.Generic;

	/// <remarks/>
	[WebServiceBindingAttribute(Name = "x360ceSoap", Namespace = ns)]
	//[System.Xml.Serialization.XmlIncludeAttribute(typeof(StructuralObject))]
	//[System.Xml.Serialization.XmlIncludeAttribute(typeof(EntityKeyMember[]))]
	public partial class WebServiceClient : SoapHttpClientProtocol, IWebService
	{

		#region Main Methods

		const string ns = "http://x360ce.com/";
		bool useDefaultCredentialsSetExplicitly;

		/// <remarks/>
		public WebServiceClient()
		{
			if ((IsLocalFileSystemWebService(Url) == true))
			{
				UseDefaultCredentials = true;
				useDefaultCredentialsSetExplicitly = false;
			}
			else
			{
				useDefaultCredentialsSetExplicitly = true;
			}
		}

		public new string Url
		{
			get { return base.Url; }
			set
			{
				if ((((IsLocalFileSystemWebService(base.Url) == true)
							&& (useDefaultCredentialsSetExplicitly == false))
							&& (IsLocalFileSystemWebService(value) == false)))
				{
					base.UseDefaultCredentials = false;
				}
				base.Url = value;
			}
		}

		public new bool UseDefaultCredentials
		{
			get
			{
				return base.UseDefaultCredentials;
			}
			set
			{
				base.UseDefaultCredentials = value;
				useDefaultCredentialsSetExplicitly = true;
			}
		}

		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		bool IsLocalFileSystemWebService(string url)
		{
			if (((url == null) || (url == string.Empty))) return false;
			System.Uri wsUri = new System.Uri(url);
			if (((wsUri.Port >= 1024)
						&& (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0)))
			{
				return true;
			}
			return false;
		}

		class InvokeUserState
		{
			public InvokeUserState(object userState, EventHandler<ResultEventArgs> handler)
			{
				UserState = userState;
				Handler = handler;
			}
			public object UserState;
			public EventHandler<ResultEventArgs> Handler;
		}

		void InvokeAsync(string method, EventHandler<ResultEventArgs> completedEvent, object userState, object[] args)
		{
			var invokeUserState = new InvokeUserState(userState, completedEvent);
			InvokeAsync(method, args, OnAsyncOperationCompleted, invokeUserState);
		}

		void OnAsyncOperationCompleted(object arg)
		{
			var invokeArgs = (InvokeCompletedEventArgs)arg;
			var invokeUserState = (InvokeUserState)invokeArgs.UserState;
			if (invokeUserState.Handler == null) return;
			var args = new ResultEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeUserState.UserState);
			invokeUserState.Handler(this, args);
		}

		#endregion

		#region Method: SignIn

		public event EventHandler<ResultEventArgs> SignInCompleted;

		public void SignInAsync(string username, string password, object userState = null)
		{
			InvokeAsync("SignIn", SignInCompleted, userState, new object[] { username, password });
		}

		[SoapDocumentMethodAttribute("http://x360ce.com/SignIn",
			RequestNamespace = ns, ResponseNamespace = ns,
			Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public KeyValueList SignIn(string username, string password)
		{
			object[] results = Invoke("SignIn", new object[] { username, password });
			return (KeyValueList)results[0];
		}

		#endregion

		#region Method: SearchSettings

		public event EventHandler<ResultEventArgs> SearchSettingsCompleted;

		[SoapDocumentMethodAttribute("http://x360ce.com/SearchSettings",
			RequestNamespace = ns, ResponseNamespace = ns,
			Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public SearchResult SearchSettings(SearchParameter[] args)
		{
			object[] results = Invoke("SearchSettings", new object[] { args });
			return (SearchResult)results[0];
		}

		public void SearchSettingsAsync(SearchParameter[] args, object userState = null)
		{
			InvokeAsync("SearchSettings", SearchSettingsCompleted, userState, new object[] { args });
		}

		#endregion

		#region Method: SaveSetting

		public event EventHandler<ResultEventArgs> SaveSettingCompleted;

		[SoapDocumentMethodAttribute("http://x360ce.com/SaveSetting",
			RequestNamespace = ns, ResponseNamespace = ns,
			Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string SaveSetting(Setting s, PadSetting ps)
		{
			object[] results = Invoke("SaveSetting", new object[] { s, ps });
			return (string)results[0];
		}

		public void SaveSettingAsync(Setting s, PadSetting ps, object userState = null)
		{
			InvokeAsync("SaveSetting", SaveSettingCompleted, userState, new object[] { s, ps });
		}

		#endregion

		#region Method: DeleteSetting

		public event EventHandler<ResultEventArgs> DeleteSettingCompleted;

		[SoapDocumentMethodAttribute("http://x360ce.com/DeleteSetting",
			RequestNamespace = ns, ResponseNamespace = ns,
			Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string DeleteSetting(Setting s)
		{
			object[] results = Invoke("DeleteSetting", new object[] { s });
			return (string)results[0];
		}

		public void DeleteSettingAsync(Setting s, object userState = null)
		{
			InvokeAsync("DeleteSetting", DeleteSettingCompleted, userState, new object[] { s });
		}

		#endregion

		#region Method: LoadSetting

		public event EventHandler<ResultEventArgs> LoadSettingCompleted;

		[SoapDocumentMethodAttribute("http://x360ce.com/LoadSetting",
			RequestNamespace = ns, ResponseNamespace = ns,
			Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public SearchResult LoadSetting(System.Guid[] checksum)
		{
			object[] results = Invoke("LoadSetting", new object[] { checksum });
			return (SearchResult)results[0];
		}

		public void LoadSettingAsync(System.Guid[] checksum, object userState = null)
		{
			InvokeAsync("LoadSetting", LoadSettingCompleted, userState, new object[] { checksum });
		}

		#endregion

		#region Method: GetPrograms

		public event EventHandler<ResultEventArgs> GetProgramsCompleted;

		[SoapDocumentMethodAttribute("http://x360ce.com/GetPrograms",
			RequestNamespace = ns, ResponseNamespace = ns,
			Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public List<Program> GetPrograms(EnabledState isEnabled, int minInstanceCount)
		{
			object[] results = Invoke("GetPrograms", new object[] { isEnabled, minInstanceCount });
			return (List<Program>)results[0];
		}

		public void GetProgramsAsync(EnabledState isEnabled, int minInstanceCount, object userState = null)
		{
			InvokeAsync("GetPrograms", GetProgramsCompleted, userState, new object[] { isEnabled, minInstanceCount });
		}

		#endregion

		#region Method: GetProgram

		public event EventHandler<ResultEventArgs> GetProgramCompleted;

		[SoapDocumentMethodAttribute("http://x360ce.com/GetProgram",
			RequestNamespace = ns, ResponseNamespace = ns,
			Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public Program GetProgram(string fileName, string fileProductName)
		{
			object[] results = Invoke("GetProgram", new object[] { fileName, fileProductName });
			return (Program)results[0];
		}

		public void GetProgramsAsync(string fileName, string fileProductName, object userState = null)
		{
			InvokeAsync("GetProgram", GetProgramCompleted, userState, new object[] { fileName, fileProductName });
		}

		#endregion

		#region Method: GetVendors

		public event EventHandler<ResultEventArgs> GetVendorsCompleted;

		[SoapDocumentMethodAttribute("http://x360ce.com/GetVendors",
			RequestNamespace = ns, ResponseNamespace = ns,
			Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public List<Vendor> GetVendors()
		{
			object[] results = Invoke("GetVendors", new object[] { });
			return (List<Vendor>)results[0];
		}

		public void GetVendorssAsync(object userState = null)
		{
			InvokeAsync("GetVendors", GetVendorsCompleted, userState, new object[] { });
		}

		#endregion

		#region Method: GetSettingsData

		public event EventHandler<ResultEventArgs> GetSettingsDataCompleted;

		[SoapDocumentMethodAttribute("http://x360ce.com/GetSettingsData",
			RequestNamespace = ns, ResponseNamespace = ns,
			Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public SettingsData GetSettingsData()
		{
			object[] results = Invoke("GetSettingsData", new object[] { });
			return (SettingsData)results[0];
		}

		public void GetSettingsDatasAsync(object userState = null)
		{
			InvokeAsync("GetSettingsData", GetSettingsDataCompleted, userState, new object[] { });
		}

		#endregion

		#region Method: SetProgram

		public event EventHandler<ResultEventArgs> SetProgramCompleted;

		[SoapDocumentMethodAttribute("http://x360ce.com/SetProgram",
			RequestNamespace = ns, ResponseNamespace = ns,
			Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string SetProgram(Program p)
		{
			object[] results = Invoke("SetProgram", new object[] { p });
			return (string)results[0];
		}

		public void SetProgramAsync(Program p, object userState = null)
		{
			InvokeAsync("SetProgram", SetProgramCompleted, userState, new object[] { p });
		}

		#endregion

		#region Method: UpdateGames

		public event EventHandler<ResultEventArgs> SetGamesCompleted;

		[SoapDocumentMethodAttribute("http://x360ce.com/SetGames",
			RequestNamespace = ns, ResponseNamespace = ns,
			Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string SetGames(CloudAction action, List<Game> games)
		{
			object[] results = Invoke("SetGames", new object[] { action, games });
			return (string)results[0];
		}

		public void SetGamesAsync(CloudAction action, List<Game> games, object userState = null)
		{
			InvokeAsync("SetGames", SetGamesCompleted, userState, new object[] { action, games });
		}

		#endregion

		#region Method: SignOut

		public event EventHandler<ResultEventArgs> SignOutCompleted;

		[SoapDocumentMethodAttribute("http://x360ce.com/SignOut",
			RequestNamespace = ns, ResponseNamespace = ns,
			Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public KeyValueList SignOut()
		{
			object[] results = Invoke("SignOut", new object[] { });
			return (KeyValueList)results[0];
		}

		public void SignOutsAsync(object userState = null)
		{
			InvokeAsync("SignOut", SignOutCompleted, userState, new object[] { });
		}

		#endregion
	}

	public partial class ResultEventArgs : AsyncCompletedEventArgs
	{
		internal ResultEventArgs(object[] results, Exception exception, bool cancelled, object userState) :
			base(exception, cancelled, userState) { _results = results; }

		object[] _results;
		public object Result
		{
			get
			{
				RaiseExceptionIfNecessary();
				return _results[0];
			}
		}
	}


}
