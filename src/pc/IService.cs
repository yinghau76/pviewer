using System;

namespace PViewer
{
	/// <summary>
	/// Summary description for IService.
	/// </summary>
	public interface IService
	{
        bool IsLoaded { get; }
        void Load();
        void Unload();
	}
}
