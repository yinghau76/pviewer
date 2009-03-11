using System;
using System.Runtime.Serialization;

namespace PViewer
{
	/// <summary>
	/// This is the base exception for all PViewer exceptions.
	/// </summary>
	public class PViewerException : ApplicationException
	{
		public PViewerException() 
			: base()
		{
		}

		public PViewerException(string message)
			: base(message)
		{
		}

		public PViewerException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected PViewerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
