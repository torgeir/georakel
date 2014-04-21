using System;

namespace Georakel.Core
{
	public class Result<T>
	{
		public T Value { get; private set;  }

		public Exception Error { get; private set; }

		public Result(T value)
		{
			Value = value;
		}

		public Result(Exception error)
		{
			Error = error;
		}

		public bool HasError()
		{
			return Error != null;
		}
	}
}