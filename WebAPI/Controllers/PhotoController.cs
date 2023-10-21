using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Abstract;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.Dtos;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private IOptions<CloudinarySettings> _cloudinaryConfig;
        private IPhotoService _photoService;
        private Cloudinary _cloudinary;

        public PhotoController(IOptions<CloudinarySettings> cloudinaryConfig, IPhotoService photoService)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _photoService = photoService;

            Account account = new Account
            {
                Cloud = _cloudinaryConfig.Value.CloudName,
                ApiKey = _cloudinaryConfig.Value.ApiKey,
                ApiSecret = _cloudinaryConfig.Value.ApiSecret
            };

            _cloudinary = new Cloudinary(account);
        }

        [HttpPost]
        [Route("uploadPhoto")]
        public IActionResult UploadPhoto([FromBody] PhotoForCreationDto photoForCreationDto)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var file = photoForCreationDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length>0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream)
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Url.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = new Photo
            {
                UserId = currentUserId,
                PublicId = photoForCreationDto.PublicId,
                Url = photoForCreationDto.Url
            };

            var result = _photoService.UploadPhoto(photo);
            if (result.Success)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("getProfilePhoto")]
        public IActionResult GetProfilePhoto([FromBody] int userId)
        {
            var result = _photoService.GetProfilePhoto(userId);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }
        
    }
}
