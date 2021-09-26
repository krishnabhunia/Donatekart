using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Donatekart.Models;
using Newtonsoft.Json;

namespace Donatekart.Controllers
{
    public class DonatekartController : ApiController
    {
        private const string strAPI = "https://testapi.donatekart.com/api/campaign";
        private readonly HttpClient httpClient = new HttpClient();
        private List<Campaign> campaigns;

        [HttpGet]
        public async Task<HttpResponseMessage> GetTotalSort()
        {
            try
            {
                if(campaigns == null)
                {
                    var stringResponse = httpClient.GetStringAsync(strAPI);
                    var msg = await stringResponse;
                    campaigns = JsonConvert.DeserializeObject<List<Campaign>>(msg);

                    var resToSend = campaigns.OrderByDescending(x=> x.totalAmount).Select(x => new { x.title,x.totalAmount,x.backersCount, x.endDate });
                    return Request.CreateResponse(HttpStatusCode.OK, resToSend);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, campaigns);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetActiveCamp()
        {
            try
            {
                if (campaigns == null)
                {
                    var stringResponse = httpClient.GetStringAsync(strAPI);
                    var msg = await stringResponse;
                    campaigns = JsonConvert.DeserializeObject<List<Campaign>>(msg);

                    var resToSend = campaigns
                        .Where(x => x.endDate >= DateTime.Today & x.endDate <= DateTime.Today.AddDays(30));
                    return Request.CreateResponse(HttpStatusCode.OK, resToSend);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, campaigns);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetCloseCamp()
        {
            try
            {
                if (campaigns == null)
                {
                    var stringResponse = httpClient.GetStringAsync(strAPI);
                    var msg = await stringResponse;
                    campaigns = JsonConvert.DeserializeObject<List<Campaign>>(msg);

                    var resToSend = campaigns
                        .Where(x => x.endDate < DateTime.Today || x.procuredAmount >= x.totalAmount);
                    return Request.CreateResponse(HttpStatusCode.OK, resToSend);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, campaigns);
        }
    }
}
