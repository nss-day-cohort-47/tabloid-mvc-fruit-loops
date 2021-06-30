using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class SubscriptionConfirmationViewModel
    {
        public Subscription subscription { get; set; }

        public int LoggedInUser { get; set; }
    }
}
