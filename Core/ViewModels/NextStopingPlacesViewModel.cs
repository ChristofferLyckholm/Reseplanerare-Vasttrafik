using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Http;
using Core.Travel;
using Ninject;

namespace Core.ViewModels
{
    public class NextStopingPlacesViewModel
    {
        [Inject]
        public NextStopingPlacesViewModel()
        {
            
        }

        public async Task<StoppingTimeManager> GetArivalsList(StoppingPlace from, StoppingPlace to, DateTime date)
        {
            var http = new Http.API();

            HttpResult stoppingTimeManager;
            if(to == null) {
                stoppingTimeManager = (HttpResult)await http.GetStop(from, date);
                var sortedList = ((StoppingTimeManager)stoppingTimeManager.Data);

                return sortedList;
            }

            return null;

        }
    }
}
