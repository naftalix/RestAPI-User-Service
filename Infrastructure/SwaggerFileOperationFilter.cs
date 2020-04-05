using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UserManagment.Infrastructure
{
    public class SwaggerFileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var name = context.MethodInfo.Name;

            var fileProps = new List<PropertyInfo>();
            //foreach (var param in context.MethodInfo.GetParameters())
            //{
            //     fileProps.AddRange(param.ParameterType.GetProperties()
            //     .Where(prop => prop.PropertyType.FullName.Equals(typeof(Microsoft.AspNetCore.Http.IFormFile).FullName)));
            //}
            
            //var fileParams = context.MethodInfo.GetParameters()
            //    .Select(param => param.GetType().GetProperties()
            //    .Where(prop => prop.PropertyType.FullName.Equals(typeof(Microsoft.AspNetCore.Http.IFormFile).FullName))
            //    .First()).ToList();

            if (fileProps.Any() && fileProps.Count() == 1 && false)
            {
                var nameParameter = fileProps.First().Name;

                var uploadFileMediaType = new OpenApiMediaType()
                {
                    Schema = new OpenApiSchema()
                    {
                        Type = "object",
                        Properties =
                        {
                            [nameParameter] = new OpenApiSchema()
                            {
                                Description = "Upload File",
                                Type = "file",
                                Format = "binary"
                            }
                        },

                        Required = new HashSet<string>()
                        {
                             nameParameter
                        }
                    }
                };


                //operation.Parameters = new List<OpenApiParameter>
                //{
                //    new OpenApiParameter
                //    {
                //        Name = nameParameter,
                //        Required = true,
                //        In = ParameterLocation.Header,
                //        Schema = new OpenApiSchema
                //        {
                //            Type = "file",
                //            Format = "binary",

                //        }
                //    }
                //};



                //var cont = operation.RequestBody.Content;


                operation.RequestBody = new OpenApiRequestBody
                {
                    Content =
                    {
                        ["multipart/form-data"] = uploadFileMediaType
                    }
                };

                //foreach (var item in cont)
                //{
                //    operation.RequestBody.Content.Add(item);

                //}

            }
        }

    }
}
