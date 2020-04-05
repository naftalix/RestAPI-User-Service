using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BrunoZell.ModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using UserManagment.Infrastructure;
using UserManagment.Models;
using UserManagment.Resources;
using UserManagment.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagment.Controllers
{
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IHostEnvironment _host;
        private readonly IMapper _mapper;


        public FilesController(IFileService fileService, IMapper mapper, IHostEnvironment host)
        {
            _fileService = fileService;
            _host = host;
            _mapper = mapper;
        }

        // GET api/values/5
        [HttpGet("{id}", Name = nameof(GetFileById))]
        [ValidateModel]
        public IActionResult GetFileById(string id)
        {
            var fileModel = _fileService.GetFileMetadata(id);

            return new OkObjectResult(fileModel);
        }

        // POST api/files
        [HttpPost("upload")]
        [ValidateModel]
        public IActionResult Upload(IFormFile file, FileResource resource)
        {
            try
            {
                if (resource == null || !resource.Id.NotEmpty() || file.IsNull())
                {
                    return BadRequest();
                }

                var fileModel = _mapper.Map<FileResource, FileModel>(resource);

                GenerateFilePath(fileModel, Path.GetExtension(file.FileName));

                _fileService.UploadFile(file, fileModel);

                _fileService.CreateFile(fileModel);

                resource.FilePath = fileModel.FilePath;

                return CreatedAtRoute("GetFileById", new { id = resource.Id }, resource);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }

        private void GenerateFilePath(FileModel fileModel, string extention)
        {
            var uploadFolder = Path.Combine(_host.ContentRootPath, "UploadedFiles");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileModel.Name + extention;
            var filePath = Path.Combine(uploadFolder, uniqueFileName);
            fileModel.FilePath = filePath;
            fileModel.UniqueFileName = uniqueFileName;
            fileModel.Name += extention;
        }

        [HttpGet("download/{fileId}")]
        [ValidateModel]
        public IActionResult Download(string fileId)
        {
            try
            {
                if (fileId == string.Empty)
                {
                    return BadRequest();
                }

                var fileData = _fileService.GetFileMetadata(fileId);


                if (fileData != null && System.IO.File.Exists(fileData.FilePath))
                {
                    var fileBytes = System.IO.File.ReadAllBytes(fileData.FilePath);
                    var fileName = fileData.Name;
                    var fileType = FileHelper.GetMimeType(fileName);

                    return File(fileBytes, fileType, fileName);
                }
                else
                {
                    if (fileData != null)
                    {
                        _fileService.DeleteFile(fileId);
                    }

                    return NotFound();
                }


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("setfileuser/{fileId}")]
        [ValidateModel]
        public IActionResult SetFilesUser(string fileId, [FromBody] string userId)
        {
            if (!userId.NotEmpty() || !fileId.NotEmpty())
            {
                return BadRequest();
            }

            try
            {
                _fileService.AddFileToUser(fileId, userId);

                return Accepted();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [ValidateModel]
        public IActionResult Update(string id, [FromBody]FileModel data)
        {
            if (data == null || !id.NotEmpty() || data.Id != id)
            {
                return BadRequest();
            }

            try
            {
                var file = _fileService.GetFileMetadata(id);

                _fileService.UpdateFile(file);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ValidateModel]
        public IActionResult Delete(string id)
        {

            try
            {
                _fileService.DeleteFile(id);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
