using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MdHack.Controllers.Models;
using MdHack.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MdHack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaceController : ControllerBase
    {
        private readonly AppDb _appDb;
        const string Group = "main-group";

        public FaceController(AppDb appDb)
        {
            _appDb = appDb;
        }
        private const string SubscriptionKey = "a718e2cdbd7147ca8243ab9f39c2a735";
        private const string FaceEndpoint =
            "https://northeurope.api.cognitive.microsoft.com/";
        private static readonly FaceAttributeType[] FaceAttributes =
            {FaceAttributeType.Age, FaceAttributeType.Gender};


        [HttpPost("face-add")]
        public async Task FaceAuth([FromForm] AddFaceModel model)
        {
            var faceClient = new FaceClient(
                new ApiKeyServiceClientCredentials(SubscriptionKey),
                new System.Net.Http.DelegatingHandler[] { });
            faceClient.Endpoint = FaceEndpoint;
            // await faceClient.PersonGroup.CreateAsync(Group, Group);
            using (var main = new MemoryStream())
            using (var modelMain = model.Main.OpenReadStream())
            using (var modelOne = model.One.OpenReadStream())
            using (var modelTwo = model.Two.OpenReadStream())
            {
                modelMain.CopyTo(main);
                main.Seek(0, 0);
                var person = await faceClient.PersonGroupPerson.CreateAsync(Group, Guid.NewGuid().ToString());
                await faceClient.PersonGroupPerson.AddFaceFromStreamAsync(Group, person.PersonId, model.Main.OpenReadStream());
                await faceClient.PersonGroupPerson.AddFaceFromStreamAsync(Group, person.PersonId, modelOne);
                await faceClient.PersonGroupPerson.AddFaceFromStreamAsync(Group, person.PersonId, modelTwo);
                await faceClient.PersonGroup.TrainAsync(Group);
                var user = AppUser.WithFace(person.PersonId.ToString(), "");
                user.Name = model.Name;
                user.Passport = model.Passport;
                main.Seek(0, 0);
                user.Avatar = main.GetBuffer();
                await _appDb.AddAsync(user);
                await _appDb.SaveChangesAsync();
            }
        }

        [HttpPost("face-auth")]
        public async Task<FaceAuthModel> FaceAuth([FromForm]DetectModel file)
        {
               var faceClient = new FaceClient(
                new ApiKeyServiceClientCredentials(SubscriptionKey),
                new System.Net.Http.DelegatingHandler[] { });
            faceClient.Endpoint = FaceEndpoint;
            var faceData = await DetectStream(faceClient, file.Face.OpenReadStream());
            if (faceData == null)
            {
                return null;
            }
            var user = await _appDb.Users.FirstOrDefaultAsync(f => f.FaceId == faceData.FaceId);
            if (user != null)
            {
                return new FaceAuthModel(user.Id.ToString(), AuthHelper.GetToken(user.Id.ToString()))
                {
                    FaceData = faceData,
                    Name = user.Name
                };
            }
            else
            {
                var newUser = AppUser.WithFace(faceData.FaceId, JsonConvert.SerializeObject(faceData));
                await _appDb.Users.AddAsync(newUser);
                await _appDb.SaveChangesAsync();
                return new FaceAuthModel(newUser.Id.ToString(), AuthHelper.GetToken(newUser.Id.ToString()))
                {
                    FaceData = faceData
                };
            }
        }

        // Detect faces in a local image
        private static async Task<FaceData> DetectStream(FaceClient faceClient, Stream stream)
        {
            try
            {
                var faceList =
                    await faceClient.Face.DetectWithStreamAsync(
                        stream, true, false, FaceAttributes);
                var faces = faceList.Where(w => w.FaceId != null)
                    .Select(s => s.FaceId.Value).ToList();
                var identify = await faceClient.Face.IdentifyWithHttpMessagesAsync(faces, Group);
                var result = identify.Body.FirstOrDefault();
                if (result == null)
                {
                    return null;
                }
                var data = new FaceData();
                foreach (var face in faceList)
                {
                    var age = face.FaceAttributes.Age;
                    var gender = face.FaceAttributes.Gender.ToString();
                    data.Age = age;
                    data.FaceId = result.Candidates.First().PersonId.ToString();
                    data.Gender = gender;
                }
                return data;
            }
            catch (APIErrorException e)
            {
                return null;
            }
        }
    }
}