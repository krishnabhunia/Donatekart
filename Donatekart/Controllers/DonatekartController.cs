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
        private const string strStatus = "Campaign is not assigned";
        private readonly static HttpClient httpClient = new HttpClient();
        private readonly static List<Campaign> campaigns = FetchAsyncD().Result;

        private async static Task<List<Campaign>> FetchAsyncD()
        {
            try
            {
                string stringResponse = await httpClient.GetStringAsync(strAPI).ConfigureAwait(false);
                var res = JsonConvert.DeserializeObject<List<Campaign>>(stringResponse);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetTotalSort()
        {
            try
            {
                if (campaigns != null)
                {
                    var resToSend = campaigns.OrderByDescending(x => x.totalAmount).Select(x => new { x.title, x.totalAmount, x.backersCount, x.endDate });
                    return Request.CreateResponse(HttpStatusCode.OK, resToSend);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, strStatus);
        }

        [HttpGet]
        public HttpResponseMessage GetActiveCamp()
        {
            try
            {
                if (campaigns != null)
                {
                    var resToSend = campaigns
                        .Where(x => x.endDate >= DateTime.Today & x.endDate <= DateTime.Today.AddDays(30));
                    return Request.CreateResponse(HttpStatusCode.OK, resToSend);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, strStatus);
        }

        [HttpGet]
        public HttpResponseMessage GetCloseCamp()
        {
            try
            {
                if (campaigns != null)
                {
                    var resToSend = campaigns
                        .Where(x => x.endDate < DateTime.Today || x.procuredAmount >= x.totalAmount);
                    return Request.CreateResponse(HttpStatusCode.OK, resToSend);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, strStatus);
        }
    }
}
