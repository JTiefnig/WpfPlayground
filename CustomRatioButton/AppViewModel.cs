using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CustomRatioButton
{

    public class ListItem
    {
        public String Name { get; set; }
    }


    public class AppViewModel
    {
        public ObservableCollection<ListItem> MyItemsList { get; set; }
            = new ObservableCollection<ListItem> {
                new ListItem { Name="eins" },
                new ListItem { Name = "zwei" },
                new ListItem { Name = "drei" }
            };

        public ListItem SelectedItem { get; set; }

        public AppViewModel()
        {
            // doo someting
        }

        public ICommand AddListItem => new RelayCommand(() =>
            this.MyItemsList.Add(new ListItem() { Name = "new itme" }));

        public ICommand DeleteItem => new ParameterizedRelayCommand((item) =>
            this.MyItemsList.Remove(item as ListItem));

        /// <summary>
        /// Properaty of the selected Options
        /// </summary>
        public Option SelectedOption { get; set; }

        public Option SelectedOptionRadioList { get; set; }
        /// <summary>
        /// Some Random options to test the comobbox
        /// </summary>
        public static List<Option> MyOptions { get; } = new List<Option>()
        {
            new Option(){Name= "Central", Number=0},
            new Option(){Name= "LaxWendroff", Number=1},
            new Option(){Name= "MacCormack", Number=2},
            new Option(){Name= "ROE", Number=3}
        };



        public enum MoreOptions
        {
            Send,
            Forward,
            Replay
        }

    }
}
