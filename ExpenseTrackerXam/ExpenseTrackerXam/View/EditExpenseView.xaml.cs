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
	public partial class EditExpenseView : MasterDetailPage
	{
        Expense _expense = new Expense();
		public EditExpenseView (Expense expense)
		{
			InitializeComponent ();
		    _expense = expense;
		}

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        ExpenseDate.Date = _expense.Date;
	        ExpenseAmount.Text = _expense.Amount.ToString();
	        ExpenseDescription.Text = _expense.Description;
	    }

	    private void SaveBtn_OnClicked(object sender, EventArgs e)
	    {
	        HttpClient client = ExpenseTrackerHttpClient.GetClient();

	        Expense expense = new Expense()
	        {
                Id = _expense.Id,
                Date = ExpenseDate.Date,
	            ExpenseGroupId = _expense.ExpenseGroupId,
	            Amount = Convert.ToDecimal(ExpenseAmount.Text),
	            Description = ExpenseDescription.Text
	        };

	        var serializedItemToCreate = JsonConvert.SerializeObject(expense);

	        var response = client.PutAsync("api/expenses/" + _expense.Id,
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