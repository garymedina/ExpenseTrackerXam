﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTrackerXam.Models
{
    public class Expense
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public int ExpenseGroupId { get; set; }
    }
}
