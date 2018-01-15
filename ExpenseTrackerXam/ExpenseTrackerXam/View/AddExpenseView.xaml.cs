using System;
using System.Collections.Generic;
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
	public partial class AddExpenseView : MasterDetailPage
	{
	    private int ExpenseGroupId;
		public AddExpenseView (int id)
		{
			InitializeComponent ();
		    ExpenseGroupId = id;
		}

	    private void SaveBtn_OnClicked(object sender, EventArgs e)
	    {
	        Expense expense = new Expense()
	        {
	            ExpenseGroupId = ExpenseGroupId,
                Amount = Convert.ToDecimal(ExpenseAmount.Text),
                Description = ExpenseDescription.Text
	        };

	        HttpClient client = ExpenseTrackerHttpClient.GetClient();

	        if (expense.Date < DateTime.Now.AddYears(-2))
	        {
	            expense.Date = DateTime.Now;
	        }

	        var serializedItemToCreate = JsonConvert.SerializeObject(expense);

	        var response = client.PostAsync("api/expenses",
	            new StringContent(serializedItemToCreate,
	                System.Text.Encoding.Unicode, "application/json")).Result;

	        if (response.IsSuccessStatusCode)
	        {
	            Navigation.PopModalAsync();
	        }
	        else
	        {
	            DisplayAlert("Alert", "Save was unsuccesfull", "Cancel");
	        }
	    }
	}
}