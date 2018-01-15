﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTrackerXam.Models
{
    public class ExpenseGroup
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int ExpenseGroupStatusId { get; set; }

        public ICollection<Expense> Expenses { get; set; }
    }
}
