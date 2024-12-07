using CleanArchProject.Core.SharedResources;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Core.Bases
{
    public class ResponseHandler
    {
        IStringLocalizer _stringLocalizer;

        public ResponseHandler(IStringLocalizer stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }

        public Response<T> Deleted<T>(string message)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = message.IsNullOrEmpty()? _stringLocalizer[SharedResourcesKeys.Deleted] : message
            };
        }
        public Response<T> InternalServerError<T>()
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Succeeded = true,
                Message = "InternalServerError"
            };
        }
        public Response<T> Success<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = _stringLocalizer[SharedResourcesKeys.Success],
                Meta = Meta
            };
        }
        public Response<T> Unauthorized<T>()
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = true,
                Message = _stringLocalizer[SharedResourcesKeys.UnAuthorized]
            };
        }
        public Response<T> BadRequest<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message == null ? _stringLocalizer[SharedResourcesKeys.BadRequest] : Message
            };
        }

        public Response<T> UnprocessableEntity<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = Message == null ? _stringLocalizer[SharedResourcesKeys.UnprocessableEntity] : Message
            };
        }

        public Response<T> NotFound<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message == null ? _stringLocalizer[SharedResourcesKeys.NotFound] : message
            };
        }

        public Response<T> Created<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = _stringLocalizer[SharedResourcesKeys.Created],
                Meta = Meta
            };
        }

        public Response<T> Conflict<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Conflict,
                Succeeded = false,
                Message = _stringLocalizer[SharedResourcesKeys.Conflict],
                Meta = Meta
            };
        }
    }
}
