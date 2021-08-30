namespace Anshan.Permission
{
    public class FakeCurrentUser : ICurrentUser
    {
        private const string UserId = "2DEE0468-8B5D-4059-9F37-87B24525436C";

        public string GetUserId()
        {
            return UserId;
        }
    }
}