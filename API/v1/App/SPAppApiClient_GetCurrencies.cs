using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.ObjectModels;
using SpecterSDK.ObjectModels.v1;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v1.App
{
    /// <summary>
    /// Represents a request to get currencies from the Specter App API.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The SPGetCurrenciesRequest class represents a request to get currencies from the Specter App API.
    /// It can be used to specify the filter criteria for the currencies to be returned.
    /// The endpoint is paginated, i.e. it can accept a limit and offset value to retrieve only a certain number of
    /// requested objects. See <see cref="SPPaginatedApiRequest"/> for more info about paginated requests.
    /// </para>
    /// <para>
    /// This request can be sent to the GetCurrenciesAsync method in the SPAppApiClient class to retrieve the currencies from the API.
    /// </para>
    /// </remarks>
    [Serializable]
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SPGetCurrenciesRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Represents a list of currency IDs used as filter criteria for retrieving currencies from the Specter App API.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The currencyIds property is used in the SPGetCurrenciesRequest class to specify the specific currency IDs for filtering the retrieved currencies from the API.
        /// </para>
        /// <para>
        /// This property is a list of strings, where each string represents a unique currency ID that is set in the dashboard.
        /// </para>
        /// </remarks>
        public List<string> currencyIds { get; set; }
        
        /// <summary>
        /// Represent a list of tags which you configured on the dashboard
        /// <remarks>
        /// This property is used to filter out resources which contain the specified tags and return only those in the API call.
        /// </remarks>>
        /// </summary>
        public List<string> includeTags { get; set; }
        
        /// <summary>
        /// Type of currency to fetch. See <see cref="SPCurrencyType"/> for available types
        /// </summary>
        public SPCurrencyType type { get; set; }
        
        /// <summary>
        /// Filter to search for and retrieve currencies by their name.
        /// Multiple currencies can be fetched by setting a name substring
        /// <example>
        /// Currencies named Gold Coin and Silver Coin can be fetched by searching for 'coin' or 'Coin' </example>
        /// </summary>
        public string search { get; set; }
    }
    
    /// <summary>
    /// Represents the result of the GetCurrenciesAsync method in the SPAppApiClient.
    /// </summary>
    public class SPGetCurrenciesResult : SpecterApiResultBase<SPGetCurrenciesResponseData>
    {
        // List of all currencies fetched
        public List<SpecterCurrency> Currencies;
        
        // Total count of currencies configured on the dashboard
        public int TotalCurrencyCount;
        
        protected override void InitSpecterObjectsInternal()
        {
            Currencies = new();
            foreach (var currency in Response.data.currencies)
            {
                Currencies.Add(new SpecterCurrency(currency));
            }
            TotalCurrencyCount = Response.data.totalCount;
        }   
    }
    
    public partial class SPAppApiClient
    {
        /// <summary>
        /// Get the list of currencies asynchronously.
        /// </summary>
        /// <param name="request">
        /// The request object that contains parameters for the API call. The details of the request structure can be found in <see cref="SPGetCurrenciesRequest"/>.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the <see cref="SPGetCurrenciesResult"/> with the result of the API call.
        /// </returns>
        public async Task<SPGetCurrenciesResult> GetCurrenciesAsync(SPGetCurrenciesRequest request) 
        {
            var result = await PostAsync<SPGetCurrenciesResult, SPGetCurrenciesResponseData>("/v1/client/app/get-currencies",AuthType,request);
            return result;
        }
    }
}

