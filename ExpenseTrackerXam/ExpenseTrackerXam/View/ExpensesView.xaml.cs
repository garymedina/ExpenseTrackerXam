using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
	public partial class ExpensesView : MasterDetailPage
	{
		public ExpensesView (ExpenseGroup expenseGroup)
		{
			InitializeComponent ();
		    ExpenseGroup = expenseGroup;
		}

	    private ExpenseGroup _expenseGroup = null;
	    public ExpenseGroup ExpenseGroup
	    {
	        get
	        {
	            return _expenseGroup;
	        }
	        set
	        {
	            if (_expenseGroup != value)
	            {
	                _expenseGroup = value;
	                OnPropertyChanged();
	            }
	        }
	    }

	    private ObservableCollection<Expense> _expenses = new ObservableCollection<Expense>();
	    public ObservableCollection<Expense> Expenses
	    {
	        get
	        {

	            return _expenses;
	        }
	        set
	        {
	            if (_expenses != value)
	            {
	                _expenses = value;
	                OnPropertyChanged();
	            }
	        }
	    }

	    protected override void OnAppearing()
	    {
	        GetExpenses();
	    }

	    private async Task GetExpenses()
	    {
	        //Expenses.Clear();


	        // load expenses for group
	        var client = ExpenseTrackerHttpClient.GetClient();
	
	        HttpResponseMessage response = await client.GetAsync("api/expensegroups/"
	                                     + ExpenseGroup.Id + "/expenses");

	        string content = await response.Content.ReadAsStringAsync();

	        if (response.IsSuccessStatusCode)
            { 
	            var lstExpenses = JsonConvert.DeserializeObject<IEnumerable<Expense>>(content);
	            Expenses = new ObservableCollection<Expense>(lstExpenses);
	            ExpenseList.ItemsSource = Expenses;
	        }
	        else
	        {
	            // something went wrong, log this, handle this, show message, ...
	        }
	    }



        private void ExpenseList_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushModalAsync(new EditExpenseView((Expense)e.Item));
        }

	    private async void RefreshBtn_OnClicked(object sender, EventArgs e)
	    {
	        await GetExpenses();
	    }

	    private void AddButton_OnClicked(object sender, EventArgs e)
	    {
	        Navigation.PushModalAsync(new AddExpenseView(ExpenseGroup.Id));
	    }
	}
}