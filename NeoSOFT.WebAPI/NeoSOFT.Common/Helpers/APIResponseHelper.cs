
using System.Collections.Generic;
using System.Net;

namespace  NeoSOFT.Common.Helpers
{
    public class ApiResponseHelper
    {
        /// <summary>
        /// Return API response along with result data
        /// </summary>
        /// <typeparam name="T">Type of Result Object</typeparam>
        /// <param name="result">Result Object</param>
        /// <param name="responseStatus">Response Status</param>
        /// <returns></returns>
        public static ApiResponse<T> CreateApiResponse<T>(T result, HttpStatusCode httpStatusCode)
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.Status = httpStatusCode;
            response.Result = result;
            return response;
        }

        /// <summary>
        /// Return API response with error information
        /// </summary>
        /// <typeparam name="T">Type of Result Object</typeparam>
        /// <param name="responseStatus">Response Status</param>
        /// <param name="errors">List of Errors</param>
        /// <returns></returns>
        public static ApiResponse<T> CreateApiResponse<T>(HttpStatusCode httpStatusCode, List<string> errors = null)
        {
            ApiResponse<T> response = new ApiResponse<T>();
            response.Status = httpStatusCode;
            response.Errors = errors;
            return response;
        }

        /// <summary>
        /// Return API response with error information
        /// </summary>
        /// <param name="responseStatus">Response Status</param>
        /// <param name="errors">List of Errors</param>
        /// <returns></returns>
        public static ApiResponse CreateApiResponse(HttpStatusCode httpStatusCode, List<string> errors = null)
        {
            ApiResponse response = new ApiResponse();
            response.Status = httpStatusCode;
            response.Errors = errors;
            return response;
        }
    }
}
