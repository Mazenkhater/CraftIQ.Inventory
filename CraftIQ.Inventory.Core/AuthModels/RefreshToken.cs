namespace CraftIQ.Inventory.Core.AuthModels
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }

        public bool IsRevoked { get; set; }

        public Guid UserId { get; set; }
        public AppUser User { get; set; }
    }
}
