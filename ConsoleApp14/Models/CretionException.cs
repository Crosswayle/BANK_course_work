using System;
using System.Runtime.Serialization;

namespace Bank
{
	[Serializable]
	internal class CretionException : Exception
	{
		public CretionException()
		{
		}

		public CretionException(string message) : base(message)
		{
		}

		public CretionException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected CretionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}