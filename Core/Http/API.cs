using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Collections.Generic;
using ModernHttpClient;
using System.Net.Http.Headers;
using Core.Travel;
using Core.Util;
using System.IO; 

namespace Core.Http
{
	public class API
	{

		//private static int _expires_in;
        private string _nyckel = "";
        private string _hemlighet = "";

        private int _expiresIn = 0;
        private DateTime _expiresInDate;
        private string _accessToken;

        private async Task<HttpResult> GetToken()
        {
            if (this._accessToken != null && DateTime.Now < this._expiresInDate)
            {
                return new HttpResult(true, null); //ve the token
            }

            try
            {
                /*
                 *  Set up basic autentisering
                 */
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(_nyckel + ":" + _hemlighet);
                var base64 = System.Convert.ToBase64String(plainTextBytes);


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.vasttrafik.se");
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("grant_type", "client_credentials"),
                        new KeyValuePair<string, string>("scope", Injected.Instance.Platform.GetId())
                    });

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", System.Convert.ToBase64String(plainTextBytes));

                    content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                    var result = await client.PostAsync("token", content);
                    result.EnsureSuccessStatusCode();
                    string resultContent = await result.Content.ReadAsStringAsync();

                    if (resultContent == null ||
                        resultContent.Length < 3)
                    {
                        throw new ArgumentException("Bad data");
                    }

                    try
                    {

                        var res = JObject.Parse(resultContent);

                        this._expiresIn = (int)res["expires_in"];
                        this._accessToken = (string)res["access_token"];
                        this._expiresInDate = DateTime.Now.AddSeconds(_expiresIn);


                        return new HttpResult(true, null);
                    }
                    catch (Exception e)
                    {
                        throw new ArgumentException("Bad data", e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Bad connection", e.Message);
            }

        }


			

		public async Task<HttpResult> GetStop(StoppingPlace stop, DateTime date) {

            await GetToken();

            try
            {
                /*
                 *  Set up basic autentisering
                 */
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(" https://api.vasttrafik.se/bin/rest.exe/v2/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._accessToken);


                    var appendstring = "?format=json&id=" + stop.Id + "&date=" + date.ToString("yyyy-MM-dd") + "&time=" + date.ToString("HH:mm");
                    var result = await client.GetAsync("departureBoard" + appendstring);
                    result.EnsureSuccessStatusCode();

                    string resultContent = await result.Content.ReadAsStringAsync();

                    if (resultContent == null ||
                        resultContent.Length < 3)
                    {
                        throw new ArgumentException("Bad data");
                    }

                    try
                    {
                        var res = JObject.Parse(resultContent);
                        
                        JArray ArrivalArr = (JArray)res["DepartureBoard"]["Departure"];
                        var arrivalManager = new StoppingTimeManager();

                        foreach (var obj in (JToken)ArrivalArr)
                        {
                            var datee = DateTime.Parse((string)obj.SelectToken("date") + " " + (string)obj.SelectToken("time"));
                            arrivalManager.Add(new TravelTime()
                            {
                                Date = DateTime.Parse((string)obj.SelectToken("date") + " " + (string)obj.SelectToken("time")),
                                direction = (string)obj.SelectToken("direction"),
                                name = (string)obj.SelectToken("name")
                            });
                        }

                        return new HttpResult(true, arrivalManager);
                        //return new HttpResult(true, null);
                    }
                    catch (Exception e)
                    {
                        throw new ArgumentException("Bad data", e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Bad connection", e.Message);
            }
		}




		public async Task<HttpResult> GetStops(string text)
		{
            await GetToken();

			var encoded = System.Net.WebUtility.UrlEncode(text);

            try
            {
                /*
                 *  Set up basic autentisering
                 */
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(" https://api.vasttrafik.se/bin/rest.exe/v2/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._accessToken);


                    var result = await client.GetAsync("location.name" + "?format=json&input=" +encoded);
                    result.EnsureSuccessStatusCode();


                    string resultContent = await result.Content.ReadAsStringAsync();


                    if (resultContent == null ||
                        resultContent.Length < 3)
                    {
                        throw new ArgumentException("Bad data");
                    }

                    try
                    {

                        var res = JObject.Parse(resultContent);

                         
                        JArray locationListArr = (JArray) res["LocationList"]["StopLocation"];
                        var stopManager = new StoppingPlaces();

                        var length = locationListArr.Count;
                        foreach(var obj in (JToken)locationListArr) {

                            stopManager.Add(new StoppingPlace() {
                                Lat = (double)obj.SelectToken("lat"),
                                Lon = (double)obj.SelectToken("lon"),
                                Id = (string)obj.SelectToken("id"),
                                Name = (string)obj.SelectToken("name")
                            });
                        }

                    return new HttpResult (true, stopManager);
                
                        //return new HttpResult(true, null);
                    }
                    catch (Exception e)
                    {
                        throw new ArgumentException("Bad data", e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Bad connection", e.Message);
            }
		}



		public async Task<HttpResult> GetNearestStops(double Lat, double Lon)
		{
            await GetToken();

            var hasAddedStopList = new List<string>();

            try
            {
                /*
                 *  Set up basic autentisering
                 */
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(" https://api.vasttrafik.se/bin/rest.exe/v2/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._accessToken);


                    var result = await client.GetAsync("location.nearbystops" + "?format=json&maxDist=6500&maxNo=1000&originCoordLat=" + Lat.ToString().Replace(",", ".") +"&originCoordLong=" + Lon.ToString().Replace(",", "."));
                    result.EnsureSuccessStatusCode();


                    string resultContent = await result.Content.ReadAsStringAsync();


                    if (resultContent == null ||
                        resultContent.Length < 3)
                    {
                        throw new ArgumentException("Bad data");
                    }

                    try
                    {

                        var res = JObject.Parse(resultContent);


                        JArray locationListArr = (JArray)res["LocationList"]["StopLocation"];
                        var stopManager = new StoppingPlaces();

                        var length = locationListArr.Count;
                        foreach (var obj in (JToken)locationListArr)
                        {

                            var element = hasAddedStopList.Find(e => e == (string)obj.SelectToken("name"));
                            if(element != null) {
                                continue;
                            }
                            stopManager.Add(new StoppingPlace()
                            {
                                Lat = (double)obj.SelectToken("lat"),
                                Lon = (double)obj.SelectToken("lon"),
                                Id = (string)obj.SelectToken("id"),
                                Name = (string)obj.SelectToken("name")
                            });


                            hasAddedStopList.Add((string)obj.SelectToken("name"));
                        }

                        return new HttpResult(true, stopManager);
                    }
                    catch (Exception e)
                    {
                        throw new ArgumentException("Bad data", e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Bad connection", e.Message);
            }

		}




	}
}


