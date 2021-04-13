using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKompTask.Client.Events;
using TKompTask.Shared;

namespace TKompTask.Client.Pages
{
    public class DataViewModel : Screen, IHandle<Events.ActivateFetchDataButtonEvent>
    {
        public ApiClient ApiClient { get; set; }
        public bool IsBusy { get; set; }
        public bool CanFetchData => ApiClient != null && !IsBusy;
        public BindableCollection<ColumnInfoDto> ColumnInfoList { get; set; }

        public DataViewModel(IEventAggregator events)
        {
            events.Subscribe(this);
        }

        public async void FetchData()
        {
            IsBusy = true;
            var list = await ApiClient.GetColumnInfoForInts();
            ColumnInfoList.AddRange(list);
            IsBusy = false;
        }

        public void Handle(ActivateFetchDataButtonEvent message)
        {
            ApiClient = message.api;
            ColumnInfoList = new BindableCollection<ColumnInfoDto>();
        }
    }
}
