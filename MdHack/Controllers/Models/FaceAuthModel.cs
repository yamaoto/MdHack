namespace MdHack.Controllers.Models
{
    public class FaceAuthModel : AuthModel
    {
        public FaceAuthModel(string userId, string accessToken) : base(userId, accessToken)
        {
        }

        public FaceData FaceData { get; set; }
    }
}