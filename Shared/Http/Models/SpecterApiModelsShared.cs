using System;
using System.Collections.Generic;
using SpecterSDK.Shared.Http.Interfaces;

namespace SpecterSDK.Shared.Http.Models
{
    /// <summary>
    /// Represents the base structure for all API request data in the SDK.
    /// This abstract class is extended to include specific properties and configurations for different API endpoints.
    /// </summary>
    [Serializable]
    public abstract class SPApiRequestBase : IProjectConfigurable
    {
        public string projectId { get; set; }
    }

    /// <summary>
    /// Represents the base structure for any API requests in the SDK where data can be paginated.
    /// </summary>
    [Serializable]
    public abstract class SPPaginatedApiRequest : SPApiRequestBase
    {
        // The maximum number of data entries to fetch
        public int? limit { get; set; }
        
        /// <summary>
        /// The offset for pagination. Indicates the starting point from which data entries should be fetched.
        /// This is simply the total number of entries you already have, or limit * pageCount (if you maintain a page count)
        /// </summary>
        public int? offset { get; set; }
    }

    /// <summary>
    /// Represents the data needed to request additional entities in an API request.
    /// Entities can be any data structures or objects that the request's response relates to.
    /// Use entities to request additional data from the server in the same request
    /// <example>An entity for a progression system is items - the item rewards for each level</example>
    /// </summary>
    [Serializable]
    public class SPApiRequestEntity
    {
        // Name of the entity
        public string value { get; set; }
                
        // The maximum number of entity instances to fetch
        public int? limit { get; set; }
        
        // the specific attributes of the entity to retrieve
        public List<string> attributes { get; set; }
        
        /// <summary>
        /// The offset for pagination. Indicates the starting point from which entities should be fetched.
        /// This is simply the total number of entities you already have, or limit * pageCount (if you maintain a page count)
        /// </summary>
        public int? offset { get; set; }
    }
    
    /// <summary>
    /// Constants representing various API response statuses.
    /// </summary>
    public struct SPApiStatus
    {
        public const string Success = "success";
        public const string Error = "error";
        public const string Warning = "warning";
        public const string Pending = "pending";
        public const string Incomplete = "incomplete";
        public const string UnprocessableEntity = "unprocessable_entity";
        public const string MultiStatus = "multi-status";
    }

    /// <summary>
    /// Represents the standard structure of an API response from Specter.
    /// This generic class encapsulates the status, error details, message, and actual data returned from the API.
    /// </summary>
    /// <typeparam name="T">Type of data being returned</typeparam>
    [Serializable]
    public class SPApiResponse<T> where T: class, ISpecterApiResponseData, new()
    {
        // Indicates the status of the API response. Can be 'success', 'error', etc.
        public string status { get; set; }
        
        // The HTTP status code associated with the response.
        public int code { get; set; }
        
        // A descriptive message providing more details about the response.
        public string message { get; set; }
        
        // A list of errors, if any, that occurred during the API call.
        public List<SPApiError> errors { get; set; }
        
        /// <summary>
        /// The actual data returned from the API. This is of generic type and represents any class that implements <see cref="ISpecterApiResponseData"/>.
        /// </summary>
        public T data { get; set; }
    }

    /// <summary>
    /// Represents an error in the API response.
    /// This class provides details about the error, including the status code, message, and specific error details.
    /// </summary>
    [Serializable]
    public class SPApiError
    {
        // The http exception that was thrown
        public string status { get; set; }
        
        // The http error code (is always same as the code field in the api response class
        public int code { get; set; }
        
        // Internal error code
        public int errorCode { get; set; }
        
        // Detailed internal error message
        public string errorMessage { get; set; }
        
        // Internal exception thrown
        public string errorStatus { get; set; }
        
        // Standard message for the error type
        public string message { get; set; }
    }

    /// <summary>
    /// A general response backed by a dictionary
    /// This is used in cases where we do not care about the data from the server
    /// <example>When we only care about a success status in the response</example>>
    /// </summary>
    [Serializable]
    public sealed class SPGeneralResponseData : Dictionary<string, object>, ISpecterApiResponseData { }

    /// <summary>
    /// Represents a general list response data class.
    /// </summary>
    /// <remarks>
    /// This class is used as a response data class for general list API requests where we do not care about the type.
    /// Similar to the <see cref="SPGeneralResponseData"/> class
    /// </remarks>
    [Serializable]
    public sealed class SPGeneralListResponseData : List<object>, ISpecterApiResponseData { }

    /// <summary>
    /// Base class for API results returned by the Specter SDK. This class provides a wrapper around
    /// API responses, and is responsible for providing convenience methods, properties and
    /// Specter object initialization.
    /// </summary>
    /// <typeparam name="T">The type of the API response data.</typeparam>
    public abstract class SpecterApiResultBase<T>
    where T: class, ISpecterApiResponseData, new()
    {
        /// <summary>
        /// A flag to determine whether to initialize objects immediately when constructing the result.
        /// </summary>
        public virtual bool LoadObjectsOnResponse => true;
        
        // The Specter API response deserialized from the raw http response
        public SPApiResponse<T> Response { get; set; }
        
        // Convenience properties to quickly access common response data.
        public string Status => Response?.status;
        public int StatusCode => Response?.code ?? 500;
        public string Message => Response?.message;
        public List<SPApiError> Errors => Response?.errors;

        // Convenience property to check if the response has any error(s)
        public bool HasError => Errors is { Count: > 0 } || Status is SPApiStatus.Error;

        /// <summary>
        /// Called by the API client when deserializing the <see cref="SPApiResponse{T}"/>.
        /// </summary>
        /// <param name="force">Flag to force initialize (only needed if <see cref="LoadObjectsOnResponse"/> is overriden to <b>false</b>)</param>
        public void InitSpecterObjects(bool force = false)
        {
            if (!force && !LoadObjectsOnResponse)
                return;
            
            if (Response.data != null)
                InitSpecterObjectsInternal();
        }

        /// <summary>
        /// Abstract method that must be implemented in subclasses.
        /// This function is meant to contain all initialization code.
        /// </summary>
        protected abstract void InitSpecterObjectsInternal();
    }

    /// <summary>
    /// Represents a general API result that contains a dictionary of objects as its response data.
    /// Typically used for API responses that do not have a specific data structure and
    /// where a simple success status is sufficient.
    /// </summary>
    public class SPGeneralResult : SpecterApiResultBase<SPGeneralResponseData>
    {
        public Dictionary<string, object> ObjectDict;

        protected override void InitSpecterObjectsInternal()
        {
            ObjectDict = Response.data;
        }
    }

    /// <summary>
    /// Represents a general API result that contains a list of objects as its response data.
    /// Typically used for API responses that do not care about the array returned and
    /// where a simple success status is sufficient.
    /// </summary>
    public class SPGeneralListResult : SpecterApiResultBase<SPGeneralListResponseData>
    {
        public List<object> ObjectList;

        protected override void InitSpecterObjectsInternal()
        {
            ObjectList = Response.data;
        }
    }
}