using System;

namespace TcpClientSendReceiveTest.Helpers
{
    //---------------------------------------------------------------------------------------------
    /// <summary>
    /// Generic result from api
    /// </summary>
    public class GenericResult
    {
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericResult"/> class.
        /// When an errormessage is provided, succeeded will automatically become false.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public GenericResult(string errorMessage)
        {
            Succeeded = errorMessage == null;
            ErrorMessage = errorMessage;
            ExceptionObject = new Exception(errorMessage);
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericResult"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public GenericResult(Exception exception)
            : this(exception.Message)
        {
            ExceptionObject = exception;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericResult"/> class.
        /// By default, succeeded will become true.
        /// </summary>
        public GenericResult()
        {
            Succeeded = true;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; set; }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the exception object.
        /// </summary>
        /// <value>
        /// The exception object.
        /// </value>
        public Exception ExceptionObject { get; set; }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Succeeded
        /// </summary>
        public bool Succeeded { get; set; }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Has error
        /// </summary>
        public bool HasError => !Succeeded;
    }

    //---------------------------------------------------------------------------------------------
    /// <summary>
    /// Generic result from api
    /// </summary>
    /// <typeparam name="T">result object</typeparam>
    public class GenericResult<T> : GenericResult
    {
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericResult{T}"/> class.
        /// </summary>
        public GenericResult()
        {
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericResult{T}"/> class.
        /// </summary>
        /// <param name="errorMessage">Error message</param>
        public GenericResult(string errorMessage)
            : base(errorMessage)
        {
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericResult{T}"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public GenericResult(Exception exception)
            : base(exception)
        {
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericResult{T}"/> class.
        /// This is a convenience constructor, to be able to pass a value or errormessage at once.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="errorMessage">The error message.</param>
        public GenericResult(T value, string errorMessage = null)
            : base(errorMessage)
        {
            if (value != null && !string.IsNullOrEmpty(errorMessage))
                throw new InvalidOperationException("When the error message is provided, value must be null.");

            Value = value;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Value
        /// </summary>
        public T Value { get; set; }
    }
}
