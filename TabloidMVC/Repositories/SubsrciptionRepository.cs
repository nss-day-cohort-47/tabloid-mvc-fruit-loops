using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class SubsrciptionRepository : BaseRepository, ISubscriptionRepository
    {
        public SubsrciptionRepository(IConfiguration config) : base(config) { }
       

        public void AddSubscription(int subscriber, int poster)
        {
            using (var conn = Connection)
            {
                conn.Open();

                DateTime startDateTime = DateTime.Now;
                 // public DateTime BeginDateTime { get; set; }
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Subscription (
                            SubscriberUserProfileId, ProviderUserProfileId, BeginDateTime )
                        VALUES (
                            @SubscriberUserProfileId, @ProviderUserProfileId, @BeginDateTime )";
                    cmd.Parameters.AddWithValue("@SubscriberUserProfileId", subscriber);
                    cmd.Parameters.AddWithValue("@ProviderUserProfileId", poster);
                    cmd.Parameters.AddWithValue("@BeginDateTime", startDateTime);

                    //subscription.Id = (int)cmd.ExecuteScalar();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
