using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ExpenseTrackerXam.Helpers;
using ExpenseTrackerXam.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTrackerXam.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExpenseGroupsView : MasterDetailPage
	{
		public ExpenseGroupsView ()
		{
			InitializeComponent ();
		    
		}

	    protected override void OnAppearing()
	    {
	        GetExpenseGroups();
	    }

	    private ObservableCollection<ExpenseGroup> _expenseGroups = null;
	    public ObservableCollection<ExpenseGroup> ExpenseGroups
	    {
	        get
	        {

	            return _expenseGroups;
	        }
	        set
	        {
	            if (_expenseGroups != value)
	            {
	                _expenseGroups = value;
	                OnPropertyChanged();
	            }

	        }
	    }

        private async Task GetExpenseGroups()
	    {
	        // load open expense groups
	        var client = ExpenseTrackerHttpClient.GetClient();

	        HttpResponseMessage response = await client
	            .GetAsync("api/expensegroups?fields=id,title,description&status=open");
	        string content = await response.Content.ReadAsStringAsync();

	        if (response.IsSuccessStatusCode)
	        {
	            var lstEG = JsonConvert.DeserializeObject<IEnumerable<ExpenseGroup>>(content);
	            ExpenseGroups = new ObservableCollection<ExpenseGroup>(lstEG);
	            GroupList.ItemsSource = ExpenseGroups;
	        }
	        else
	        {
	            // something went wrong, log this, handle this, show message, ...
	        }

	    }

	    private async void GroupList_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        await Navigation.PushModalAsync(new ExpensesView((ExpenseGroup)e.Item));
	    }
	}
}