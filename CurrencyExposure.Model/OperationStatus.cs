using System;
using System.Diagnostics;

namespace CurrencyExposure.Model
{
	[DebuggerDisplay("Status: {Status}")]
	public class OperationStatus
	{
		public OperationStatus()
		{
		}

		public OperationStatus(bool status, string message)
		{
			Status = status;
			Message = message;
		}

		public bool Status { get; set; }
		public int RecordsAffected { get; set; }
		public string Message { get; set; }
		public int NewId { get; set; }

		public bool IsException
		{
			get { return !string.IsNullOrEmpty(ExceptionMessage); }
		}

		public string ExceptionMessage { get; set; }
		public string ExceptionStackTrace { get; set; }
		public string ExceptionInnerMessage { get; set; }
		public string ExceptionInnerStackTrace { get; set; }
		public bool NeedConfirmation { get; set; }
		public string ConfirmationToken { get; set; }
		public string GetFailureMessage()
		{
			string msg = string.Format("Operation Status has Failed: {0}", Message);

			if (!string.IsNullOrEmpty(ExceptionMessage))
			{
				msg = msg + string.Format("{0}Exception Message: {1}{0}Stack Trace: {2}",
					Environment.NewLine, ExceptionMessage, ExceptionStackTrace);
			}

			if (!string.IsNullOrEmpty(ExceptionInnerMessage))
				msg = msg +
					  string.Format(
						  "{0}>>>>> Inner Exception >>>>>{0}Inner Exception Message: {1}{0}Inner Exception StackTrace:{2}",
						  Environment.NewLine, ExceptionInnerMessage, ExceptionInnerStackTrace);

			return msg;
		}

		public virtual OperationStatus CreateFromException(string message, Exception ex)
		{
			OperationStatus opStatus = new OperationStatus
			{
				Status = false,
				Message = message,
			};

			if (ex != null)
			{
				opStatus.ExceptionMessage = ex.Message;
				opStatus.ExceptionStackTrace = ex.StackTrace;
				opStatus.ExceptionInnerMessage = (ex.InnerException == null) ? null : ex.InnerException.Message;
				opStatus.ExceptionInnerStackTrace = (ex.InnerException == null) ? null : ex.InnerException.StackTrace;
			}
			return opStatus;
		}
	}

	public class OperationStatus<T> : OperationStatus
	{
		public OperationStatus()
		{
		}

		public OperationStatus(bool status, string message)
		{
			Status = status;
			Message = message;
		}

		public OperationStatus(bool status, string message, T operationObject)
		{
			Status = status;
			Message = message;
			OperationObject = operationObject;
		}

		public T OperationObject { get; set; }

		public object GetOperationObject()
		{
			return (object)OperationObject;
		}

		public new OperationStatus<T> CreateFromException(string message, Exception ex)
		{
			OperationStatus<T> opStatus = new OperationStatus<T>()
			{
				Status = false,
				Message = message,
			};

			if (ex != null)
			{
				opStatus.ExceptionMessage = ex.Message;
				opStatus.ExceptionStackTrace = ex.StackTrace;
				opStatus.ExceptionInnerMessage = (ex.InnerException == null) ? null : ex.InnerException.Message;
				opStatus.ExceptionInnerStackTrace = (ex.InnerException == null) ? null : ex.InnerException.StackTrace;
			}
			return opStatus;
		}
	}

}
