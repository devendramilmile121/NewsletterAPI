using Domain.Entities;


namespace Infrastructure.Data
{
    public class AppDbContextInitilizer
    {
        public static async Task SeedReferralDataAsync(AppDbContext context)
        {
            if (!context.ReferralSources.Any())
            {
                List<ReferralSource> referralSources = new List<ReferralSource>();
                referralSources.Add(new ReferralSource { Name = "Advert" });
                referralSources.Add(new ReferralSource { Name = "Word of Mouth" });
                referralSources.Add(new ReferralSource { Name = "Other" });
                
                context.ReferralSources.AddRange(referralSources);

                await context.SaveChangesAsync();
            }
        }
    }
}
