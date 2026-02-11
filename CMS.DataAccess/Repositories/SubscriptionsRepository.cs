using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.DTOs;
using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace CMS.DataAccess.Repositories
{
    public class SubscriptionsRepository : GenericRepository<SubscriptionDTO>, ISubscriptionsRepository
    {
        public SubscriptionsRepository(string ConnectionString, ILogger<SubscriptionsRepository> logger) : base(ConnectionString, "Subscriptions", "SubscriptionID", logger)
        {
        }
    }
}
