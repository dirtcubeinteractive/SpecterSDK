using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpecterSDK.APIModels.ClientModels;
using SpecterSDK.APIModels.ClientModels.v1;
using SpecterSDK.ObjectModels;
using SpecterSDK.ObjectModels.v1;
using SpecterSDK.Shared;
using SpecterSDK.Shared.Networking.Models;

namespace SpecterSDK.API.v1.Competitions
{
    [Serializable]
    public class SPGetMyCompetitionsRequest : SPPaginatedApiRequest
    {
        /// <summary>
        /// Filter to retrieve specific competitions by their Id.
        /// </summary>
        public List<string> competitionIds {  get; set; }
        
        /// <summary>
        /// Filter retrieved competition by schedule statuses. See <see cref="SPScheduleStatus"/> for possible values
        /// </summary>
        public List<SPScheduleStatus> scheduleStatuses { get; set; }
        
        /// <summary>
        /// Filter to retrieve only certain types of competitions. See <see cref="SPCompetitionFormat"/> for possible values
        /// </summary>
        public List<SPCompetitionFormat> formatTypes { get; set; }
    }

    public class SPGetMyCompetitionsResult : SpecterApiResultBase<SPResponseDataList<SPEnteredCompetitionResponseData>>
    {
        public List<SpecterEnteredCompetition> MyCompetions;
        
        protected override void InitSpecterObjectsInternal()
        {
            MyCompetions = new List<SpecterEnteredCompetition>();
            foreach (var comp in Response.data)
            {
                MyCompetions.Add(new SpecterEnteredCompetition(comp));
            }
        }
    }
    
    public partial class SPCompetitionsApiClient
    {
        public async Task<SPGetMyCompetitionsResult> GetMyCompetitionsAsync(SPGetMyCompetitionsRequest request)
        {
            var result = await PostAsync<SPGetMyCompetitionsResult, SPResponseDataList<SPEnteredCompetitionResponseData>>("/v1/client/competitions/get-my-competitions", AuthType, request);
            return result;
        }
    }
}